using glTech.ePipemonitor.WSNSCADAPlugin.Models;
using glTech.ePipemonitor.WSNSCADAPlugin.Repositories;
using PluginContract;
using PluginContract.Data;
using PluginContract.Extensions;
using PluginContract.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace glTech.ePipemonitor.WSNSCADAPlugin
{
    class DataBulkService
    {
        private IDapper _repo;
        private PluginMonitor _monitorActiveDb;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ConcurrentQueue<SubStationUpdateRealDataEventArgs> _queue = new ConcurrentQueue<SubStationUpdateRealDataEventArgs>();
        private static readonly Dictionary<Type, (MethodInfo, string[])> _staticMethodProperty 
            = new Dictionary<Type, (MethodInfo, string[])>();
        private Task _taskDatabase;
        public void DasUpdateRealData(object sender, SubStationUpdateRealDataEventArgs e)
        {
            _queue.Enqueue(e);
        }

        public void Start(PluginMonitor monitorActiveDb)
        {
            BuildTableInfo<RealDataModel>();
            BuildTableInfo<Alarm_TodayModel>();
            BuildTableInfo<AnalogAlarmModel>();
            BuildTableInfo<AnalogRunModel>();
            BuildTableInfo<AnalogStatisticModel>();
            //BuildTableInfo<SubStationRunModel>();
            BuildTableInfo<FluxRealDataModel>();
            BuildTableInfo<FluxRunModel>();
            _monitorActiveDb = monitorActiveDb;
            _repo = DasConfig.Repo.Dapper;
            _taskDatabase = Task.Factory.StartNew(ExecuteBulk, _cts.Token);
        }

        public void Stop()
        {
            _cts.Cancel();
            if (_taskDatabase != null)
                // 等待两个任务执行完成.
                Task.WaitAll(new[] { _taskDatabase });
        }
        private void BuildTableInfo<T>()
            where T : class
        {
            MethodInfo list2TableMethod = typeof(T).GetMethod("List2Table", BindingFlags.Public | BindingFlags.Static);
            var primaryKeysProperty =
                typeof(T).GetProperty("PrimaryKeys", BindingFlags.Public | BindingFlags.Static);

            if (primaryKeysProperty == null)
            {
                throw new NotSupportedException("保存数据库时, 主键无法找到.");
            }
            string[] primaryKeys = (string[])primaryKeysProperty.GetValue(null, null);

            _staticMethodProperty.Add(typeof(T), (list2TableMethod, primaryKeys));
        }
        private void ExecuteBulk()
        {
            CultureInfoHelper.SetDateTimeFormat();
            while (!_cts.IsCancellationRequested)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    while (!_cts.IsCancellationRequested && _queue.Any())
                    {
                        //Log($"插入数据库队列数目为:{_queue.Count}...");
                        Stopwatch stopwatch1 = new Stopwatch();
                        stopwatch1.Start();
                        string message = "";
                        var dict = ExtractDict(_queue);
                        var realDataModels = dict.Values.SelectMany(o => o.RealDataModels).ToList();
                        var alarmTodayModels = dict.Values.SelectMany(o => o.Alarm_TodayModels).ToList();
                        var analogAlarmModels = dict.Values.SelectMany(o => o.AnalogAlarmModels).ToList();
                        var analogRunModels = dict.Values.SelectMany(o => o.AnalogRunModels).ToList();
                        var analogStatisticModels = dict.Values.SelectMany(o => o.AnalogStatisticModels).ToList();
                        var fluxRealDataModels = dict.Values.SelectMany(o => o.FluxRealDataModels).ToList();
                        var fluxRunModels = dict.Values.SelectMany(o => o.FluxRunModels).ToList();

                        var realDataItem = BulkModels(realDataModels, ref message);
                        var alarmTodayItem = BulkModels(alarmTodayModels, ref message);
                        var analogAlarmItem = BulkModels(analogAlarmModels, ref message);
                        var analogRunItem = BulkModels(analogRunModels, ref message);
                        var analogStatisticItem = BulkModels(analogStatisticModels, ref message);
                        var fluxRealDataItem = BulkModels(fluxRealDataModels, ref message);
                        var fluxRunItem = BulkModels(fluxRunModels, ref message);


                        Bulk(realDataItem.Item1, realDataItem.Item2, ref message);
                        Bulk(alarmTodayItem.Item1, alarmTodayItem.Item2, ref message);
                        Bulk(analogAlarmItem.Item1, analogAlarmItem.Item2, ref message);
                        Bulk(analogRunItem.Item1, analogRunItem.Item2, ref message);
                        Bulk(analogStatisticItem.Item1, analogStatisticItem.Item2, ref message);

                        Bulk(fluxRealDataItem.Item1, fluxRealDataItem.Item2, ref message);
                        Bulk(fluxRunItem.Item1, fluxRunItem.Item2, ref message);
                        if (!string.IsNullOrEmpty(message))
                            Log("=".Repeat(20) + message + $"共耗时:{stopwatch1.Elapsed.TotalSeconds:F2}" + "=".Repeat(20));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Log("保存数据库出错 " + e);
                }

                SleepLifeCycle(null, stopwatch);
            }
        }
        /// <summary>
        /// 等待一个巡检周期.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="stopwatch"></param>
        private void SleepLifeCycle(Action action = null, Stopwatch stopwatch = null)
        {
            if (stopwatch == null)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }

            while (stopwatch.Elapsed.TotalSeconds < DasConfig.DasGatherInterval && !_cts.IsCancellationRequested)
            {
                action?.Invoke();
                Thread.Sleep(50);
            }
            stopwatch.Stop();
        }

        /// <summary>
        /// 执行写入数据库操作.
        /// </summary>
        private void Bulk(string[] primaryKeys, List<DataTable> dts, ref string message)
        {
            foreach (var dt in dts)
            {
                if (dt.Rows.Count == 0) continue;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                if (dt == null)
                {
                    throw new NotSupportedException("保存数据库时, 调用函数不存在");
                }
                var count = Bulk(_repo, dt, primaryKeys);

                message += $"{dt.TableName}:{count} ";
                Log($"保存{dt.TableName} 共 {count} 条, 耗时 {stopwatch.Elapsed.TotalSeconds:F2} 秒");
                stopwatch.Stop();
            }
        }
        private int Bulk(IDapper repo, DataTable dt, params string[] primarykeys)
        {
            try
            {
                var count = repo.BulkUpdate(dt, dt.TableName, primarykeys);
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log($"Bulk {dt.TableName} , 共{dt.Rows.Count}条,出错." + e);
                return -1;
            }
        }
        private void Log(string message, bool isAddMonitor = true)
        {
            if (isAddMonitor)
                _monitorActiveDb.OnWatch(_monitorActiveDb.Id, DateTime.Now.ToString(), message);
            LogD.Info(message);
        }

        /// <summary>
        /// 生成主键和datatable
        /// </summary>
        private Tuple<string[], List<DataTable>> BulkModels<T>(List<T> models, ref string message)
            where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var item = _staticMethodProperty[typeof(T)];
            MethodInfo list2TableMethod = item.Item1;
            string[] primaryKeys = item.Item2;
            stopwatch.Restart();
            var dts = (IEnumerable<DataTable>)list2TableMethod?.Invoke(null, new[] { models });

            return Tuple.Create(primaryKeys, dts.ToList());
        }

        /// <summary>
        /// 从队列里面获取每个采集的实时数据.
        /// </summary>
        /// <param name="queue">队列</param>
        /// <returns>实时数据字典</returns>
        private Dictionary<int, SubStationUpdateRealDataEventArgs> ExtractDict(ConcurrentQueue<SubStationUpdateRealDataEventArgs> queue)
        {
            var dict = new Dictionary<int, SubStationUpdateRealDataEventArgs>();
            var secondQueue = new Queue<SubStationUpdateRealDataEventArgs>();
            while (_queue.Any())
            {
                if (_queue.TryDequeue(out SubStationUpdateRealDataEventArgs e))
                {
                    if (!dict.ContainsKey(e.DasId))
                    {
                        dict.Add(e.DasId, e);
                    }
                    else
                    {
                        secondQueue.Enqueue(e);
                    }
                }
            }
            while (secondQueue.Any())
            {
                _queue.Enqueue(secondQueue.Dequeue());
            }
            return dict;
        }
    }

    public interface IPrimaryModel
    {
        string Key { get; }
    }
}

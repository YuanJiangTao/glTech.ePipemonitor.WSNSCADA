﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PluginContract.Utils
{
    /// <summary>
    ///     FTP 功能模块
    /// </summary>
    public class MyFtp
    {
        #region 构造函数

        /// <summary>
        ///     缺省构造函数
        /// </summary>
        public MyFtp()
        {
            strRemoteHost = "";
            strRemotePath = "";
            strRemoteUser = "";
            strRemotePass = "";
            strRemotePort = 21;
            bConnected = false;
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="remoteHost"></param>
        /// <param name="remotePath"></param>
        /// <param name="remoteUser"></param>
        /// <param name="remotePass"></param>
        /// <param name="remotePort"></param>
        public MyFtp(string remoteHost, string remotePath, string remoteUser, string remotePass, int remotePort)
        {
            strRemoteHost = remoteHost;
            strRemotePath = remotePath;
            strRemoteUser = remoteUser;
            strRemotePass = remotePass;
            strRemotePort = remotePort;
            Connect();
        }

        #endregion

        #region 登陆

        /// <summary>
        ///     FTP服务器IP地址
        /// </summary>
        private string strRemoteHost;

        public string RemoteHost
        {
            get => strRemoteHost;
            set => strRemoteHost = value;
        }

        /// <summary>
        ///     FTP服务器端口
        /// </summary>
        private int strRemotePort;

        public int RemotePort
        {
            get => strRemotePort;
            set => strRemotePort = value;
        }

        /// <summary>
        ///     当前服务器目录
        /// </summary>
        private string strRemotePath;

        public string RemotePath
        {
            get => strRemotePath;
            set => strRemotePath = value;
        }

        /// <summary>
        ///     登录用户账号
        /// </summary>
        private string strRemoteUser;

        public string RemoteUser
        {
            set => strRemoteUser = value;
        }

        /// <summary>
        ///     用户登录密码
        /// </summary>
        private string strRemotePass;

        public string RemotePass
        {
            set => strRemotePass = value;
        }

        /// <summary>
        ///     是否登录
        /// </summary>
        private bool bConnected;

        public bool Connected => bConnected;

        #endregion

        #region 连接

        /// <summary>
        ///     建立连接
        /// </summary>
        public void Connect()
        {
            socketControl =
                new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    SendTimeout = 5000,
                    ReceiveTimeout = 5000
                };
            var ep = new IPEndPoint(IPAddress.Parse(RemoteHost), strRemotePort);
            // 链接
            try
            {
                socketControl.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Couldn't connect to remote server");
            } // 获取应答码
            ReadReply();
            if (iReplyCode != 220)
            {
                DisConnect();
                throw new IOException(strReply.Substring(4));
            } // 登陆
            SendCommand("USER " + strRemoteUser);
            if (!(iReplyCode == 331 || iReplyCode == 230))
            {
                CloseSocketConnect(); //关闭连接
                throw new IOException(strReply.Substring(4));
            }
            if (iReplyCode != 230)
            {
                SendCommand("PASS " + strRemotePass);
                if (!(iReplyCode == 230 || iReplyCode == 202))
                {
                    CloseSocketConnect(); //关闭连接
                    throw new IOException(strReply.Substring(4));
                }
            }
            bConnected = true; // 切换到目录
            ChDir(strRemotePath);
        }

        /// <summary>
        ///     关闭连接
        /// </summary>
        public void DisConnect()
        {
            if (socketControl != null)
                SendCommand("QUIT");
            CloseSocketConnect();
        }

        #endregion

        #region 传输模式

        /// <summary>
        ///     传输模式:二进制类型、ASCII类型
        /// </summary>
        public enum TransferType
        {
            Binary,
            ASCII
        }

        /// <summary>
        ///     设置传输模式
        /// </summary>
        /// <param name="ttType">传输模式</param>
        public void SetTransferType(TransferType ttType)
        {
            if (ttType == TransferType.Binary)
                SendCommand("TYPE I"); //binary类型传输
            else
                SendCommand("TYPE A"); //ASCII类型传输
            if (iReplyCode != 200)
                throw new IOException(strReply.Substring(4));
            trType = ttType;
        }

        /// <summary>
        ///     获得传输模式
        /// </summary>
        /// <returns>传输模式</returns>
        public TransferType GetTransferType()
        {
            return trType;
        }

        #endregion

        #region 文件操作

        /// <summary>
        ///     获得文件列表
        /// </summary>
        /// <param name="strMask">文件名的匹配字符串</param>
        /// <returns></returns>
        public string[] Dir(string strMask)
        {
            // 建立链接
            if (!bConnected)
                Connect();
            var socketData = CreateDataSocket();
            //传送命令
            SendCommand("NLST " + strMask); //分析应答代码
            if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226))
                throw new IOException(strReply.Substring(4));
            strMsg = "";
            while (true)
            {
                var iBytes = socketData.Receive(buffer, buffer.Length, 0);
                strMsg += ASCII.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                    break;
            }
            char[] seperator = { '\n' };
            var strsFileList = strMsg.Split(seperator);
            socketData.Close(); //数据socket关闭时也会有返回码
            if (iReplyCode != 226)
            {
                ReadReply();
                if (iReplyCode != 226)
                    throw new IOException(strReply.Substring(4));
            }
            return strsFileList;
        }

        /// <summary>
        ///     获取文件大小
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <returns>文件大小</returns>
        private long GetFileSize(string strFileName)
        {
            if (!bConnected)
                Connect();
            SendCommand("SIZE " + Path.GetFileName(strFileName));
            long lSize = 0;
            if (iReplyCode == 213)
                lSize = long.Parse(strReply.Substring(4));
            else
                throw new IOException(strReply.Substring(4));
            return lSize;
        }

        public bool Exists(string file)
        {
            if (!bConnected)
                Connect();
            SendCommand("SIZE " + Path.GetFileName(file));
            //long lSize = 0;
            if (iReplyCode == 213)
                return true;
            return false;
        }

        public bool Delete(string file)
        {
            try
            {
                DeleteFile(file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="strFileName">待删除文件名</param>
        public void DeleteFile(string strFileName)
        {
            if (!bConnected)
                Connect();
            SendCommand("DELE " + strFileName);
            if (iReplyCode != 250)
                throw new IOException(strReply.Substring(4));
        }

        /// <summary>
        ///     重命名(如果新文件名与已有文件重名,将覆盖已有文件)
        /// </summary>
        /// <param name="strOldFileName">旧文件名</param>
        /// <param name="strNewFileName">新文件名</param>
        public void Rename(string strOldFileName, string strNewFileName)
        {
            if (!bConnected)
                Connect();
            SendCommand("RNFR " + strOldFileName);
            if (iReplyCode != 350)
                throw new IOException(strReply.Substring(4));
            // 如果新文件名与原有文件重名,将覆盖原有文件
            SendCommand("RNTO " + strNewFileName);
            if (iReplyCode != 250)
                throw new IOException(strReply.Substring(4));
        }

        #endregion

        #region 上传和下载

        /// <summary>
        ///     下载一批文件
        /// </summary>
        /// <param name="strFileNameMask">文件名的匹配字符串</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        public void Get(string strFileNameMask, string strFolder)
        {
            if (!bConnected)
                Connect();
            var strFiles = Dir(strFileNameMask);
            foreach (var strFile in strFiles)
                if (!strFile.Equals("")) //一般来说strFiles的最后一个元素可能是空字符串
                    Get(strFile, strFolder, strFile);
        }

        /// <summary>
        ///     下载一个文件
        /// </summary>
        /// <param name="strRemoteFileName">要下载的文件名</param>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strLocalFileName">保存在本地时的文件名</param>
        public void Get(string strRemoteFileName, string strFolder, string strLocalFileName)
        {
            if (!bConnected)
                Connect();
            SetTransferType(TransferType.Binary);
            if (strLocalFileName.Equals(""))
                strLocalFileName = strRemoteFileName;
            if (!File.Exists(strLocalFileName))
            {
                Stream st = File.Create(strLocalFileName);
                st.Close();
            }
            var output = new
                FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
            var socketData = CreateDataSocket();
            SendCommand("RETR " + strRemoteFileName);
            if (!(iReplyCode == 150 || iReplyCode == 125
                  || iReplyCode == 226 || iReplyCode == 250))
                throw new IOException(strReply.Substring(4));
            while (true)
            {
                var iBytes = socketData.Receive(buffer, buffer.Length, 0);
                output.Write(buffer, 0, iBytes);
                if (iBytes <= 0)
                    break;
            }
            output.Close();
            if (socketData.Connected)
                socketData.Close();
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                    throw new IOException(strReply.Substring(4));
            }
        }

        /// <summary>
        ///     上传一批文件
        /// </summary>
        /// <param name="strFolder">本地目录(不得以\结束)</param>
        /// <param name="strFileNameMask">文件名匹配字符(可以包含*和?)</param>
        public void Put(string strFolder, string strFileNameMask)
        {
            var strFiles = Directory.GetFiles(strFolder, strFileNameMask);
            foreach (var strFile in strFiles)
                //strFile是完整的文件名(包含路径)
                Put(strFile);
        }

        /// <summary>
        ///     上传一个文件
        /// </summary>
        /// <param name="strFileName">本地文件名</param>
        public void Put(string strFileName)
        {
            if (!bConnected)
                Connect();
            var socketData = CreateDataSocket();
            SendCommand("STOR " + Path.GetFileName(strFileName));
            if (!(iReplyCode == 125 || iReplyCode == 150))
                try
                {
                    throw new IOException(strReply.Substring(4));
                }
                catch
                {
                }
                finally
                {
                }
            var input = new
                FileStream(strFileName, FileMode.Open);
            var iBytes = 0;
            while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                socketData.Send(buffer, iBytes, 0);
            input.Close();
            if (socketData.Connected)
                socketData.Close();
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                    throw new IOException(strReply.Substring(4));
            }
        }

        #endregion

        #region 目录操作

        /// <summary>
        ///     创建目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void MkDir(string strDirName)
        {
            if (!bConnected)
                Connect();
            SendCommand("MKD " + strDirName);
            if (iReplyCode != 257)
                throw new IOException(strReply.Substring(4));
        }

        /// <summary>
        ///     删除目录
        /// </summary>
        /// <param name="strDirName">目录名</param>
        public void RmDir(string strDirName)
        {
            if (!bConnected)
                Connect();
            SendCommand("RMD " + strDirName);
            if (iReplyCode != 250)
                throw new IOException(strReply.Substring(4));
        }

        /// <summary>
        ///     改变目录
        /// </summary>
        /// <param name="strDirName">新的工作目录名</param>
        public void ChDir(string strDirName)
        {
            if (strDirName.Equals(".") || strDirName.Equals(""))
                return;
            if (!bConnected)
                Connect();
            SendCommand("CWD " + strDirName);
            if (iReplyCode != 250)
                throw new IOException(strReply.Substring(4));
            strRemotePath = strDirName;
        }

        #endregion

        #region 内部变量

        /// <summary>
        ///     服务器返回的应答信息(包含应答码)
        /// </summary>
        private string strMsg;

        /// <summary>
        ///     服务器返回的应答信息(包含应答码)
        /// </summary>
        private string strReply;

        /// <summary>
        ///     服务器返回的应答码
        /// </summary>
        private int iReplyCode;

        /// <summary>
        ///     进行控制连接的socket
        /// </summary>
        private Socket socketControl;

        /// <summary>
        ///     传输模式
        /// </summary>
        private TransferType trType;

        /// <summary>
        ///     接收和发送数据的缓冲区
        /// </summary>
        private static readonly int BLOCK_SIZE = 512;

        private readonly byte[] buffer = new byte[BLOCK_SIZE];

        /// <summary>
        ///     编码方式
        /// </summary>
        private readonly Encoding ASCII = Encoding.Default;

        #endregion

        #region 内部函数

        /// <summary>
        ///     将一行应答字符串记录在strReply和strMsg
        ///     应答码记录在iReplyCode
        /// </summary>
        private void ReadReply()
        {
            strMsg = "";
            strReply = ReadLine();
            iReplyCode = int.Parse(strReply.Substring(0, 3));
        }

        /// <summary>
        ///     建立进行数据连接的socket
        /// </summary>
        /// <returns>数据连接socket</returns>
        private Socket CreateDataSocket()
        {
            SendCommand("PASV");
            if (iReplyCode != 227)
                throw new IOException(strReply.Substring(4));
            var index1 = strReply.IndexOf('(');
            var index2 = strReply.IndexOf(')');
            var ipData =
                strReply.Substring(index1 + 1, index2 - index1 - 1);
            var parts = new int[6];
            var len = ipData.Length;
            var partCount = 0;
            var buf = "";
            for (var i = 0; i < len && partCount <= 6; i++)
            {
                var ch = char.Parse(ipData.Substring(i, 1));
                if (char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                    throw new IOException("Malformed PASV strReply: " +
                                          strReply);
                if (ch == ',' || i + 1 == len)
                    try
                    {
                        parts[partCount++] = int.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " +
                                              strReply);
                    }
            }
            var ipAddress = parts[0] + "." + parts[1] + "." +
                            parts[2] + "." + parts[3];
            var port = (parts[4] << 8) + parts[5];
            var s = new
                Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ep = new
                IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Can't connect to remote server");
            }
            return s;
        }

        /// <summary>
        ///     关闭socket连接(用于登录以前)
        /// </summary>
        private void CloseSocketConnect()
        {
            if (socketControl != null)
            {
                socketControl.Close();
                socketControl = null;
            }
            bConnected = false;
        }

        /// <summary>
        ///     读取Socket返回的所有字符串
        /// </summary>
        /// <returns>包含应答码的字符串行</returns>
        private string ReadLine()
        {
            while (true)
            {
                var iBytes = socketControl.Receive(buffer, buffer.Length, 0);
                strMsg += Encoding.Default.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                    break;
            }
            char[] seperator = { '\n' };
            var mess = strMsg.Split(seperator);
            if (strMsg.Length > 2)
                strMsg = mess[mess.Length - 2];
            else
                strMsg = mess[0];
            if (!strMsg.Substring(3, 1).Equals(" ")) //返回字符串正确的是以应答码(如220开头,后面接一空格,再接问候字符串)
                return ReadLine();
            return strMsg;
        }

        /// <summary>
        ///     发送命令并获取应答码和最后一行应答字符串
        /// </summary>
        /// <param name="strCommand">命令</param>
        private void SendCommand(string strCommand)
        {
            var cmdBytes =
                Encoding.Default.GetBytes((strCommand + "\r\n").ToCharArray());
            socketControl.Send(cmdBytes, cmdBytes.Length, 0);
            ReadReply();
        }

        #endregion
    }
}

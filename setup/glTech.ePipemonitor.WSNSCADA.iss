; 脚本由 Inno Setup 脚本向导 生成！
; 有关创建 Inno Setup 脚本文件的详细资料请查阅帮助文档！

#define MyAppName "GPRS传感器数据采集"
#define MyAppVersion "4.3"
#define MyAppPublisher "郑州光力科技股份有限公司"
#define MyAppURL "http://www.gltech.cn/"
#define MyAppExeName "glTech.ePipemonitor.WSNSCADA.exe"

[Setup]
; 注: AppId的值为单独标识该应用程序。
; 不要为其他安装程序使用相同的AppId值。
; (生成新的GUID，点击 工具|在IDE中生成GUID。)
AppId={{91BFA1E7-853E-408A-973D-2A728AFD303F}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName=D:\Program Files\郑州光力科技股份有限公司\光力科技GPRS传感器数据采集
DefaultGroupName={#MyAppName}
OutputDir=output
OutputBaseFilename={#MyAppName}
Compression=lzma
SolidCompression=yes
DirExistsWarning=no
SetupIconFile="..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\icon.ico"
UninstallDisplayIcon="..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\icon.ico"


;安装卸载时检查程序是否正在运行
AppMutex = {#MyAppName}

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Accessibility.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-console-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-datetime-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-debug-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-errorhandling-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-file-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-file-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-file-l2-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-handle-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-heap-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-interlocked-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-libraryloader-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-localization-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-memory-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-namedpipe-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-processenvironment-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-processthreads-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-processthreads-l1-1-1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-profile-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-rtlsupport-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-string-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-synch-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-synch-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-sysinfo-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-timezone-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-core-util-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-conio-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-convert-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-environment-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-filesystem-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-heap-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-locale-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-math-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-multibyte-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-private-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-process-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-runtime-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-stdio-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-string-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-time-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\api-ms-win-crt-utility-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\clrcompression.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\clretwrc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\clrjit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\ControlzEx.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\coreclr.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\D3DCompiler_47_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Dapper.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\dbgshim.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\DirectWriteForwarder.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\glTech.ePipemonitor.WSNSCADA.deps.json"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\glTech.ePipemonitor.WSNSCADA.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\glTech.ePipemonitor.WSNSCADA.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\glTech.ePipemonitor.WSNSCADA.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\glTech.ePipemonitor.WSNSCADAPlugin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\hostfxr.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\hostpolicy.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.BoxIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Entypo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.EvaIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.FeatherIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.FontAwesome.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Ionicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.JamIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Material.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.MaterialDesign.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.MaterialLight.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Microns.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Modern.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Octicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.PicolIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.RPGAwesome.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.SimpleIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Typicons.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Unicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.WeatherIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\MahApps.Metro.IconPacks.Zondicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.Bcl.AsyncInterfaces.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.CSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.DiaSymReader.Native.amd64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.VisualBasic.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.VisualBasic.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.Win32.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.Win32.Registry.AccessControl.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.Win32.Registry.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.Win32.SystemEvents.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Microsoft.Xaml.Behaviors.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\mscordaccore.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\mscordaccore_amd64_amd64_4.700.20.20201.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\mscordbi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\mscorlib.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\mscorrc.debug.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\mscorrc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\netstandard.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\NLog.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\NLog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PenImc_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\Pipelines.Sockets.Unofficial.dll"; DestDir: "{app}"; Flags: ignoreversion


Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PluginContract.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationCore.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework.Aero.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework.Aero2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework.AeroLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework.Classic.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework.Luna.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework.Royale.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework-SystemCore.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework-SystemData.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework-SystemDrawing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework-SystemXml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationFramework-SystemXmlLinq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationNative_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\PresentationUI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\ReachFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\sni.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\SOS_README.md"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\StackExchange.Redis.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.AppContext.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.CodeDom.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Collections.Concurrent.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Collections.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Collections.Immutable.dll"; DestDir: "{app}"; Flags: ignoreversion



Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Collections.NonGeneric.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Collections.Specialized.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ComponentModel.Annotations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ComponentModel.DataAnnotations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ComponentModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ComponentModel.EventBasedAsync.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ComponentModel.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ComponentModel.TypeConverter.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Configuration.ConfigurationManager.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Configuration.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Console.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Data.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Data.DataSetExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Data.SqlClient.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Design.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.Contracts.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.Debug.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.DiagnosticSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.EventLog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.FileVersionInfo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.PerformanceCounter.dll"; DestDir: "{app}"; Flags: ignoreversion



Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.Process.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.StackTrace.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.TextWriterTraceListener.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.Tools.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.TraceSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Diagnostics.Tracing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.DirectoryServices.dll"; DestDir: "{app}"; Flags: ignoreversion


Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Drawing.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Drawing.Design.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Drawing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Drawing.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Dynamic.Runtime.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Globalization.Calendars.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Globalization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Globalization.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Compression.Brotli.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Compression.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Compression.FileSystem.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Compression.ZipFile.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.FileSystem.AccessControl.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.FileSystem.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.FileSystem.DriveInfo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.FileSystem.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.FileSystem.Watcher.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.IsolatedStorage.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.MemoryMappedFiles.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Packaging.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Pipelines.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Pipes.AccessControl.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Pipes.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.Ports.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.IO.UnmanagedMemoryStream.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Linq.Expressions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Linq.Parallel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Linq.Queryable.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.Http.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.HttpListener.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.Mail.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.NameResolution.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.NetworkInformation.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.Ping.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.Requests.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.ServicePoint.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.Sockets.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.WebClient.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.WebHeaderCollection.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.WebProxy.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.WebSockets.Client.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Net.WebSockets.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Numerics.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ObjectModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Printing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Private.CoreLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Private.DataContractSerialization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Private.Uri.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Private.Xml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Private.Xml.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.DispatchProxy.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.Emit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.Emit.ILGeneration.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.Emit.Lightweight.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.Metadata.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Reflection.TypeExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Resources.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Resources.Reader.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Resources.ResourceManager.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Resources.Writer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion


Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.CompilerServices.VisualC.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Handles.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.InteropServices.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.InteropServices.RuntimeInformation.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.InteropServices.WindowsRuntime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Intrinsics.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Loader.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Numerics.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Serialization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Serialization.Formatters.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Serialization.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Serialization.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.Serialization.Xml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.WindowsRuntime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Runtime.WindowsRuntime.UI.Xaml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.AccessControl.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Claims.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.Algorithms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.Cng.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.Csp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.Encoding.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.OpenSsl.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.Pkcs.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.ProtectedData.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.X509Certificates.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Cryptography.Xml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Permissions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Principal.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.Principal.Windows.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Security.SecureString.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ServiceModel.Web.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ServiceProcess.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Text.Encoding.CodePages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Text.Encoding.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Text.Encoding.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Text.Encodings.Web.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Text.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Text.RegularExpressions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.AccessControl.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Channels.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Overlapped.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Tasks.Dataflow.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Tasks.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Tasks.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Tasks.Parallel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Thread.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.ThreadPool.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Threading.Timer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Transactions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Transactions.Local.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.ValueTuple.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Web.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Web.HttpUtility.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.Controls.Ribbon.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.Forms.Design.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.Forms.Design.Editors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.Forms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.Input.Manipulations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Windows.Presentation.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xaml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.ReaderWriter.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.Serialization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.XDocument.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.XmlDocument.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.XmlSerializer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.XPath.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\System.Xml.XPath.XDocument.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\ucrtbase.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\UIAutomationClient.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\UIAutomationClientSideProviders.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\UIAutomationProvider.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\UIAutomationTypes.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\vcruntime140_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\WindowsBase.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\WindowsFormsIntegration.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\wpfgfx_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\cs\*"; DestDir: "{app}\cs"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\de\*"; DestDir: "{app}\de"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\es\*"; DestDir: "{app}\es"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\fr\*"; DestDir: "{app}\fr"; Flags: ignoreversion 

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\it\*"; DestDir: "{app}\it"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\ja\*"; DestDir: "{app}\ja"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\ko\*"; DestDir: "{app}\ko"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\pl\*"; DestDir: "{app}\pl"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\pt-BR\*"; DestDir: "{app}\pt-BR"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\ru\*"; DestDir: "{app}\ru"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\tr\*"; DestDir: "{app}\tr"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\zh-Hans\*"; DestDir: "{app}\zh-Hans"; Flags: ignoreversion 
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Publish\zh-Hant\*"; DestDir: "{app}\zh-Hant"; Flags: ignoreversion 






; 注意: 不要在任何共享系统文件上使用“Flags: ignoreversion”

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent


[UninstallRun] 
Filename: {sys}\taskkill.exe; Parameters: "/f /im glTech.ePipemonitor.WSNSCADA.exe"; Flags: skipifdoesntexist runhidden


[Code]
function GetUninstallString: string;
var
  sUnInstPath: string;
  sUnInstallString: String;
begin
  Result := '';
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{{91BFA1E7-853E-408A-973D-2A728AFD303F}_is1'); //Your App GUID/ID
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade: Boolean;
begin
  Result := (GetUninstallString() <> '');
end;

function InitializeSetup: Boolean;
var
  V: Integer;
  iResultCode: Integer;
  sUnInstallString: string;
begin
  Result := True; // in case when no previous version is found



  if RegValueExists(HKEY_LOCAL_MACHINE,'Software\Microsoft\Windows\CurrentVersion\Uninstall\{91BFA1E7-853E-408A-973D-2A728AFD303F}_is1', 'UninstallString') then  //Your App GUID/ID
  begin
    V := MsgBox(ExpandConstant('你好! 发现一个旧版本.是否要卸载它?'), mbInformation, MB_YESNO); //Custom Message if App installed
    if V = IDYES then
    begin
      sUnInstallString := GetUninstallString();
      sUnInstallString :=  RemoveQuotes(sUnInstallString);
      Exec(ExpandConstant(sUnInstallString), '', '', SW_SHOW, ewWaitUntilTerminated, iResultCode);
      Result := True; //if you want to proceed after uninstall
                //Exit; //if you want to quit after uninstall
    end
    else
      Result := False; //when older version present and not uninstalled
  end;
end;





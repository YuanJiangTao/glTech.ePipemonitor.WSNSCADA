; �ű��� Inno Setup �ű��� ���ɣ�
; �йش��� Inno Setup �ű��ļ�����ϸ��������İ����ĵ���

#define MyAppName "GPRS���������ݲɼ�"
#define MyAppVersion "4.3"
#define MyAppPublisher "֣�ݹ����Ƽ��ɷ����޹�˾"
#define MyAppURL "http://www.gltech.cn/"
#define MyAppExeName "glTech.ePipemonitor.WSNSCADA.exe"

[Setup]
; ע: AppId��ֵΪ������ʶ��Ӧ�ó���
; ��ҪΪ������װ����ʹ����ͬ��AppIdֵ��
; (�����µ�GUID����� ����|��IDE������GUID��)
AppId={{91BFA1E7-853E-408A-973D-2A728AFD303F}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName=D:\Program Files\֣�ݹ����Ƽ��ɷ����޹�˾\�����Ƽ�GPRS���������ݲɼ�
DefaultGroupName={#MyAppName}
OutputDir=output
OutputBaseFilename={#MyAppName}
Compression=lzma
SolidCompression=yes
DirExistsWarning=no
SetupIconFile="..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\icon.ico"
UninstallDisplayIcon="..\DataExchangeScaffold\if_stack_1287510.ico"


;��װж��ʱ�������Ƿ���������
AppMutex = {#MyAppName}

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Dapper.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\glTech.ePipemonitor.WSNSCADA.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\glTech.ePipemonitor.WSNSCADA.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\glTech.ePipemonitor.WSNSCADAPlugin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.BoxIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Entypo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.EvaIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.FeatherIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.FontAwesome.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Ionicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.JamIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Material.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.MaterialDesign.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.MaterialLight.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Microns.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Modern.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Octicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.PicolIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.RPGAwesome.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.SimpleIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Typicons.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Unicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.WeatherIcons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\MahApps.Metro.IconPacks.Zondicons.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Microsoft.Bcl.AsyncInterfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Microsoft.Xaml.Behaviors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Pipelines.Sockets.Unofficial.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\PluginContract.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\StackExchange.Redis.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\System.Data.SqlClient.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\System.IO.Pipelines.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\System.IO.Ports.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\System.Text.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\System.Threading.Channels.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\runtimes\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs    onlyifdoesntexist  uninsneveruninstall
Source: "..\..\glTech.ePipemonitor.WSNSCADA\glTech.ePipemonitor.WSNSCADA\bin\Release\netcoreapp3.1\Plugins\*"; DestDir: "{app}\Plugins"; Flags: ignoreversion recursesubdirs createallsubdirs    onlyifdoesntexist  uninsneveruninstall
; ע��: ��Ҫ���κι���ϵͳ�ļ���ʹ�á�Flags: ignoreversion��

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent


[UninstallRun] 
Filename: {sys}\taskkill.exe; Parameters: "/f /im glTech.ePipemonitor.WSNSCADA.exe"; Flags: skipifdoesntexist runhidden

[Code]

function CheckKb2468871():boolean;
begin
  Result := not (RegKeyExists(HKLM, 'SOFTWARE\WOW6432Node\Microsoft\Updates\Microsoft .NET Framework 4 Client Profile\KB2468871') or RegKeyExists(HKLM, 'SOFTWARE\Microsoft\Updates\Microsoft .NET Framework 4 Client Profile\KB2468871') );
end;

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

  if CheckKb2468871() then
  begin
    MsgBox(ExpandConstant('��鵽ϵͳû�а�װ.net����NDP40-KB2468871,�밲װ!'), mbInformation, MB_OK);
  end;


  if RegValueExists(HKEY_LOCAL_MACHINE,'Software\Microsoft\Windows\CurrentVersion\Uninstall\{91BFA1E7-853E-408A-973D-2A728AFD303F}_is1', 'UninstallString') then  //Your App GUID/ID
  begin
    V := MsgBox(ExpandConstant('���! ����һ���ɰ汾.�Ƿ�Ҫж����?'), mbInformation, MB_YESNO); //Custom Message if App installed
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



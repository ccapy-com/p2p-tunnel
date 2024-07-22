@echo off

SET sdkpath=%1
if "%sdkpath%"=="" (SET sdkpath="D:\\Android\\android-sdk")

rd /s /q public\\publish
rd /s /q public\\publish-zip
mkdir public\\publish-zip

set DOTNET_TieredPGO=1

rem �ͻ��˺ͷ����
for %%f in (client,server) do (
	for %%r in (win-x64,win-arm64,linux-x64,linux-arm64,osx-x64,osx-arm64) do (
		dotnet publish ./%%f/%%f.service -c release -f net7.0 -o ./public/publish/%%r-single/%%f  -r %%r  --self-contained true -p:TieredPGO=true  -p:DebugType=none -p:DebugSymbols=false  -p:PublishSingleFile=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true -p:DebuggerSupport=false -p:EnableUnsafeBinaryFormatterSerialization=false -p:EnableUnsafeUTF7Encoding=false -p:HttpActivityPropagationSupport=false -p:InvariantGlobalization=true  -p:MetadataUpdaterSupport=false  -p:UseSystemResourceKeys=true  -p:TrimMode=partial
		
		dotnet publish ./%%f/%%f.service -c release -f net7.0 -r %%r -o ./public/publish/any/%%f/%%r  -p:PublishSingleFile=true --self-contained false
	)
	rem dotnet publish ./%%f/%%f.service -c release -f net7.0 -o ./public/publish/any/%%f -p:PublishSingleFile=true --self-contained false
)
rem app ��Ϊ�Լ���Android sdk��ַ, ������ ����->ѡ��->Xamarin->Android���� ��鿴sdk��ַ
rem dotnet publish ./client/client.service.app -c:Release -f:net7.0-android /p:AndroidSigningKeyPass=123321 /p:AndroidSdkDirectory=%sdkpath%
rem echo F|xcopy "client\\client.service.app\\bin\\Release\\net7.0-android\\publish\\p2p_tunnel.p2p_tunnel-Signed.apk" "public\\publish-zip\\p2p-tunnel.apk"  /s /f /h /y


for %%r in (x64,arm64) do (
	for %%f in (tun2socks-linux,tun2socks-osx,tun2socks-windows.exe) do (
		del  "public\\publish\\win-%%r-single\\client\\%%f"
		del  "public\\publish\\osx-%%r-single\\client\\%%f"
		del  "public\\publish\\linux-%%r-single\\client\\%%f"

		del  "public\\publish\\any\\client\\linux-%%r\\%%f"
		del  "public\\publish\\any\\client\\osx-%%r\\%%f"
		del  "public\\publish\\any\\client\\win-%%r\\%%f"
	)
	for %%f in (wintun.dll) do (
		del  "public\\publish\\linux-%%r-single\\client\\%%f"
		del  "public\\publish\\osx-%%r-single\\client\\%%f"
		del  "public\\publish\\any\\client\\linux-%%r\\%%f"
		del  "public\\publish\\any\\client\\osx-%%r\\%%f"
		del  "public\\publish\\any\\client\\win-%%r\\%%f"
	)
	for %%p in (win,linux,osx) do (
		del  "public\\publish\\%%p-%%r-single\\server\\*.pac"
	)
)


for %%r in (x64,arm64) do (
	echo F|xcopy "client\\plugins\\client.service.vea\\tun2socks-linux-%%r" "public\\publish\\linux-%%r-single\\client\\tun2socks-linux"  /f /h /y
	echo F|xcopy "client\\plugins\\client.service.vea\\tun2socks-osx-%%r" "public\\publish\\osx-%%r-single\\client\\tun2socks-osx"  /f /h /y
	echo F|xcopy "client\\plugins\\client.service.vea\\tun2socks-windows.exe-%%r" "public\\publish\\win-%%r-single\\client\\tun2socks-windows.exe"  /f /h /y
	echo F|xcopy "client\\plugins\\client.service.vea\\wintun.dll" "public\\publish\\win-%%r-single\\client\\wintun.dll"  /f /h /y

	echo F|xcopy "client\\plugins\\client.service.vea\\tun2socks-linux-%%r" "public\\publish\\any\\client\\linux-%%r\\tun2socks-linux"  /f /h /y
	echo F|xcopy "client\\plugins\\client.service.vea\\tun2socks-osx-%%r" "public\\publish\\any\\client\\osx-%%r\\tun2socks-osx"  /f /h /y
	echo F|xcopy "client\\plugins\\client.service.vea\\tun2socks-windows.exe-%%r" "public\\publish\\any\\client\\win-%%r\\tun2socks-windows.exe"  /f /h /y
	echo F|xcopy "client\\plugins\\client.service.vea\\wintun.dll" "public\\publish\\any\\client\\win-%%r\\wintun.dll"  /f /h /y

	for %%p in (win,linux,osx) do (
		echo D|xcopy "client\\plugins\\client.service.ui\\client.service.ui.api.service\\public\\web\\" "public\\publish\\%%p-%%r-single\\client\\public\\web\\" /s /f /h /y
	)

	echo F|xcopy "client\\plugins\\client.service.ui\\client.service.ui.api.service\\public\\client.service.tray.exe" "public\\publish\\win-%%r-single\\client\\client.service.tray.exe"  /s /f /h /y
	echo F|xcopy "client\\plugins\\client.service.ui\\client.service.ui.api.service\\public\\client.service.tray.exe" "public\\publish\\any\\client\\win-%%r\\client.service.tray.exe"  /s /f /h /y
	echo F|xcopy "server\\server.service\\public\\server.service.tray.exe" "public\\publish\\win-%%r-single\\server\\server.service.tray.exe"  /s /f /h /y
	echo F|xcopy "server\\server.service\\public\\server.service.tray.exe" "public\\publish\\any\\server\\win-%%r\\server.service.tray.exe"  /s /f /h /y
	
)
del  "public\\publish\\any\\server\\*.pac" 
 
7z a -tzip ./public/publish-zip/p2p-tunnel-any.zip ./public/publish/any/*
7z a -tzip ./public/publish-zip/p2p-tunnel-win-x64-single.zip ./public/publish/win-x64-single/*
7z a -tzip ./public/publish-zip/p2p-tunnel-win-arm64-single.zip ./public/publish/win-arm64-single/*
7z a -tzip ./public/publish-zip/p2p-tunnel-linux-x64-single.zip ./public/publish/linux-x64-single/*
7z a -tzip ./public/publish-zip/p2p-tunnel-linux-arm64-single.zip ./public/publish/linux-arm64-single/*
7z a -tzip ./public/publish-zip/p2p-tunnel-osx-x64-single.zip ./public/publish/osx-x64-single/*
7z a -tzip ./public/publish-zip/p2p-tunnel-osx-arm64-single.zip ./public/publish/osx-arm64-single/*


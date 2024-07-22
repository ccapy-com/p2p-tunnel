@echo off
SET target=%~dp0

SET image=%1
if "%image%"=="" (SET image="snltty/p2p-tunnel")


for %%f in (client,server) do (
	for %%p in (alpine,debian) do (
		for %%r in (x64,arm64) do (
			dotnet publish ./%%f/%%f.service -c release -f net7.0 -o ./public/publish/linux-%%p-%%r/%%f  -r %%p-%%r  --self-contained true -p:TieredPGO=true  -p:DebugType=none -p:DebugSymbols=false  -p:PublishSingleFile=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true -p:DebuggerSupport=false -p:EnableUnsafeBinaryFormatterSerialization=false -p:EnableUnsafeUTF7Encoding=false -p:HttpActivityPropagationSupport=false -p:InvariantGlobalization=true  -p:MetadataUpdaterSupport=false  -p:UseSystemResourceKeys=true  -p:TrimMode=partial
			move "public\\publish\\linux-%%p-%%r\\%%f\\%%f.service" "public\\publish\\linux-%%p-%%r\\%%f\\service.run"
			echo F|xcopy "client\\client.service\\Dockerfile-%%p-%%f" "public\\publish\\linux-%%p-%%r\\%%f\\Dockerfile-%%p-%%f"  /s /f /h /y
			echo F|xcopy "client\\plugins\\client.service.vea\\tun2socks-linux-%%r" "public\\publish\\linux-%%p-%%r\\%%f\\tun2socks-linux"  /f /h /y
			del  "public\\publish\\linux-%%p-%%r\\%%f\\tun2socks-osx"
			del  "public\\publish\\linux-%%p-%%r\\%%f\\tun2socks-windows.exe"
			del  "public\\publish\\linux-%%p-%%r\\%%f\\wintun.dll"
		)

		cd public/publish/linux-%%p-x64/%%f
		docker buildx build -f "%target%\\public\\publish\\linux-%%p-x64\\%%f\\Dockerfile-%%p-%%f" --platform="linux/x86_64"  --force-rm -t "%image%-%%f-%%p-x64" . --push
		cd ../../../../

		cd public/publish/linux-%%p-arm64/%%f
		docker buildx build -f "%target%\\public\\publish\\linux-%%p-arm64\\%%f\\Dockerfile-%%p-%%f" --platform="linux/arm64"  --force-rm -t "%image%-%%f-%%p-arm64" . --push
		cd ../../../../
	)
)
@ECHO OFF
cd /d %~dp0
echo 开始安装...
dotnet SFBR.Device.Api.dll install
ECHO.
ECHO 安装完成正在启动服务...
dotnet SFBR.Device.Api.dll start
ECHO.
ECHO 安装完成
pause
@ECHO OFF
cd /d %~dp0
echo 正在停止服务...
dotnet SFBR.Repair.Api.dll stop
echo.
echo 服务停止开始卸载...
dotnet SFBR.Repair.Api.dll uninstall
echo.
ECHO 卸载完成
pause
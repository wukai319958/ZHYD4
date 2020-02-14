@ECHO OFF
cd /d %~dp0
echo 正在停止服务...
dotnet SFBR.Terminal.Web.dll stop
echo.
echo 服务停止开始卸载...
dotnet SFBR.Terminal.Web.dll uninstall
echo.
ECHO 卸载完成
pause
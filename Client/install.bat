rem need change run interval
rem change server ip
set ip=192.168.3.100
rem need change topic name
set topic=Inventory
rem need to change install directory
set InstallDIR=c:\inventory
if not exist %InstallDIR% (md %InstallDIR%) else (echo already exist folder)
copy *.exe %InstallDIR%
copy *.dll %InstallDIR%
schtasks /create /tn Inventory /tr "%InstallDIR%\InventoryClient.exe %ip% %topic%" /sc MINUTE /MO 30 /F
pause
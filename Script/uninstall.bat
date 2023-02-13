@schtasks /query /tn Inventory && schtasks /delete /tn Inventory /F || echo system not exist such task.

@if exist c:\inventory (rd c:\inventory /Q/S) else (echo folder already remove)

pause

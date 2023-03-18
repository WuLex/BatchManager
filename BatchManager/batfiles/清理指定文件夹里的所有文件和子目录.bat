REM cd D:\ProjectsTemp\
REM del * /S /Q "D:\ProjectsTemp\"
REM rmdir /S /Q "D:\ProjectsTemp\"

cd /d D:\ProjectsTemp\
del /f /s /q *
for /d %%d in (*) do rmdir /s /q "%%d"
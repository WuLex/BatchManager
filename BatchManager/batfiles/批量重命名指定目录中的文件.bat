

@echo off

setlocal EnableDelayedExpansion

#将 SOURCE_DIR 变量设置为要重命名的目录
set SOURCE_DIR=D:\bat\
#set DEST_DIR=D:\bat\pdfs
set PREFIX=photo_
set COUNTER=1

#用 for 命令来遍历源目录中的所有文件（不包括子目录中的文件）
REM for %%f in ("%SOURCE_DIR%\*.*") do (
    REM set "filename=%%~nf"
    REM set "ext=%%~xf"
    REM #使用 ren 命令将文件重命名为 PREFIX+COUNTER+ext 的格式
    REM ren "%%f" "!PREFIX!!COUNTER!!ext!"
    REM set /a COUNTER+=1
REM )

#使用 for /r 命令来遍历 SOURCE_DIR 目录及其所有子目录中的所有文件
for /r "%SOURCE_DIR%" %%f in (*) do (
    set "filename=%%~nf"
    set "ext=%%~xf"
    ren "%%f" "!PREFIX!!COUNTER!!ext!"
    set /a COUNTER+=1
)
echo Renaming completed successfully.
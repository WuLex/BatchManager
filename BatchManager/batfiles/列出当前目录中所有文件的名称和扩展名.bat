
#用于关闭脚本的命令行回显功能，以使输出更清晰。
@echo off
#启用延迟环境变量扩展，以便在循环中使用!作为变量的标记符号。
setlocal EnableDelayedExpansion

set FILELIST=
#循环遍历当前目录中的所有文件
for %%f in (*) do (
    #%%~nf 和 %%~xf 分别表示文件名和扩展名，它们用于将文件名和扩展名分别保存到变量中
    set FILENAME=%%~nf
    set EXTENSION=%%~xf
    #用于将每个文件的名称和扩展名添加到 FILELIST 变量中
    set FILELIST=!FILELIST! !FILENAME!!EXTENSION!
)

for %%i in (%FILELIST%) do (
    echo %%i >> filelist.txt
)

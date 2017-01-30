@echo off
SET script_dir=%~dp0
if EXIST %script_dir%alias\%1.bat (
	del "%script_dir%alias\%1.*"
	echo alias %1 removed
) ELSE (
	echo %1 alias not found
)
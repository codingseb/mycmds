@echo off
set /p response=Warning this is going to empty permanently the current directory, are you sure to continue (y/n): 

if not "%response%" == "y" goto :end

rm -d -r -f %* *

:end

SET response=
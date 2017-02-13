@echo off
SET script_dir=%~dp0
cscs %script_dir%json.cs %* | tail -n +4

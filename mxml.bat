@echo off
SET script_dir=%~dp0
cscs %script_dir%mxml.cs %* | tail -n +4

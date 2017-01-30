@echo off

set q=%*

call set q=%%q: =+%%

@start https://www.google.ch?q=%q%^&gws_rd=ssl

echo on

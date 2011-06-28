makecab.exe  /F solution/solution.ddf
@IF ERRORLEVEL 1 GOTO FINE
@ECHO =================================
@ECHO wsp  generato
@ECHO =================================
setup.exe
:FINE


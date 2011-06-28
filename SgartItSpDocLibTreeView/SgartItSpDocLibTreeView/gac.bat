@echo off

SET DLLNAME=SgartIt.Sp.DocLibTreeView
SET OUTDIR=DLL
SET KEYFILE=Properties\Temporary.snk
SET PAGE=http://microsys-moss:80
SET APPPOOL=SharePoint - 80
SET SHAREPOINTDLL=%CommonProgramFiles%\Microsoft Shared\web server extensions\12\ISAPI\Microsoft.SharePoint.dll

ECHO .
ECHO .
ECHO .
ECHO .
ECHO .
time /T
ECHO ***************************************
ECHO DLL: %DLLNAME%
ECHO ***************************************

IF "%1"== "/r" GOTO RECICLE
IF "%1"== "/R" GOTO RECICLE

IF "%1"== "/g" GOTO GAC
IF "%1"== "/g" GOTO GAC


ECHO * compiling
"%WINDIR%\Microsoft.NET\FrameWork\v2.0.50727\csc.exe" /target:library /out:"%OUTDIR%\%DLLNAME%.dll" /recurse:*.cs /warn:0 /keyfile:"%KEYFILE%" /debug:full /pdb:"%OUTDIR%\%DLLNAME%.pdb" /r:"%SHAREPOINTDLL%"
IF NOT %ERRORLEVEL%==0 GOTO ERRORCSC

:GAC

ECHO * add to GAC
"%ProgramFiles%\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe" /i "%OUTDIR%\%DLLNAME%.dll" /f
IF NOT %ERRORLEVEL%==0 GOTO ERRORGAC

:RECICLE

ECHO * recicle AppPool
iisapp.vbs /a "%APPPOOL%" /r
IF NOT %ERRORLEVEL%==0 GOTO ERRORAPP

ECHO * open page
start %PAGE%

ECHO * current AppPool
iisapp.vbs /a "%APPPOOL%"

ECHO * OK

GOTO FINE 

:ERRORCSC
ECHO ***************************************************
ECHO ERRORE NON COMPILATO
GOTO FINE 

:ERRORGAC
ECHO ***************************************************
ECHO ERRORE NON copiato nella gac
GOTO FINE 

:ERRORAPP
ECHO ***************************************************
ECHO ERRORE application pool (%APPPOOL%) non trovato 
GOTO FINE 

:FINE



@ECHO OFF

@ docfx -h
IF %errorlevel% NEQ 0 GOTO ERROR 
GOTO RUN

:ERROR
ECHO Docfx is not installed! 
ECHO Please see https://dotnet.github.io/docfx .
GOTO END

:RUN
ECHO Docfx is starting...
docfx docfx_local.json --serve
GOTO END

:END
@ECHO ON

@echo off
cls
set exepath = Brubeck\bin\Debug\net5.0-windows\Brubeck.exe
goto start

:start
echo -a  Assemble Mode
echo -r  Run Mode
echo -c  Clear Screen
echo -x  Exit
set /p mode=Mode (-?):
if '%mode%'=='-a' goto assemble
if '%mode%'=='-r' goto run
if '%mode%'=='-c' goto clear
if '%mode%'=='-x' goto leave
echo Mode unrecognised, try again.
goto start

:assemble
set /p filename=Filename to assemble (.brbkasm, do not include extension): 
set /p verbose=Type 'Y' to run verbose:
if '%verbose%'=='Y' goto verbose

%exepath% -a %filename%.brbkasm
goto start
:verbose
%exepath% -a %filename%.brbkasm -v
goto start

:run
set /p filename=Filename to run (.brbk5, do not include extension):
%exepath% -r %filename%.brbk5
goto start

:clear
cls
goto start

:leave
pause
exit
@echo off
xcopy "%1" "%2" /E /I /Y 
rem /E g�r at alle undermapper bliver kopieret, inklusive tomme mapper.
rem /I g�r at hvis destinationsmappen ikke findes, skal den oprettes, i dette tilf�lde opretter den backupmappen
rem /Y g�r at hvis en eller flere filer eller mappe allerede eksistere, overskrives de i destinationsmappen. 

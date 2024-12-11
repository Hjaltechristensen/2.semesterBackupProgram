@echo off
xcopy "%1" "%2" /E /I /Y 
rem /E gør at alle undermapper bliver kopieret, inklusive tomme mapper.
rem /I gør at hvis destinationsmappen ikke findes, skal den oprettes, i dette tilfælde opretter den backupmappen
rem /Y gør at hvis en eller flere filer eller mappe allerede eksistere, overskrives de i destinationsmappen. 

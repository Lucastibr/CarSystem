@ECHO OFF

:choice
set /P c=Deseja Executar Migrator em Lucas Home Office? [S/N]?
if /I "%c%" EQU "S" goto :somewhere
if /I "%c%" EQU "N" goto :somewhere_else
goto :choice

:somewhere
echo "Executando Migrator..."
dotnet fm migrate -p sqlserver -c "Server=.\BDLUCAS;Database=CarSystem;Trusted_Connection=True;MultipleActiveResultSets=true;Pooling=True;Max Pool Size=1024;Connection Lifetime=3600" -a ".\bin\Debug\netstandard2.0\CarSystem.Migrations.dll"
pause 
exit

:somewhere_else
echo "Migrator nao foi excecutado!"
pause 
exit
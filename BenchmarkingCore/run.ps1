dotnet publish -c release -r win-x64 /p:PublishSingleFile=true

Write-Output "Net Core 3.1 Single File Executable"
Measure-Command { .\bin\release\netcoreapp3.1\win-x64\publish\BenchmarkingCore.exe }

Write-Output "Net Core 3.1 Non-Single File Executable"
Measure-Command { .\bin\release\netcoreapp3.1\win-x64\BenchmarkingCore.exe }

Write-Output ""

# publish project for dotnet core 3.1 for generic windows release
dotnet publish -c release -r win-x64 -f netcoreapp3.1

# publish project for dotnet core 3.1 for windows 10 specific release
dotnet publish -c release -r win10-x64 -f netcoreapp3.1

# publish project for dotnet framework 4.8 for generic windows release
dotnet publish -c release -r win-x64 -f net48
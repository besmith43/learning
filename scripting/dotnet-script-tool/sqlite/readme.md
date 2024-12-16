# Sqlite from Dotnet Script

I have no idea why, but the microsoft.Data.Sqlite failed to include the libe_sqlite3.dylib file in the path of the dotnet script binary.
So I copied it from a temp csproj based project into the /usr/local/share/dotnet/shared/Microsoft.NETCore.App/9.0.0/ where it was looking for the .dylib.





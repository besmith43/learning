[CmdletBinding()]
Param(

)

# run cake publish

dotnet cake --target=publish

remove-item -Path .\.publish\appsettings*json -Recurse -Force

# get 2 account creds
$cred = Get-Credential

# create pssession with portappd01.tntech.edu

$session = New-PSSession -ComputerName portappd01.tntech.edu -credential $cred -ErrorAction Stop

# stop app pool in IIS

Invoke-Command -Session $session -ScriptBlock { Stop-WebAppPool -Name "new-express" }

# create psdrive to portappd01.tntech.edu

new-psdrive ExpressAppDev -root \\portappd01\d$ -PSProvider FileSystem -Credential $cred -ErrorAction Stop

# copy files to C:\scripts\EABAttendanceUpdater\ on portappd01.tntech.edu

Copy-Item -Path .\.publish\Express\* -Destination ExpressAppDev:\inetpub\wwwroot\new-express\

# start app pool in IIS

Invoke-Command -Session $session -ScriptBlock { Start-WebAppPool -Name "new-express" }

# close pssession

Remove-PSSession -Session $session

# remove psdrive

Remove-PSDrive -Name ExpressAppDev

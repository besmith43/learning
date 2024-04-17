param (
    [Parameter(Mandatory=$true)]
    [string] $executable
)

[Convert]::ToBase64String([IO.File]::ReadAllBytes("$PSScriptRoot\$executable")) | Out-File "$PSScriptRoot\$executable.txt"


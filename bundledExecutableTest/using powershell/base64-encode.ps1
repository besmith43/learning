param (
[string]$message=""
)

[Convert]::ToBase64String([System.Text.Encoding]::Unicode.GetBytes($message))

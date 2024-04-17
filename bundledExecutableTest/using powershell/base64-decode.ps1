param (
[string]$message=""
)

[System.Text.Encoding]::Unicode.GetString([System.Convert]::FromBase64String($message))

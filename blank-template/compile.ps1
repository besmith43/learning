if ((Test-Path ".\less") -and (Test-Path ".\css"))
{
    if (Get-Command lessc -ErrorAction SilentlyContinue)
    {
        lessc .\less\main.less .\css\main.css
    }
}

if ((Test-Path ".\ts") -and (Test-Path ".\js"))
{
    if (Get-Command tsc -ErrorAction SilentlyContinue)
    {
        tsc --outfile .\js\main.js .\ts\main.ts
    }
}
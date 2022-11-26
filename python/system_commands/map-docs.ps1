try
{
	net use u: \\10.0.1.2\Docs /persistent:yes /USER:besmith crap4m32
}
catch
{
	write-host "Mapping Network drive failed" --foregroundcolor "Red"
}

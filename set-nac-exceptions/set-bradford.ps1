$num_mac_addresses = 0

try
{
    $eth = Get-NetAdapter | Where-Object { $_.name -eq "Ethernet" } | select MacAddress
	$eth = $eth -replace "-",":"
	$mac = out-string -InputObject $eth
	$eth = $mac.Substring(13,17)
	$num_mac_addresses = $num_mac_addresses + 1
}
catch
{
	$eth = "0"
}

try
{
    $wifi = Get-NetAdapter | Where-Object { $_.name -Match "Wi-Fi" } | select MacAddress
	$wifi = $wifi -replace "-",":"
	$wifi = out-string -InputObject $wifi
	$wifi = $wifi.Substring(13,17)
	$num_mac_addresses = $num_mac_addresses + 1
}
catch
{
	$wifi = "0"
}	

$hostname = hostname
$hostname = $hostname.ToUpper()
$hostname_length = $hostname.length
$location = $hostname.substring(0,$hostname_length-4)

$legal_Stuff = "adap.mac,siblings,host.host,adap.loc,host.devType,host.expireDate,host.inact"

if ($num_mac_addresses -eq 2)
{
    $comp_info = "$eth,$wifi,$hostname,$location,Windows,Never,1825 Days`n$wifi,$eth,$hostname,$location,Windows,Never,1825 Days"
}
elseif ($num_mac_addresses -eq 1 -and $eth -ne "0")
{
    $comp_info = "$eth,$eth,$hostname,$location,Windows,Never,1825 Days"
}
elseif ($num_mac_addresses -eq 1 -and $wifi -ne "0")
{
    $comp_info = "$wifi,$wifi,$hostname,$location,Windows,Never,1825 Days"
}
else
{
    echo "Failed"
    exit
}

$final_content = "$legal_stuff`n$comp_info"

$pwd = pwd

$date = get-date -format o
$date = $date.Substring(0,10)
$month = $date.Substring(5,2)
$day = $date.Substring(8,2)
$year = $date.Substring(0,4)
$date_final = "$month$day$year"

Set-Content "$pwd\$date_final-$hostname.csv" $final_content

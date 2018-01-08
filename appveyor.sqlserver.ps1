[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo") | Out-Null
[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.SqlWmiManagement") | Out-Null

$instancename = $args[0];
$computerName = $env:COMPUTERNAME
$smo = 'Microsoft.SqlServer.Management.Smo.'
$wmi = New-Object ($smo + 'Wmi.ManagedComputer')

#tcp
$uri = "ManagedComputer[@Name='$computerName']/ServerInstance[@Name='$instancename']/ServerProtocol[@Name='Tcp']"
$Tcp = $wmi.GetSmoObject($uri)
foreach ($ipAddress in $Tcp.IPAddresses)
{
    $ipAddress.IPAddressProperties["TcpDynamicPorts"].Value = ""
    $ipAddress.IPAddressProperties["TcpPort"].Value = "1433"
}
$Tcp.IsEnabled = $true
$Tcp.Alter()
$Tcp

#named pipes
# $uri = "ManagedComputer[@Name='$serverName']/ ServerInstance[@Name='$instanceName']/ServerProtocol[@Name='Np']"
# $Np = $wmi.GetSmoObject($uri)
# $Np.IsEnabled = $true
# $Np.Alter()
# $Np

# Set Alias
New-Item HKLM:\SOFTWARE\Microsoft\MSSQLServer\Client -Name ConnectTo | Out-Null
Set-ItemProperty -Path HKLM:\SOFTWARE\Microsoft\MSSQLServer\Client\ConnectTo -Name '(local)' -Value "DBMSSOCN,$serverName\$instanceName" | Out-Null

Stop-Service SQLBrowser
Stop-Service "MSSQL`$$instancename"

# Start services
Set-Service SQLBrowser -StartupType Manual
Start-Service SQLBrowser
Start-Service "MSSQL`$$instancename"
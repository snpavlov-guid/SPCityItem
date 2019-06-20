#
# Install solution
#

param(
[switch]$uninstall)

$computer = get-content env:computername

$packageName = "CityList.wsp"
$packagePath = $pwd.Path.TrimEnd('\') + "\" + $packageName

Write-Host "Computer name: $computer"
Write-Host "Solution Path: $packagePath"

# Make sure, that SharePoint Cmdlets are loaded
if ( (Get-PSSnapin -Name Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue) -eq $null )
{
    Add-PsSnapin Microsoft.SharePoint.PowerShell
}
# ErrorActionPreference
# SilentlyContinue	0	Will continue processing without notifying the user that an action has occurred.
# Stop 				1	Will stop processing when an action occurs.
# Continue			2	Will continue processing and notify the user that an action has occurred.
# Inquire			3	Will stop processing and ask the user how it should proceed.
$ErrorActionPreference = 1


### loop whilst there is a Job attached to the Solution 
function WaitForJob($sln, $timeout = 5)
{
	while ($sln.JobExists -eq $True) 
	{ 
		$currentStatus = $Solution.JobStatus 
		if ($currentStatus -ne $lastStatus) 
		{  Write-Host "$currentStatus…" -foreground green -nonewline 
		   $lastStatus = $currentStatus 
		} 
		Write-Host "." -foreground green -nonewline 
		start-sleep -s $timeout 
	} 
}

### Unistall current solution
if ($uninstall)
{
	$sln = get-spsolution -identity $packageName

	Write-Output "Started solution retraction..." 
	Uninstall-SPSolution -Identity $packageName -Confirm:$False 

	WaitForJob($sln)

	Write-Output ""
	Write-Output "Remove solution"
	Remove-SPSolution -Identity $packageName -Confirm:$False 
}


### Deploy new solution

Write-Output  "Add new solution package"
Add-SPSolution -LiteralPath $packagePath

$sln = get-spsolution -identity $packageName

Write-Output ""
Write-Output  "Started solution installation..."
Install-SPSolution -Identity $packageName -GACDeployment -force

WaitForJob($sln)

Write-Output ""

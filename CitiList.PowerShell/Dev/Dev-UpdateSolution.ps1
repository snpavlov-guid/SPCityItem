#
# Update Solution
#

Param(
[string]$packagePath)

# Make sure, that SharePoint Cmdlets are loaded
if ((Get-PSSnapin -Name Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue) -eq $null )
{
    Add-PsSnapin Microsoft.SharePoint.PowerShell
}

$solution = "CityList.wsp"

[string]$dir = PWD

if ([String]::IsNullOrEmpty($packagePath) -eq $true) {
	$packagePath = $dir.TrimEnd('\') + "\" + $solution
}

$sln = get-spsolution -identity $solution

Write-Output "Updating the solution $($solution)..."

Update-SPSolution -Identity $solution -LiteralPath $packagePath -GacDeployment -Force

## loop whilst there is a Job attached to the Solution 
while ($sln.JobExists -eq $True) 
{ 
    $currentStatus = $Solution.JobStatus 
    if ($currentStatus -ne $lastStatus) 
    {  Write-Host "$currentStatus…" -foreground green -nonewline 
       $lastStatus = $currentStatus 
    } 
    Write-Host "." -foreground green -nonewline 
    sleep 1 
} 
Write-Output "`n"





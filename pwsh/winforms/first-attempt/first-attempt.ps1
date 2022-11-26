param (

)

function Resized() {
    Write-Host "Window Resized"
    Write-Host "Width: $($Form.Width)"
    Write-Host "Height: $($Form.Height)"
}

function Resize_Button() {
    
}

Add-Type -AssemblyName System.Windows.Forms
[System.Windows.Forms.Application]::EnableVisualStyles()
$Form = New-Object system.Windows.Forms.Form
$Form.Text = "Working with WinForms"
$Form.Width = 350
$Form.Height = 550
$Form.AutoScroll = $True
$Form.AutoScaleMode = "Dpi"
$Form.Add_Resize({ Resized })

$GeneralFont = New-Object System.Drawing.Font("Times New Roman",12,[System.Drawing.FontStyle]::Regular)
$LabelFont = New-Object System.Drawing.Font("Times New Roman",12,[System.Drawing.FontStyle]::Underline)

# Sizing Variables

$ButtonWidth = 250
$ButtonHeight = 25

$DistanceTop = 35

$Win10Left = 50

# Windows 10 Section

$Win10Label = new-Object System.Windows.Forms.Label
$Win10Label.Text = "Windows 10"
$Win10Label.AutoSize = $True
$Win10Label.Font = $LabelFont
$Win10Label.Top = 10
$Win10Label.Left = 150

$Win10AddDomain = new-Object System.Windows.Forms.Button
$Win10AddDomain.add_click({& "$PSScriptRoot\Win10\add-domain\AddComp2Domain_Work.ps1"})
$Win10AddDomain.Text = "Add Computer to Domain"
$Win10AddDomain.Width = $ButtonWidth
$Win10AddDomain.Height = $ButtonHeight
$Win10AddDomain.Left = $Win10Left
$Win10AddDomain.Top = $Win10Label.Top + $DistanceTop

$Win10Bradford = new-Object System.Windows.Forms.Button
$Win10Bradford.add_click({& "$PSScriptRoot\Win10\bradford\set-bradford.ps1"})
$Win10Bradford.Text = "Create Bradford CSV"
$Win10Bradford.Width = $ButtonWidth
$Win10Bradford.Height = $ButtonHeight
$Win10Bradford.Left = $Win10Left
$Win10Bradford.Top = $Win10AddDomain.Top + $DistanceTop

$Win10CompInfo = new-Object System.Windows.Forms.Button
$Win10CompInfo.add_click({& "$PSScriptRoot\Win10\add-domain\AddComp2Domain_Work.ps1"})
$Win10CompInfo.Text = "Get Computer Hardware Info"
$Win10CompInfo.Width = $ButtonWidth
$Win10CompInfo.Height = $ButtonHeight
$Win10CompInfo.Left = $Win10Left
$Win10CompInfo.Top = $Win10Bradford.Top + $DistanceTop

$Win10MapDeptShare = new-Object System.Windows.Forms.Button
$Win10MapDeptShare.add_click({& "$PSScriptRoot\Win10\map-dept-share\get-deptshare.ps1"})
$Win10MapDeptShare.Text = "Map Departmental Share"
$Win10MapDeptShare.Width = $ButtonWidth
$Win10MapDeptShare.Height = $ButtonHeight
$Win10MapDeptShare.Left = $Win10Left
$Win10MapDeptShare.Top = $Win10CompInfo.Top + $DistanceTop

$Win10MapJake = new-Object System.Windows.Forms.Button
$Win10MapJake.add_click({& "$PSScriptRoot\Win10\map-Jake\get-jake.ps1"})
$Win10MapJake.Text = "Map Jake"
$Win10MapJake.Width = $ButtonWidth
$Win10MapJake.Height = $ButtonHeight
$Win10MapJake.Left = $Win10Left
$Win10MapJake.Top = $Win10MapDeptShare.Top + $DistanceTop

$Win10MapUserShare = new-Object System.Windows.Forms.Button
$Win10MapUserShare.add_click({& "$PSScriptRoot\Win10\map-user-share\get-usershare.ps1"})
$Win10MapUserShare.Text = "Map Usershare"
$Win10MapUserShare.Width = $ButtonWidth
$Win10MapUserShare.Height = $ButtonHeight
$Win10MapUserShare.Left = $Win10Left
$Win10MapUserShare.Top = $Win10MapJake.Top + $DistanceTop

$Win10NewComp = new-Object System.Windows.Forms.Button
$Win10NewComp.add_click({& "$PSScriptRoot\Win10\new-comp\setup-new-comp.ps1"})
$Win10NewComp.Text = "First Time Setup"
$Win10NewComp.Width = $ButtonWidth
$Win10NewComp.Height = $ButtonHeight
$Win10NewComp.Left = $Win10Left
$Win10NewComp.Top = $Win10MapUserShare.Top + $DistanceTop

$Win10EnableRemoteDesktop = new-Object System.Windows.Forms.Button
$Win10EnableRemoteDesktop.add_click({& "$PSScriptRoot\Win10\remote-desktop\set-remote.ps1"})
$Win10EnableRemoteDesktop.Text = "Enable Remote Desktop"
$Win10EnableRemoteDesktop.Width = $ButtonWidth
$Win10EnableRemoteDesktop.Height = $ButtonHeight
$Win10EnableRemoteDesktop.Left = $Win10Left
$Win10EnableRemoteDesktop.Top = $Win10NewComp.Top + $DistanceTop

$Win10RemoveDomain = new-Object System.Windows.Forms.Button
$Win10RemoveDomain.add_click({& "$PSScriptRoot\Win10\remove-domain\remove-adcomp.ps1"})
$Win10RemoveDomain.Text = "Remove Computer from Domain"
$Win10RemoveDomain.Width = $ButtonWidth
$Win10RemoveDomain.Height = $ButtonHeight
$Win10RemoveDomain.Left = $Win10Left
$Win10RemoveDomain.Top = $Win10EnableRemoteDesktop.Top + $DistanceTop

$Win10RenameComp = new-Object System.Windows.Forms.Button
$Win10RenameComp.add_click({& "$PSScriptRoot\Win10\rename-comp\rename-adcomp.ps1"})
$Win10RenameComp.Text = "Rename Computer"
$Win10RenameComp.Width = $ButtonWidth
$Win10RenameComp.Height = $ButtonHeight
$Win10RenameComp.Left = $Win10Left
$Win10RenameComp.Top = $Win10RemoveDomain.Top + $DistanceTop

$Win10AddTTUDevicesWifi = new-Object System.Windows.Forms.Button
$Win10AddTTUDevicesWifi.add_click({& "$PSScriptRoot\Win10\TTU-devices\Add-Wifi.ps1"})
$Win10AddTTUDevicesWifi.Text = "Add Computer to TTU Devices"
$Win10AddTTUDevicesWifi.Width = $ButtonWidth
$Win10AddTTUDevicesWifi.Height = $ButtonHeight
$Win10AddTTUDevicesWifi.Left = $Win10Left
$Win10AddTTUDevicesWifi.Top = $Win10RenameComp.Top + $DistanceTop

$Win10UpdateWindows = new-Object System.Windows.Forms.Button
$Win10UpdateWindows.add_click({& "$PSScriptRoot\Win10\update-win\PS_WinUpdate.ps1"})
$Win10UpdateWindows.Text = "Update Windows"
$Win10UpdateWindows.Width = $ButtonWidth
$Win10UpdateWindows.Height = $ButtonHeight
$Win10UpdateWindows.Left = $Win10Left
$Win10UpdateWindows.Top = $Win10AddTTUDevicesWifi.Top + $DistanceTop



$Form.Font = $GeneralFont


$Form.Controls.Add($Win10Label)
$Form.Controls.Add($Win10AddDomain)
$Form.Controls.Add($Win10Bradford)
$Form.Controls.Add($Win10CompInfo)
$Form.Controls.Add($Win10MapDeptShare)
$Form.Controls.Add($Win10MapJake)
$Form.Controls.Add($Win10MapUserShare)
$Form.Controls.Add($Win10NewComp)
$Form.Controls.Add($Win10EnableRemoteDesktop)
$Form.Controls.Add($Win10RemoveDomain)
$Form.Controls.Add($Win10RenameComp)
$Form.Controls.Add($Win10AddTTUDevicesWifi)
$Form.Controls.Add($Win10UpdateWindows)

$Form.ShowDialog()
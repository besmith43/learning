#!/usr/bin/env python3

import subprocess
import win32api

not_running_message = b'INFO: No tasks are running which match the specified criteria.\r\n'

chrome_test = subprocess.Popen('tasklist /FI "IMAGENAME eq chrome.exe"', stdin=subprocess.PIPE, stderr=subprocess.PIPE, stdout=subprocess.PIPE).communicate()[0]
teams_test = subprocess.Popen('tasklist /FI "IMAGENAME eq Teams.exe"', stdin=subprocess.PIPE, stderr=subprocess.PIPE, stdout=subprocess.PIPE).communicate()[0]
skype_test = subprocess.Popen('tasklist /FI "IMAGENAME eq lync.exe"', stdin=subprocess.PIPE, stderr=subprocess.PIPE, stdout=subprocess.PIPE).communicate()[0]
remote_desktop_test = subprocess.Popen('tasklist /FI "IMAGENAME eq mstsc.exe"', stdin=subprocess.PIPE, stderr=subprocess.PIPE, stdout=subprocess.PIPE).communicate()[0]

if chrome_test != not_running_message:
    print("chrome is already running")
else:
    print("launch chrome")
    win32api.WinExec('"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" "helpdesk.tntech.edu"')

if teams_test != not_running_message:
    print("teams is already running")
else:
    win32api.WinExec('"C:/Users/blake/AppData/Roaming/Microsoft/Windows/Start Menu/Programs/Microsoft Corporation/Microsoft Teams.lnk"')
    print("launch teams")

if skype_test != not_running_message:
    print("skype is already running")
else:
    win32api.WinExec('"C:\Program Files (x86)\Microsoft Office\\root\Office16\lync.exe"')
    print("launch skype")

if remote_desktop_test != not_running_message:
    print("remote desktop is already running")
else:
    win32api.WinExec('"mstsc"')
    print("launch remote desktop")

#!/usr/bin/env python3

import os

command = "powershell.exe -ExecutionPolicy Bypass -NoProfile -command \"& {start-sleep 10; write-host \"hello from powershell\"}\""

os.system(command)

print("hello from python")
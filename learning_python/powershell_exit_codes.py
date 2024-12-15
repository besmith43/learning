#!/usr/bin/env python3

import os

pwd = os.getcwd()
exit_code = os.system("powershell.exe -file " + pwd + "\\test_exit_code.ps1")

print(exit_code)

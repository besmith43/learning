#!/usr/bin/env python3

from sys import platform

# this needs to have either python or python3 at the top.  It will run on windows if there is a .py extension but linux will look at the first line of the file first.

if platform == "linux" or platform == "linux2":
	    # linux
		print("Running from Linux")
elif platform == "win32":
	    # Windows...
		print("Running from Windows")

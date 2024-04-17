import os
import sys

length = len(sys.argv)
number = int(sys.argv[1])

if(length == 2):
    if(isinstance(number, int)):
        os.system('powershell.exe powercfg -setacvalueindex d51f48c1-3988-4ae3-a045-1cf32831bcbe 54533251-82be-4824-96c1-47b60b740d00 bc5038f7-23e0-4960-96da-33abaf5935ec ' + str(number))
        #os.system('powershell.exe powercfg -setdcvalueindex d51f48c1-3988-4ae3-a045-1cf32831bcbe 54533251-82be-4824-96c1-47b60b740d00 bc5038f7-23e0-4960-96da-33abaf5935ec ' + str(number))
    else:
        print("not an integer")
else:
    print("wrong number of arguments")

#!/usr/bin/env python3

import os

message = "testing boxes"

if (os.path.isfile("boxes")):
    os.system('echo "' + message + '" | boxes')

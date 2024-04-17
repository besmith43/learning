#!/usr/bin/env pwsh

# release flag is used to compile for older versions of java
# in this case, I'm targeting the version of java that's on TTU's computers
javac --release 8 ./GUI.java
java GUI

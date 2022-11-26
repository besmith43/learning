from subprocess import call
return_code = call("echo Hello World", shell=True)
print(return_code)

#!/usr/bin/env bash

echo cleaning up

if [ -f ./libmylib.dylib.o ]; then
    rm ./libmylib.dylib.o
fi

if [ -f ./libmylib.dylib ]; then
    rm ./libmylib.dylib
fi

if [ -f ./main ]; then
    rm ./main
fi

echo compile zig file as dynamic library 
zig build-lib mylib.zig -dynamic 
echo compile rust file linking our library 
rustc main.rs -L . -l mylib 



if [ -f ./libmylib.dylib.o ]; then
    rm ./libmylib.dylib.o
fi


echo running application
LD_LIBRARY_PATH=./ ./main 


IF EXIST "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\VC\Auxiliary\Build\vcvarsall.bat" (
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\VC\Auxiliary\Build\vcvarsall.bat" x86_arm64
    clang -c hello.cpp
    clang -o hello_arm64.exe hello.o -L. -lshared_lib
)
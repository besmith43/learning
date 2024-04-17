echo "compiled C"
measure-command {.\hello_c.exe}

echo "compiled Python"
measure-command {.\hello_py.exe}

echo "interpreted Python"
measure-command {python .\hello.py}

echo "Cython"


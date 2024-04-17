from cx_Freeze import setup, Executable

setup(
        name = 'myapp',
        version = '0.1',
        description = 'fib speed test',
        author = 'blake smith',
        executables = [Executable('fib.py')]
)

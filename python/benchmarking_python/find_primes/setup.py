from cx_Freeze import setup, Executable

setup(
        name = 'myapp',
        version = '0.1',
        description = 'find primes speed test',
        author = 'blake smith',
        executables = [Executable('find_primes.py')]
)

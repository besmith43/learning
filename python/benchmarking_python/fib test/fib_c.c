#include <stdio.h>

void main(int argc, char* argv[])
{
    int  fib1 = 0;
    int fib2 = 1;
    int fib3 = 0;
    int limit = atoi(argv[1]);
    int count = 2;

    while (count <= limit)
    {
        fib3 = fib1 + fib2;
        count++;
        fib1 = fib2;
        fib2 = fib3;
    }

    printf("%d\n", fib3);
}
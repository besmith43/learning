#include <stdio.h>

void main()
{
	unsigned int max = 100000;
	int prime_count = 0; 
	int isprime = 0; // 0 for yes, 1 for no
	unsigned int i;

	for(i=2;i<=max;i++)
	{
		//printf("this is i: %i \n", i);
		isprime = 0;
		unsigned int j;
		for(j=2;j<i;j++)
		{
			//printf("this is j: %i \n", j);
			if(i%j == 0)
			{
				isprime = 1;
				break;
			}
		}

		//printf("ending j loop, testing isprime\n");
		if(isprime == 0);
		{
			//printf("passed is prime test\n");
			//printf("isprime's value is %i\n", isprime);
			//printf("prime_count is %i\n", prime_count);
			prime_count++;
		}
	}

	printf("The number of primes is %i \n", prime_count);
}

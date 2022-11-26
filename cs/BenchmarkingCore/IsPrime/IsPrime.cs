using System;
using System.Collections.Generic;

namespace BenchmarkingCore
{
    public class IsPrime
    {
        private List<int> primes;

        public IsPrime()
        {
            primes = new List<int>();
        }
        public bool TestForPrime(int num)
        {
            if (num < 1)
            {
                return false;
            }
            else if (num == 1 || num == 2)
            {
                primes.Add(num);
                return true;
            }
            else if (num % 2 == 0)
            {
                return false;
            }
            else
            {
                foreach(var prime in primes)
                {
                    if (num % prime == 0)
                    {
                        return false;
                    }
                }

                primes.Add(num);
                return true;
            }
        }
    }
}

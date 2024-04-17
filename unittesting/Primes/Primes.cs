using System;

//see https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit and https://docs.microsoft.com/en-us/dotnet/core/testing/
// for more info

namespace Primes
{
    public class Prime
    {
        public bool IsPrime(int candidate)
        {
            if (candidate < 2 || candidate % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

using NUnit.Framework;
using Primes;

namespace Primes.Test
{
    public class Tests
    {
        [Test]
        public void IsPrime_Test1()
        {
            Prime instance = new Prime();

            bool result = instance.IsPrime(1);

            Assert.IsFalse(result, "1 should not be prime");
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            Prime instance = new Prime();

            var result = instance.IsPrime(value);

            Assert.IsFalse(result, $"{value} should not be prime");
        }
    }
}
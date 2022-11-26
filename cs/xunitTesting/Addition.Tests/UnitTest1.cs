using System;
using Xunit;
using Addition;

namespace Addition.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, Class1.Add(2,2));
        }
    }
}

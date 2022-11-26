using System;
using Xunit;
using doWorkLib;

namespace lib.test
{
    public class UnitTest1
    {
        [Fact]
        public void TestAdd()
        {
			int c = SharedClass.add(5, 5);
			Assert.Equal(c, 10);
        }

		[Fact]
		public void TestSub()
		{
			int c = SharedClass.sub(5, 3);
			Assert.Equal(c, 2);
		}

		[Fact]
		public void TestMul()
		{
			int c = SharedClass.mul(6, 2);
			Assert.Equal(c, 12);
		}

		[Fact]
		public void TestDiv()
		{
			int c = SharedClass.div(6, 2);
			Assert.Equal(c, 3);
		}
    }
}

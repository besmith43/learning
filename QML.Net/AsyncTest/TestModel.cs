using System;
using System.Threading.Tasks;

namespace AsyncTest
{
    public class TestModel
    {
        public async Task<string> RunTask(string message)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return message;
        }
    }
}
using System;

// see https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
// see https://blogs.msdn.microsoft.com/benjaminperkins/2017/03/08/how-to-call-an-async-method-from-a-console-app-main-method/

namespace ParallelvsThreadingvsAsyncEx
{
    public class AsyncLib
    {
        static private async Task callWebApi(string url)
        {
            WebResponse response = await WebRequest
                .Create(url)
                .GetResponseAsync()
                .ConfigureAwait(false);;
            WriteLine(response.StatusCode.ToString());
        }
    }
}

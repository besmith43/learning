using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

//see https://www.dotnetperls.com/async for another example

namespace async
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new GetWebsites();
            test.StartDownload();
        }
    }

    class GetWebsites
    {
        public async void StartDownload()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await RunDownloadParallelAsync();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.Write($"Total execution time: { elapsedMs }");
        }

        private async Task RunDownloadParallelAsync()
        {
            List<string> websites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (string site in websites)
            {
                tasks.Add(DownloadWebsiteAsync(site));
            }

            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                Console.Write($"{ item.WebsiteUrl } downloaded: { item.WebsiteData.Length } characters long.{ Environment.NewLine }");
            }
        }

        private async Task<WebsiteDataModel> DownloadWebsiteAsync(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = await client.DownloadStringTaskAsync(websiteURL);

            return output;
        }

        private List<string> PrepData()
        {
            List<string> output = new List<string>();

            output.Add("https://www.yahoo.com");
            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.cnn.com");
            output.Add("https://www.codeproject.com");
            output.Add("https://www.stackoverflow.com");

            return output;
        }
    }

    public class WebsiteDataModel
    {
        public string WebsiteUrl { get; set; } = "";
        public string WebsiteData { get; set; } = "";
    }
}

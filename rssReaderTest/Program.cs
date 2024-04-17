using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace rssReaderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string hauntHerFeedURL = "https://anchor.fm/s/30977464/podcast/rss";

            GetAllPosts(hauntHerFeedURL);
        }

        public static void GetAllPosts(string feedURL)
        {
            var reader = XmlReader.Create(feedURL);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();

            Console.WriteLine($"{ feed.Title.Text }");
            
            foreach(var post in feed.Items)
            {
                Console.WriteLine($"{ post.Title.Text }\n{ post.Links[0].Uri }\n{ post.PublishDate }");
            }
        }
    }
}

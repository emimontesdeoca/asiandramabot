using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DramaBot.Sources
{
    public class Dramafever
    {
        public static string source = "DramaFever";
        public static string baseurl = "https://www.dramafever.com/";

        public static List<string> Tweets()
        {

            List<string> tweets = new List<string>();

            string url, title, episode = "", genre = "Series", shorturl = "", message;

            try
            {
                Console.WriteLine("[{0}] - Starting {1}!", DateTime.Now, source);
                using (WebClient client = new WebClient())
                {
                    /// Get all page
                    string htmlCode = client.DownloadString("https://www.dramafever.com/");

                    /// Get first of all items
                    string latest = htmlCode.Split(new[] { @"<ul class=""item-list latest-list"">" }, StringSplitOptions.None)[1];
                    latest = latest.Split(new[] { @"<a href=""" }, StringSplitOptions.None)[1];
                    /// Get URL
                    url = latest.Split(new[] { @" class" }, StringSplitOptions.None)[0];
                    url = url.Remove(url.Length - 1, 1);
                    url = baseurl + url;

                    /// Get title
                    title = latest.Split(new[] { @">" }, StringSplitOptions.None)[1];
                    title = title.Split(new[] { @"<" }, StringSplitOptions.None)[0];
                    /// Get episode first
                    episode = latest.Split(new[] { @"Ep " }, StringSplitOptions.None)[1];
                    episode = episode.Split(new[] { @"<" }, StringSplitOptions.None)[1];



                    /// Get short URL
                    shorturl = Utils.urlShorter(url);

                    /// Twitter message
                    message = "[🇬🇧 - " + source + " - " + genre + "] " + title + " - " + episode + "- https://goo.gl/" + shorturl + " #KDrama";


                    /// Log
                    //Console.WriteLine("URL: " + url);
                    //Console.WriteLine("Title: " + title);
                    //Console.WriteLine("Genre: " + genre);
                    //Console.WriteLine("Episode: " + episode);
                    //Console.WriteLine("Shorturl: https://goo.gl/" + shorturl);
                    //Console.WriteLine("Twitter message: " + message);
                    //Console.WriteLine("---");

                    if (!Utils.tweetExists(message))
                    {
                        try
                        {
                            tweets.Add(message);
                            Utils.saveTxt(message);
                        }
                        catch (Exception)
                        {
                        }
                    }



                }
                Console.WriteLine("[{0}] - {1} finished!", DateTime.Now, source);
            }
            catch (Exception)
            {
                Console.WriteLine("[{0}] - There was an error with {1}!", DateTime.Now, source);
            }

            return tweets;

        }
    }
}

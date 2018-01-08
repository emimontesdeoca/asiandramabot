using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DramaBot.Sources
{
    public class Kshow123
    {
        public static string source = "Kshow123";
        public static string baseurl = "http://kshow123.net/";

        public static List<string> Tweets()
        {

            List<string> tweets = new List<string>();

            string url, title, episode = "", genre = "Series", shorturl = "", message;

            try
            {
                Console.WriteLine("[{0}] - Starting {1}!", DateTime.Now, source);

                string htmlCode = Utils.getHtml(baseurl);

                /// Get first of all items
                string latest = htmlCode.Split(new[] { @"<span class=""label label-sub"">SUB</span>" }, StringSplitOptions.None)[1];
                latest = latest.Split(new[] { @"a href=""" }, StringSplitOptions.None)[1];
                /// Get URL
                url = latest.Split(new[] { @" title" }, StringSplitOptions.None)[0];
                //url = url.Remove(url.Length - 1, 1);
                url = baseurl + url.Remove(url.Length - 1, 1);


                /// Get title
                title = latest.Split(new[] { @"title=""" }, StringSplitOptions.None)[1];
                title = title.Split(new[] { @">" }, StringSplitOptions.None)[0];
                title = title.Remove(title.Length - 1, 1);
                /// Get episode first
                episode = latest.Split(new[] { @" Ep " }, StringSplitOptions.None)[1];
                episode = episode.Split(new[] { @"<" }, StringSplitOptions.None)[0];
                episode = "Episode " + episode;

                /// Get short URL
                shorturl = Utils.urlShorter(url);

                /// Twitter message
                message = "[🇬🇧 - " + source + " - " + genre + "] " + title + " - " + episode + " - https://goo.gl/" + shorturl + " #KDrama";

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DramaBot.Sources
{
    public class Kbsworld
    {
        public static string source = "KBS World TV";
        public static string baseurl = "https://www.youtube.com/user/kbsworld/";
        public static string baseurlyt = "https://www.youtube.com/watch?v=";

        public static List<string> Tweets()
        {

            List<string> tweets = new List<string>();

            string url, title, genre = "Variety", shorturl = "", message;

            try
            {
                Console.WriteLine("[{0}] - Starting {1}!", DateTime.Now, source);

                string htmlCode = Utils.getHtml("https://www.youtube.com/user/kbsworld/videos?sort=dd&view=0&flow=grid");

                /// Get first of all items
                string latest = htmlCode.Split(new[] { @"yt-ui-ellipsis yt-ui-ellipsis-2" }, StringSplitOptions.None)[1];

                /// Get URL
                url = latest.Split(new[] { @"watch?v=" }, StringSplitOptions.None)[1];
                url = url.Split(new[] { @">" }, StringSplitOptions.None)[0];
                url = url.Remove(url.Length - 1, 1);
                url = baseurlyt + url;

                /// Get title
                title = latest.Split(new[] { @"title=""" }, StringSplitOptions.None)[1];
                title = title.Split(new[] { @" aria" }, StringSplitOptions.None)[0];
                title = title.Remove(title.Length - 2, 2);

                /// Get short URL
                shorturl = Utils.urlShorter(url);

                /// Twitter message
                message = "[🇬🇧 - " + source + " - " + genre + "] " + title + " - https://goo.gl/" + shorturl + " #KDrama";


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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DramaBot.Sources
{
    public class Ondemandkorea
    {
        public static string source = "OnDemandKorea";
        public static string baseurl = "http://www.ondemandkorea.com/";

        public static List<string> Tweets()
        {

            List<string> tweets = new List<string>();

            string url, title, genre = "Series", episode = "", shorturl = "", message;

            try
            {
                Console.WriteLine("[{0}] - Starting {1}!", DateTime.Now, source);

                string htmlCode = Utils.getHtml(baseurl);

                string latest = htmlCode.Split(new[] { @"class=""tab-pane active""" }, StringSplitOptions.None)[1];

                for (int i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        latest = htmlCode.Split(new[] { @"class=""tab-pane active""" }, StringSplitOptions.None)[1];
                        genre = "Series";
                        latest = latest.Split(new[] { @"<div class=""content-half-right"">" }, StringSplitOptions.None)[1];
                    }
                    else
                    {
                        latest = htmlCode.Split(new[] { @"class=""tab-pane active""" }, StringSplitOptions.None)[1];
                        genre = "Variety";
                        latest = latest.Split(new[] { @"<div class=""content-half-left"">" }, StringSplitOptions.None)[1];
                    }
                    /// Get first of all items

                    latest = latest.Split(new[] { @"<div class=""truncate"">" }, StringSplitOptions.None)[1];

                    /// Get URL
                    url = latest.Split(new[] { @"href=""" }, StringSplitOptions.None)[1];
                    url = url.Split(new[] { @"""" }, StringSplitOptions.None)[0];
                    url = baseurl + url;

                    /// Get title
                    title = latest.Split(new[] { @"detail"">• " }, StringSplitOptions.None)[1];
                    title = title.Split(new[] { @" :" }, StringSplitOptions.None)[0];

                    /// Get episode first
                    try
                    {
                        episode = latest.Split(new[] { @" : E" }, StringSplitOptions.None)[1];
                        episode = "Episode " + episode.Split(new[] { @" href" }, StringSplitOptions.None)[0];
                        episode = episode.Remove(episode.Length - 1, 1);
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    /// Get short URL
                    shorturl = Utils.urlShorter(url);

                    /// Twitter message
                    message = "[🇬🇧 - @ondemandkorea - " + genre + "] " + title + " - " + episode + " - https://goo.gl/" + shorturl + " #KDrama";

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

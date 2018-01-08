using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DramaBot.Sources
{
    public class Quickdrama
    {

        public static string[] sites = { "series", "movies", "series-genre/variety", "" };
        public static string source = "QuickDrama";
        public static string language = "EN";

        public static List<string> Tweets()
        {

            List<string> tweets = new List<string>();

            try
            {
                Console.WriteLine("[{0}] - Starting {1}!", DateTime.Now, source);
                foreach (string genre in sites)
                {
                    if (genre == "")
                    {
                        break;
                    }
                    string url, title, newgenre, episode = "", shorturl, message;

                    string htmlCode = Utils.getHtml("http://quickdrama.com/" + genre);


                    /// Get first of all items
                    string latest = htmlCode.Split(new[] { @"<a data-notiffy=""false"" href=""" }, StringSplitOptions.None)[1];

                    /// Get URL
                    url = latest.Split(new[] { @"/""" }, StringSplitOptions.None)[0];

                    /// Get genre and fix for variety
                    newgenre = (genre == "series-genre/variety") ? Utils.UppercaseFirst("variety") : Utils.UppercaseFirst(genre);

                    /// Get title
                    title = latest.Split(new[] { @"title=""" }, StringSplitOptions.None)[1];
                    title = title.Split(new[] { @"data-url=""" }, StringSplitOptions.None)[0];
                    title = title.Remove(title.Length - 2, 2);

                    /// Fix genre for movies
                    if (genre != "movies")
                    {
                        episode = latest.Split(new[] { @"<span class=""mli-eps"">" }, StringSplitOptions.None)[1];
                        episode = episode.Split(new[] { @"</" }, StringSplitOptions.None)[0];
                    }

                    /// Get short URL
                    shorturl = Utils.urlShorter(url);

                    /// Twitter message
                    if (genre != "movies")
                    {
                        message = "[🇬🇧 - " + source + " - " + newgenre + "] " + title + " - " + episode + " - https://goo.gl/" + shorturl + " #KDrama";
                    }
                    else
                    {
                        message = "[🇬🇧 - " + source + " - " + newgenre + "] " + title + " - https://goo.gl/" + shorturl + " #KDrama";
                    }

                    /// Log
                    //Console.WriteLine("URL: " + url);
                    //Console.WriteLine("Title: " + title);
                    //Console.WriteLine("Genre: " + newgenre);
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

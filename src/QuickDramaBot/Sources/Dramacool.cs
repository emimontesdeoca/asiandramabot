using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DramaBot.Sources
{
    public class Dramacool
    {
        public static string source = "Dramacool";
        public static string baseurl = "https://watchasian.co/";

        public static List<string> Tweets()
        {

            List<string> tweets = new List<string>();

            string url, title, episode = "", shorturl = "", message;

            try
            {
                Console.WriteLine("[{0}] - Starting {1}!", DateTime.Now, source);

                string htmlCode = Utils.getHtml(baseurl);

                for (int i = 1; i <= 3; i++)
                {
                    /// Get first of all items
                    string latest = htmlCode.Split(new[] { @"<div class=""tab-content left-tab-" + i }, StringSplitOptions.None)[1];
                    latest = latest.Split(new[] { @"<ul class=""switch-block list-episode-item"">" }, StringSplitOptions.None)[1];
                    latest = latest.Split(new[] { @"<a href=""" }, StringSplitOptions.None)[1];
                    /// Get URL
                    url = latest.Split(new[] { @"""" }, StringSplitOptions.None)[0];
                    url = baseurl + url;

                    /// Get title
                    title = latest.Split(new[] { @"alt=""" }, StringSplitOptions.None)[1];
                    title = title.Split(new[] { @" data-original" }, StringSplitOptions.None)[0];

                    /// Get short URL
                    shorturl = Utils.urlShorter(url);

                    if (i == 1)
                    {
                        /// Get title

                        if (title.Contains('('))
                        {
                            title = title.Split(new[] { @" (" }, StringSplitOptions.None)[0];
                        }
                        else
                        {
                            title = title.Split(new[] { @"""" }, StringSplitOptions.None)[0];
                        }

                        /// Get episode 
                        episode = "Episode " + latest.Split(new[] { @"<span class=""ep SUB"">EP " }, StringSplitOptions.None)[1];
                        episode = episode.Split(new[] { @"</span>" }, StringSplitOptions.None)[0];

                        /// Twitter message
                        message = "[🇬🇧 - " + source + " - Series] " + title + " - " + episode + "- https://goo.gl/" + shorturl + " #KDrama";
                    }
                    else
                    {

                        if (i == 2)
                        {
                            /// Get title
                            title = title.Split(new[] { @"""" }, StringSplitOptions.None)[0];
                            /// Get episode
                            episode = "Movie ";
                            /// Twitter message
                            message = "[🇬🇧 - " + source + " - Movie] " + title + " - https://goo.gl/" + shorturl + " #KDrama";
                        }
                        else
                        {
                            /// Get title
                            title = title.Split(new[] { @"""" }, StringSplitOptions.None)[0];
                            /// Get episode 
                            episode = "Episode " + latest.Split(new[] { @"<span class=""ep SUB"">EP " }, StringSplitOptions.None)[1];
                            episode = episode.Split(new[] { @"</span>" }, StringSplitOptions.None)[0];
                            episode += " ";

                            /// Twitter message
                            message = "[🇬🇧 - " + source + " - Show] " + title + " - " + episode + "- https://goo.gl/" + shorturl + " #KDrama";
                        }

                    }

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

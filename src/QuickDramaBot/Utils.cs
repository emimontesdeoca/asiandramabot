﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;

namespace DramaBot
{
    public class Utils
    {

        public static string path = @"C:\bots\dramabot\tweets.txt";
        public static string log = @"C:\bots\dramabot\logs.txt";
        public static string googleApi = "";
        public static string UppercaseFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        public static string urlShorter(string url)
        {
            /// AIzaSyD8gFksIv8wq6QoEuXIdCYcBKY1Mc72Ufw

            string finalURL = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url?key=" + googleApi);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"longUrl\" : ";
                json += "\"" + url + "\"}";
                //Console.WriteLine(json);
                streamWriter.Write(json);
            }

            var responseText = "";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseText = streamReader.ReadToEnd();
                //Console.WriteLine(responseText);
            }

            finalURL = responseText.Split(new[] { @"https://goo.gl/" }, StringSplitOptions.None)[1];
            finalURL = finalURL.Split(new[] { @"""," }, StringSplitOptions.None)[0];
            return finalURL;
        }

        public static async Task<Status> SendTweet(string message)
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "",
                    ConsumerSecret = "",
                    AccessToken = "-KofpFi4VTLpEQXTijQK6PZynMc8OqWH",
                    AccessTokenSecret = ""
                }
            };

            var context = new TwitterContext(auth);

            var status = await context.TweetAsync(message);

            return status;
        }

        public static void saveTxt(string message)
        {

            using (StreamWriter save = new StreamWriter(path, true))
            {
                save.WriteLine(message);
            }

        }

        public static void saveLog(string message)
        {
            using (StreamWriter save = new StreamWriter(log, true))
            {
                save.WriteLine(message);
            }
        }
        public static bool tweetExists(string message)
        {
            List<string> tweets = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tweets.Add(line);
                }

            }

            try
            {
                tweets.Single(x => x == message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string checkErrors(string message)
        {
            if (message.Contains("&#8211;"))
            {
                message = message.Replace("&#8211;", "-");
            }

            if (message.Contains("&amp;#039;"))
            {
                message = message.Replace("&amp;#039;", "'");
            }

            if (message.Contains("&#8217;"))
            {
                message = message.Replace("&#8217;", "'");
            }

            if (message.Contains("â€™"))
            {
                message = message.Replace("â€™", "'");
            }

            if (message.Contains("â€“"))
            {
                message = message.Replace("â€“", "-");
            }

            if (message.Contains("&#39;"))
            {
                message = message.Replace("&#39;", "'");
            }

            return message;
        }

        public static string getHtml(string html)
        {
            string htmlCode = "";
            using (WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                /// Get all page
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                htmlCode = client.DownloadString(html);
            }

            return htmlCode;
        }
    }
}

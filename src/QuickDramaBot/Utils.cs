
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
        public static string googleApi = "AIzaSyD8gFksIv8wq6QoEuXIdCYcBKY1Mc72Ufw";
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
                    ConsumerKey = "savKnrRwFokkbVFt7bpkjDHxA",
                    ConsumerSecret = "tARrxzKYmph0oXWv7vP6HkWy0Cm32NMArAyyiBgfhUW4Ja8jYO",
                    AccessToken = "949460851156713472-KofpFi4VTLpEQXTijQK6PZynMc8OqWH",
                    AccessTokenSecret = "WZDTPrXsxLFybyRrVaIwvSDimIyLEOSZr7IRhnMCeK18C"
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
    }
}

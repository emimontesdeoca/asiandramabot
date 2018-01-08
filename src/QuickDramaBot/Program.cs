using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Threading;

namespace DramaBot
{
    class Program
    {

        static void Main(string[] args)
        {
            DateTime start = DateTime.Now, finish;
            Console.WriteLine("[{0}] - Starting @Asiandramabot!", DateTime.Now);
            Console.WriteLine("\n--------------------------------------------------------------------\n");
            List<string> tweets = new List<string>();

            //tweets.AddRange(Sources.Myasiantv.Tweets());
            //tweets.AddRange(Sources.Quickdrama.Tweets());
            //tweets.AddRange(Sources.Dramacool.Tweets());
            //tweets.AddRange(Sources.Estrenosdoramas.Tweets());
            //tweets.AddRange(Sources.Ondemandkorea.Tweets());
            //tweets.AddRange(Sources.Kbsworld.Tweets());
            //tweets.AddRange(Sources.Kshowonline.Tweets());
            //tweets.AddRange(Sources.Dramafever.Tweets());
            //tweets.AddRange(Sources.Dramahood.Tweets());
            //tweets.AddRange(Sources.Fastdrama.Tweets());
            tweets.AddRange(Sources.Kshow123.Tweets());


            Console.WriteLine("\n--------------------------------------------------------------------\n");

            Console.WriteLine("[{0}] - Updates: " + tweets.Count() + "!", DateTime.Now);
            foreach (var item in tweets)
            {
                string message = item;

                message = Utils.checkErrors(message);

                Console.WriteLine("\n" + message);
                //try
                //{
                //    var result = Task.Run(() => Utils.SendTweet(message));
                //    result.Wait();
                //    if (result == null)
                //    {
                //        Console.WriteLine("Tweet failed to process, but API did not report an error"); 
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
            }
            Console.WriteLine("\n--------------------------------------------------------------------\n");
            finish = DateTime.Now;
            var diff = finish.Subtract(start);
            Console.WriteLine("[{0}] - Work completed in {1} seconds!", DateTime.Now, diff.Seconds);

            //Console.ReadLine();
        }
    }
}


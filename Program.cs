using System;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;

namespace InstaParser
{
    public class Program
    {
        
        public static void  Main(string[] args)
        {
            string InstagramUrl = "https://instagram.com/";
            Console.WriteLine("Hello I'm lite instagram parser!");
            Console.WriteLine("Please, eneter user name and I'll fetch the number of posts and followers for you");
            var userName = Console.ReadLine();
            Console.WriteLine("Thank you, wait a sec!");
            try
            {
                var userInfo = InstagramUrl
                    .AppendPathSegment(userName)
                    .SetQueryParams(new { __a = 1 })
                    .GetStringAsync().GetAwaiter().GetResult();

                JObject JsonObject = JObject.Parse(userInfo);
                Console.WriteLine(userName);
                Console.WriteLine($"Followers count {JsonObject.SelectToken("graphql.user.edge_followed_by.count")}");
                Console.WriteLine($"Posts count {JsonObject.SelectToken("graphql.user.edge_owner_to_timeline_media.count")}");
                Console.ReadKey();

            }catch (Exception ex)
            {
                Console.WriteLine($"Something going wrong! Check the user name or see exception message {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}

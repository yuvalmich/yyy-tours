using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace yyytours.Controllers
{
    public class FacebookApi
    {
        public static readonly string facebookToken = "EAAOG9p5n6eUBAMipnUBAEV5c6FghheOISLPZCXTm69yWr7nqXPuA82cmt9xES6GAAcd9owL6bZCZBMpfPeqg1yqgNOYrZB89Fx50L3w50UIqo3shezN86VWc3EUhlidJmRXpaWdY2GpzqfC3912kDf1z3ewEN8UiYae5gZAQOjnrPr5ZBx9GPJR2VeZB9wP4KMZD";
        public static readonly string facebookPageId = "100819638428211";
        public static readonly string facebookBaseUrl = "https://graph.facebook.com/";
        private static readonly HttpClient client = new HttpClient();

        public async static Task CreateFacebookPost(string[] message)
        {
            var parameters = new Dictionary<string, string>
            {
                { "message", string.Join(Environment.NewLine, message) },
                { "access_token", facebookToken}
            };
            var content = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(facebookBaseUrl + facebookPageId + "/feed", content);
            await response.Content.ReadAsStringAsync();
        }

    }

}

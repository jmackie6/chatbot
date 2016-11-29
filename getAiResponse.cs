using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatbot
{
    public class Json
    {
        public string timestamp { get; set; }
        public string result { get; set; }
    }

    class getAiResponse
    {
        private static string URL = "https://api.api.ai/v1/query";
        private static string urlParameters = "";
        private static string language = "&lang=en";
        private static string queryString = "&query=";
        //private string developerAccessToken = "204591ee637d4460a5179c06fc038c5a";
        private static string clientAccessToken = "efeec4bb5e9f43fa836537751ca674d5";
        private  static int v = 20161128;
        private static string sessionId = "&sessionId=d16c4966-d289-4b28-b916-8bb57f95024d";

        
        public static string timestamp = "";
        public static string answer = "";
        public static string error = "";

        public static async void GetResponse(string question, string question2)
        {
           
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientAccessToken);
           
            urlParameters += "?v=" + v + queryString + question + language + sessionId;
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;

            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic json = JObject.Parse(responseBody);
                //timestamp = stuff.timestamp;
                answer = json.result.fulfillment.speech;
                
                //answer = "we called the api";
            }
            else
            {
                //answer = response.StatusCode.ToString();
                error = response.ReasonPhrase;
                //error = urlParameters;

            }
        }
    }
}

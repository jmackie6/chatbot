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
using ApiAiSDK;
using ApiAiSDK.Model;
using Google.Apis.Calendar.v3;

namespace Chatbot
{
    
    public class getAiResponse
    {
        //private static string URL = "https://api.api.ai/v1/query";
        //private static string urlParameters = "";
        //private static string language = "&lang=en";
        //private static string queryString = "&query=";
        //private string developerAccessToken = "204591ee637d4460a5179c06fc038c5a";
        //private static int v = 20161128;
        //private static string sessionId = "&sessionId=d16c4966-d289-4b28-b916-8bb57f95024d";
        //public static string timestamp = "";

        public static string answer = "";
        public static string error = "";

        public static string startTime = "";
        public static string endTime = "";
        public static string title = "";
        public static string date = "";
        public static string[] parameters = new string[] { };
        Dictionary<string, object> dObject = new Dictionary<string, object>();

        //public static ApiAi apiAi;
        private static string clientAccessToken = "efeec4bb5e9f43fa836537751ca674d5";
        static AIService aiService;
        
        //public static async Task<string> GetResponse(string question, string startTime, string endTime, string title, string date,  string[] parameters, Dictionary<string, object> dObject)
        //public static async Task<string> GetResponse(string question, AIService aiService)
        public static async Task<string> GetResponse(string question)
        {

            try
            {
                var config = new AIConfiguration(clientAccessToken, SupportedLanguage.English);
                aiService = AIService.CreateService(config);
                await aiService.InitializeAsync();

                
                
                //foreach (var i in response.Result.Parameters)
                //{
                //    if (i.Key == "startTime")
                //    {
                //        startTime = i.Value.ToString();
                //    }
                //    else if (i.Key == "endTime")
                //    {
                //        endTime = i.Value.ToString();
                //    }
                //    else if (i.Key == "date")
                //    {
                //        date = i.Value.ToString();
                //    }
                //    else if (i.Key == "title")
                //    {
                //        title = i.Value.ToString();
                //    }
                //}
                //aiService.StartRecognitionAsync();
                //await aiService.StartRecognitionAsync();
            }
            catch (Exception e)
            {
                error = "error"; // Some exception processing
            }

            var response = await aiService.TextRequestAsync(question);
            //answer = response.Result.Fulfillment.Speech;
            //response = await aiService.StartRecognitionAsync();
            answer = response.Result.Fulfillment.Speech;
            return answer;
        }
        
    }
}

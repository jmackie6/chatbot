using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Newtonsoft.Json;
using ApiAiSDK;
using ApiAiSDK.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Chatbot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {

        //private static string clientAccessToken = "efeec4bb5e9f43fa836537751ca674d5";
        //static AIService aiService;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private  async void send_Click(object sender, RoutedEventArgs e)
        {
            
            string except = error.Text;

            //getAiResponse response = new getAiResponse();
            //try
            //{
            //    //var response = await aiService.StartRecognitionAsync();
            //    var config = new AIConfiguration(clientAccessToken, SupportedLanguage.English);
            //    aiService = AIService.CreateService(config);
            //    await aiService.InitializeAsync();
            //}
            //catch (Exception exception)
            //{
            //    // Some exception processing
            //}

            //await getAiResponse.GetResponse(question.Text, aiService);
            //await .StartRecognitionAsync();

            await getAiResponse.GetResponse(question.Text);

            answer.Text = getAiResponse.answer;
            //answer.Text = getAiResponse.startTime;
            //error.Text = getAiResponse.error;
        }

    }
}

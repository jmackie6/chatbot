using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


using System.Collections;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;


using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

using Newtonsoft.Json;
using CalendarBot;
using Newtonsoft.Json.Linq;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CalendarBot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
       
        public static bool accessPage = false;
        public static string log = "";
        public static string passy = "";
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            
            accessPage = false;
            log = "";
            passy = "";

            MyFrame.Navigate(typeof(loginPage));
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (accessPage != true)
            {
                MyFrame.Navigate(typeof(loginPage));
            }
            string temp = log;
            if (log != "")
            {
                login.Text = "Windows Calendar Bot" + temp;
                displayAnswers();
            }
            
        }

        private async void displayAnswers()
        {

            string urlParameters = "https://calendarbot-2573c.firebaseio.com/users/" + log + "/history.json";
            
            HttpClient client2 = new HttpClient();

            HttpResponseMessage response = client2.GetAsync(urlParameters).Result;
            
            string responseBody = await response.Content.ReadAsStringAsync();
            
            if (responseBody == "null")
            {
                //TextBlock texxy = new TextBlock()
                //{
                //    Text = "This is the start of your conversation",
                //    TextAlignment = TextAlignment.Left,
                //    Foreground = new SolidColorBrush(Colors.White)
                //};

                //Grid griddy = new Grid()
                //{
                //    Background = new SolidColorBrush(Colors.Aqua)
                //};

                //griddy.Children.Add(texxy);
                
                //ansAndQues.Children.Add(griddy);

                ansAndQues.Children.Add(new TextBlock()
                {
                    Text = "This is the start of your conversation\n with the windows calendar bot ",
                    TextAlignment = TextAlignment.Left,
                    Foreground = new SolidColorBrush(Colors.Black)
                });

                
            }
            else
            {
                if (response.IsSuccessStatusCode)
                {

                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);
                    int count = 0;
                    foreach (var answ in dict)
                    {
                        
                        string answerValue = answ.Value;
                        
                        if(count % 2 == 0)
                        {
                            //TextBlock texxy = new TextBlock()
                            //{
                            //    Text = answerValue,
                            //    TextAlignment = TextAlignment.Left,
                            //    Foreground = new SolidColorBrush(Colors.Black)
                            //};

                            //Grid griddy = new Grid()
                            //{
                            //    Background = new SolidColorBrush(Colors.Aquamarine)
                            //};

                            //griddy.Children.Add(texxy);

                            //ansAndQues.Children.Add(griddy);
                            ansAndQues.Children.Add(new TextBlock()
                            {
                                Text = answerValue,
                                TextAlignment = TextAlignment.Left,
                                Foreground = new SolidColorBrush(Colors.Gray)
                            });

                            scroll.UpdateLayout();
                            scroll.ChangeView(0.0f, double.MaxValue, 1.0f);
                        }
                        else
                        {
                            //TextBlock texxy = new TextBlock()
                            //{
                            //    Text = answerValue,
                            //    TextAlignment = TextAlignment.Right,
                            //    Foreground = new SolidColorBrush(Colors.Black)
                            //};

                            //Grid griddy = new Grid()
                            //{
                            //    Background = new SolidColorBrush(Colors.Gray)
                            //};

                            //griddy.Children.Add(texxy);

                            //ansAndQues.Children.Add(griddy);

                            ansAndQues.Children.Add(new TextBlock()
                            {
                                Text = answerValue,
                                TextAlignment = TextAlignment.Right,
                                Foreground = new SolidColorBrush(Colors.Black)
                            });

                            scroll.UpdateLayout();
                            scroll.ChangeView(0.0f, double.MaxValue, 1.0f);

                        }
                        count++;
                    }
                }
            }
            
        }

        private async void send_Click(object sender, RoutedEventArgs e)
        {

            string parameter = string.Empty;
            
            
            var firebase = new Firebase.Database.FirebaseClient("https://calendarbot-2573c.firebaseio.com/");
            
            // add new item to list of data and let the client generate new key for you (done offline)
            var ques = await firebase
                .Child("users")
                .Child(log)
                .Child("questions")
                .PostAsync(question.Text);


            string que = question.Text;

            question.Text += "\n";
            ansAndQues.Children.Add(new TextBlock() { Text = question.Text,
                TextAlignment = TextAlignment.Right, 
                Foreground = new SolidColorBrush(Colors.Black)});

            //TextTrimming.WordEllipsis = 5 , TextWrapping.Wrap = 4,
            //    Width = 200,

            scroll.UpdateLayout();
            scroll.ChangeView(0.0f, double.MaxValue, 1.0f);

            var histQ = await firebase
                .Child("users")
                .Child(log)
                .Child("history")
                .PostAsync(question.Text);

            await AiResponse.GetResponse(question.Text);

            answer.Text = AiResponse.answer;

            var ans = await firebase
                .Child("users")
                .Child(log)
                .Child("answers")
                .PostAsync(answer.Text);

            answer.Text += "\n";
            ansAndQues.Children.Add(new TextBlock() { Text = answer.Text, TextAlignment = TextAlignment.Left, Foreground = new SolidColorBrush(Colors.Gray) });

            scroll.UpdateLayout();
            scroll.ChangeView(0.0f, double.MaxValue, 1.0f);

            var histA = await firebase
                .Child("users")
                .Child(log)
                .Child("history")
                .PostAsync(answer.Text);

            //startTime.Text = AiResponse.startTime;
            //endTime.Text = AiResponse.endTime;
            login.Text = "Welcome " + log;
            
            question.Text = "";

        }

        private void send_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //if (e.Key == )
            //{
            //    button.PerformClick();
            //    // these last two lines will stop the beep sound
            //    e.SuppressKeyPress = true;
            //    e.Handled = true;
            //}
        }
    }
    internal class Answers
    {
        public string answers { get; set; }
    }
}

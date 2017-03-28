using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static CalendarBot.MainPage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CalendarBot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class loginPage : Page
    {
        public static int num = 0;
        public loginPage()
        {
            this.InitializeComponent();
            
        }

        public async void Login_Click(object sender, RoutedEventArgs e)
        {
            
            if (!Regex.IsMatch(UserName.Text.Trim(), @"^[A-Za-z_][a-zA-Z0-9_\s]*$"))
            {
                //MessageBox.Show("Invalid UserName");
                var dialog = new MessageDialog("Invalid UserName");
                await dialog.ShowAsync();
            }
            //Password length Validation   
            else if (PassWord.Password.Length < 6)
            {
                //MessageBox.Show("Password length should be minimum of 6 characters!");
                var dialog = new MessageDialog("Password length is greater than 6 characters!");
                await dialog.ShowAsync();
            }
            //    //After validation success ,store user detials and also check information  
            else if (UserName.Text != "" && PassWord.Password != "")
            {
                    var firebase = new FirebaseClient("https://calendarbot-2573c.firebaseio.com/");
                var username = await firebase
                  .Child("users")
                  .OnceAsync<UserName>();

                MainPage.log = UserName.Text;
                
                bool find = false;
                bool err = false;
                string urlParameters = "https://calendarbot-2573c.firebaseio.com/users/" + UserName.Text + "/password.json";
                HttpClient client = new HttpClient();
                
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;

                string responseBody = await response.Content.ReadAsStringAsync();

                if(responseBody != "null")
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string pa = PassWord.Password;
                        var json = JObject.Parse(responseBody);
                        var first = json.Properties().Select(p => p.Name).FirstOrDefault();
                        if (first == PassWord.Password)
                        {
                            find = true;
                        }
                    }
                }
                else
                {
                    var dialog = new MessageDialog("Wrong username or password");
                    await dialog.ShowAsync();
                }
                

                foreach (var u in username)
                {
                    if (u.Key == UserName.Text && find == true)
                    {
                        
                        MainPage.accessPage = true;
                        LoginFrame.Navigate(typeof(MainPage));

                    }
                    else if (u.Key == UserName.Text && find != true)
                    {
                        err = true;
                    }

                }

                if (err == true)
                {
                    var dialog = new MessageDialog("Wrong username or password");
                    await dialog.ShowAsync();
                }

            }
        }

        public void SignUp_Click(object sender, RoutedEventArgs e)
        {
            LoginFrame.Navigate(typeof(signUp));
        }
    }

    internal class UserPass
    {
        //public string email { get; set; }
        public string pass { get; set; }
    }

    internal class UserName
    {
        public string username { get; set; }
    }
}

    

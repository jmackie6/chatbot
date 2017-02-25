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
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

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
        
        //config = new AIConfiguration(clientAccessToken, SupportedLanguage.English);
        public MainPage()
        {
            this.InitializeComponent();
        }

        private  async void send_Click(object sender, RoutedEventArgs e)
        {


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

            //var firebase = new Firebase.Database.FirebaseClient("https://calendarbot-2573c.firebaseio.com/");

            //// add new item to list of data and let the client generate new key for you (done offline)
            //var dino = await firebase
            //    .Child("question")
            //    .Child("timestamp")
            //    .PostAsync(question.Text);

            await getAiResponse.GetResponse(question.Text);

            answer.Text = getAiResponse.answer;
            startTime.Text = getAiResponse.startTime;
            endTime.Text = getAiResponse.endTime;
            title.Text = getAiResponse.title;
            date.Text = getAiResponse.date;

            appoint.Text = getAiResponse.appoint;
            question.Text = "";

            
            //var appointment = new Windows.ApplicationModel.Appointments.Appointment();
            ////error.Text = DateTime.Now.ToString();
            //string dateTime = getAiResponse.date + " " + getAiResponse.startTime;
            ////appointment.StartTime = dateTime + TimeSpan.FromDays(1);
            //DateTime myDate = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss",
            //                           System.Globalization.CultureInfo.InvariantCulture) + TimeSpan.FromDays(1);
            //appointment.StartTime = myDate;
            ////error.Text = appointment.StartTime.ToString();
            ////error.Text = dateTime;
            //if (getAiResponse.endTime != "")
            //{
            //    int start = 1;
            //    int end = 0;
            //    int duration = start - end;
            //    appointment.Duration = TimeSpan.FromHours(duration);
            //}
            //else
            //    appointment.Duration = TimeSpan.FromHours(1);

            //appointment.Subject = getAiResponse.title;

            //appointment.Reminder = TimeSpan.FromMinutes(20); // Remind me 15 minutes prior
            //                                                 // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user' s calendar.
            //                                                 // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
            //                                                 // An empty string return value indicates that the user canceled the operation before the appointment was added.
            ////String appointmentId =
            ////    await Windows.ApplicationModel.Appointments.AppointmentManager.ShowEditNewAppointmentAsync(appointment);
            ////if (appointmentId != String.Empty)
            ////{
            ////    appoint.Text = "Appointment Id: " + appointmentId;
            ////}
            ////else
            ////{
            ////    appoint.Text = "Appointment not added.";
            ////}

            //try
            //{
            //    var response = await aiService.StartRecognitionAsync();
            //    //answer.Text = getAiResponse.answer;
            //}
            //catch (Exception exception)
            //{
            //    // Some exception processing
            //}

            //await getAiResponse.GetResponse(question.Text);


            //answer.Text = getAiResponse.startTime;
            //error.Text = getAiResponse.error;
        }


    }
}

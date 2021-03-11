using TodoLocalized.Model;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using RestSharp;
using System;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
    public partial class Verification
    {
        RandomInt random = new RandomInt();
        private static System.Timers.Timer aTimer;
        //int Backcount = 1;

        public Verification(String s)
        {
            InitializeComponent();
            switch (s)
            {
                case "first_login":
                    {
                        Debug.WriteLine("^^^^^^^^^^^^^^^^^");
                        done.Clicked += Done_Clicked;
                    }
                    break;
                case "add_account":
                    {
                        done.Clicked += Hospital_Done_Clicked;
                    }
                    break;
            }
        }
        private void Hospital_Done_Clicked(object sender, EventArgs e)
        {
            showLoading();
            var client = new RestClient("http://smartcare-health.com/phr/hospital/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("method", "c_pin");
            request.AddParameter("id", CrossSecureStorage.Current.GetValue("hid"));
            request.AddParameter("pin", code.Text);

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "999999999999999999999999999999" + CrossSecureStorage.Current.GetValue("hid"));
                    if (checkRespone(response))
                    {

                        Debug.WriteLine("im in");
                        XFToast.LongMessage("Successfully");
                        JArray ja = JArray.Parse(response.Content);
                        Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                        Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Pages.Accounts()));

                        hideLoading();
                    }
                });
            }
            else
            {
                alert("", "", "ok");
                hideLoading();
            }
        }
        private void Done_Clicked(object sender, EventArgs e)
        {
            showLoading();
            var client = new RestClient("http://smartcare-health.com/phr/hospital/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            Debug.WriteLine("111" + CrossSecureStorage.Current.GetValue("hid") + "2222" + code.Text);
            request.AddParameter("method", "c_pin");
            request.AddParameter("id", CrossSecureStorage.Current.GetValue("hid"));
            request.AddParameter("pin", code.Text);

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "999999999999999999999999999999" + CrossSecureStorage.Current.GetValue("hid"));
                    if (checkRespone(response))
                    {

                        Debug.WriteLine("im in");
                        //   XFToast.LongMessage("Successfully");
                        JArray ja = JArray.Parse(response.Content);
                        Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                        Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Signup(CrossSecureStorage.Current.GetValue("first_name"), CrossSecureStorage.Current.GetValue("last_name"), CrossSecureStorage.Current.GetValue("phone"))));

                        hideLoading();
                    }
                });
            }
            else
            {
                alert("", "", "ok");
                hideLoading();
            }
        }

        /* protected override bool OnBackButtonPressed()
         {

             if (Backcount >=3)
             {
                 return base.OnBackButtonPressed();
             }
             else
             {
                 XFToast.LongMessage("Press again to exit " + Backcount);
                 Backcount++;
             }
             return true;

         }*/
        public void showLoading()
        {
            // Show your overlay
            overlay.IsVisible = true;
            stack.IsVisible = false;
            aTimer = new System.Timers.Timer(15000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            // Hide the overlay

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            hideLoading();

        }
        public void hideLoading()
        {
            Device.BeginInvokeOnMainThread(() => overlay.IsVisible = false);
            Device.BeginInvokeOnMainThread(() => stack.IsVisible = true);
        }
        public void alert(String title, String message, String button)
        {
            //Device.BeginInvokeOnMainThread(() => DisplayAlert(title, message, button));
        }
        public Boolean isOnline()
        {
            return CrossConnectivity.Current.IsConnected;
        }
        public Boolean checkRespone(IRestResponse response)
        {
            Boolean status = false;
            if (response.Content == "ERROR")
            {
                alert("", "", "");
                Debug.WriteLine(response.Content);
                return status;


            }
            else if (response.Content == "505" || response.Content == "404")
            {
                alert("", "", "");
                return status;

            }
            else if (response.Content == "" || response.Content == null)
            {
                alert("", "", "");
                return status;

            }
            else if (response.Content.Contains("<span> Security by Cachewall </span>"))
            {
                alert("", "", "");
                return status;

            }
            else
            {
                status = true;
                return status;

            }

        }
    }
}
using TodoLocalized.Model;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
    public partial class exist_login
    {
        RandomInt random = new RandomInt();
        private static System.Timers.Timer aTimer;
        public exist_login()
        {
            InitializeComponent();
            login.Clicked += Login_Clicked;
            signup.Clicked += Signup_ClickedAsync;
        }

        private async void Signup_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup());
        }
        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            showLoading();
            var client = new RestClient("http://smartcare-health.com/phr/master/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);

            request.AddParameter("method", "m_l");
            request.AddParameter("pass", pass.Text);
            request.AddParameter("phone", phone.Text);


            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "999999999999999999999999999999");
                    if (checkRespone(response))
                    {
                        if (response.Content.Contains("true"))
                        {
                            Debug.WriteLine("im in");
                            CrossSecureStorage.Current.SetValue("id", response.Content.Replace("true", ""));
                            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Pages.Accounts()));


                        }
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

        public void showLoading()
        {


            // Show your overlay
            overlay.IsVisible = true;
            stack.IsVisible = false;
            aTimer = new System.Timers.Timer(60000);
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
            Device.BeginInvokeOnMainThread(() => DisplayAlert(title, message, button));
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
            else if (response.Content == "505")
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
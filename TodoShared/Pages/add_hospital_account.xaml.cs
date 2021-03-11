using TodoLocalized.Model;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace TodoLocalized.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class add_hospital_account
    {
        RandomInt random = new RandomInt();
        private static System.Timers.Timer aTimer;

        public add_hospital_account()
        {
            InitializeComponent();
            getAccounts();
            login.Clicked += Login_Clicked;
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            var client = new RestClient("http://smartcare-health.com/phr/master/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);

            request.AddParameter("method", "add_account");
            request.AddParameter("id", id.Text);
            request.AddParameter("hPass", pass.Text);
            request.AddParameter("account_type", accountType.SelectedIndex);

            request.AddParameter("user_id", CrossSecureStorage.Current.GetValue("id"));


            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "999999999999999999999999999999" + accountType.SelectedItem + "99999999999" + accountType.SelectedIndex);
                    if (checkRespone(response))
                    {
                        List<string> types = new List<string>();
                        Debug.WriteLine(response.Content + "==========================");

                        Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Verification("add_account")));
                        CrossSecureStorage.Current.SetValue("hid", id.Text);
                        CrossSecureStorage.Current.SetValue("hPass", pass.Text);

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

        public void getAccounts()
        {
            showLoading();
            var client = new RestClient("http://smartcare-health.com/phr/master/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);

            request.AddParameter("method", "g_account_types");

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "999999999999999999999999999999");
                    if (checkRespone(response))
                    {
                        List<string> types = new List<string>();
                        Debug.WriteLine(response.Content + "==========================");

                        JArray ja = JArray.Parse(response.Content);
                        for (int x = 0; x < ja.Count; x++)
                        {
                            Debug.WriteLine("1111111111111111111111111111" + ja[x]["type_description"].ToString());
                            types.Add(ja[x]["type_description"].ToString());
                        }
                        Device.BeginInvokeOnMainThread(() => accountType.ItemsSource = types);

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
        public void alert(string title, string message, string button)
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
            else if (response.Content == "404")
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
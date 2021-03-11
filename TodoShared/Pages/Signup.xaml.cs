using TodoLocalized.Model;
using Newtonsoft.Json.Linq;
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

    public partial class Signup
    {
        RandomInt random = new RandomInt();
        private static System.Timers.Timer aTimer;
        public Signup()
        {
            InitializeComponent();
            signup.Clicked += Signup_Clicked;
        }
        public Signup(String first_name, String last_name, String phone)
        {
            InitializeComponent();
            first.Text = first_name;
            last.Text = last_name;
            this.phone.Text = phone;
            signup.Clicked += Signup_Clicked;
            if (CrossSecureStorage.Current.HasKey("hid")) { get_account_types(); accountType.IsVisible = true; }

        }
        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
        private void get_account_types()
        {
            showLoading();
            /*    JArray ja = JArray.Parse("[{'id':'1','type_description':'mother'},{'id':'2','type_description':'father'},{'id':'3','type_description':'grandfather'},{'id':'4','type_description':'grandmother'},{'id':'5','type_description':'baby'},{'id':'6','type_description':'boy'},{'id':'7','type_description':'girl'}]");
                setAccounts(ja);*/
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
                        Debug.WriteLine("sssssssssssssssssssssssssssssssssssssssssss");
                        JArray ja = JArray.Parse(response.Content);
                        setAccounts(ja);
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
        private void setAccounts(JArray ja)
        {
            List<String> list = new List<string>();
            Debug.WriteLine("mohanedmohanedfetgfs" + ja[0]);
            for (int x = 0; x < ja.Count; x++)
            {
                Debug.WriteLine(ja[x]["type_description"].ToString() + "000000000000");
                list.Add(ja[x]["type_description"].ToString());
            }
            Device.BeginInvokeOnMainThread(() => accountType.ItemsSource = list);
            hideLoading();

        }
        private void Signup_Clicked(object sender, EventArgs e)
        {
            showLoading();
            var client = new RestClient("http://smartcare-health.com/phr/master/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);

            if (CrossSecureStorage.Current.HasKey("hid"))
            {
                request.AddParameter("method", "m_s_h");
                request.AddParameter("id", CrossSecureStorage.Current.GetValue("hid"));
                request.AddParameter("hPass", CrossSecureStorage.Current.GetValue("Hpass"));

            }
            else
            {
                request.AddParameter("method", "m_s");
            }
            request.AddParameter("password", pass.Text);
            request.AddParameter("phone", phone.Text);
            request.AddParameter("email", email.Text);
            request.AddParameter("gender", "male");
            request.AddParameter("birth_date", bdate.Date);
            request.AddParameter("first_name", first.Text);
            request.AddParameter("last_name", last.Text);


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
                            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());

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
            //   Device.BeginInvokeOnMainThread(() => DisplayAlert(title, message, button));
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
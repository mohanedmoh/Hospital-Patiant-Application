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
    public partial class Login
    {
        RandomInt random = new RandomInt();
        private static System.Timers.Timer aTimer;
        public Login()
        {
            InitializeComponent();
            // XFToast.LongMessage("Please fill all fields !");
            //  Signup.Clicked+=openSignupAsync;
            int x = 1;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            Debug.WriteLine(x / 2 + "|||||||||||||||||||");
            login.Clicked += Login_Clicked;
            exist_login.Clicked += Exist_login_ClickedAsync;
            forget.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Forget_Clicked();
                })
            });
        }

        private async void Exist_login_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.exist_login());
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            if (id.Text == "" || pass.Text == null || pass.Text == "" || id.Text == null)
            {
                XFToast.LongMessage("Please Fill All Fields !!");
            }
            else
            {
                showLoading();
                var client = new RestClient("http://smartcare-health.com/phr/master/method.php?x=" + random.rand());
                var request = new RestRequest("resource/{id}", Method.POST);

                request.AddParameter("method", "check_user_hospital");
                request.AddParameter("pass", pass.Text);
                request.AddParameter("id", id.Text);



                if (isOnline())
                {
                    client.ExecuteAsync(request, response =>
                    {
                        response.Content = response.Content.Trim();
                        Debug.WriteLine("############" + response.Content + "999999999999999999999999999999");
                        if (checkRespone(response))
                        {
                            Debug.WriteLine("============");
                            JArray ja = JArray.Parse(response.Content);
                            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Verification("first_login")));
                            CrossSecureStorage.Current.SetValue("hid", id.Text);
                            CrossSecureStorage.Current.SetValue("Hpass", pass.Text);
                            CrossSecureStorage.Current.SetValue("first_name", ja[0]["first_name"].ToString());
                            CrossSecureStorage.Current.SetValue("last_name", ja[0]["last_name"].ToString());
                            CrossSecureStorage.Current.SetValue("phone", ja[0]["phone"].ToString());


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


        }
        private void Forget_Clicked() { }
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
            //  Device.BeginInvokeOnMainThread(() => DisplayAlert(title, message, button));
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
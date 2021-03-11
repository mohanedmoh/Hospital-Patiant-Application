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
using NControl.Controls;
using System.Collections.Generic;
using TodoLocalized.Database;
using System.Collections.ObjectModel;
using TodoLocalized.Model.DbModel;

namespace TodoLocalized.Pages
{
    public partial class Accounts : ContentPage
    {
        RandomInt random = new RandomInt();
        //bool done = false;
        private static System.Timers.Timer aTimer;


        public Accounts()
        {
            InitializeComponent();

            /* bigstack.HeightRequest = relative.Height;
               bigstack.WidthRequest = relative.WidthRequest;
               relative.Children.Add(makeFAB(), () => new Rectangle(((relative.Width / 4) * 3)+10 , (relative.Height /1.15) - (200), 56, 250));
            */
            getAccountsAsync();

        }
        public void gridcook(List<IAccounts> ja)
        {
            Debug.WriteLine("11111111111111111111111111111111"+ja);

            this.Content.BackgroundColor = Color.FromHex("#ecf0f1");
            Debug.WriteLine("1");

            StackLayout all = new StackLayout() { Spacing = 10, BackgroundColor = Color.FromHex("#ecf0f1") };
            StackLayout row;
            StackLayout single;
            Debug.WriteLine("2");

            int accountN = ja.Count;
            int c = 0;
            int countall = accountN;
            Debug.WriteLine("3"+accountN);
            
            if (countall % 2 != 0)
            {
                countall++;
                if (countall == 1) { countall = 2; }
            };
            Debug.WriteLine("4" + countall);
           // if (countall == 0) countall = 2;
            for (int x = 0; x < countall / 2; x++)
            {
                Debug.WriteLine("2");

                row = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 6, BackgroundColor = Color.Transparent, Margin = new Thickness(2, 2, 2, 2) };
                for (int y = 0; y < 2; y++)
                {
                    Image i = new Image();
                    Label l = new Label();
                    if (x == (countall / 2) - 1 && accountN % 2 != 0 && y != 0)
                    {
                        Debug.WriteLine("inside shadeed");

                        single = new StackLayout() { Orientation = StackOrientation.Vertical };
                        i.Source = "http://icons.iconarchive.com/icons/paomedia/small-n-flat/1024/sign-add-icon.png";
                        i.HeightRequest = 90;
                        i.WidthRequest = 125;
                        i.HorizontalOptions = LayoutOptions.Center;
                        l.Text = "Add";
                        l.HorizontalOptions = LayoutOptions.Center;
                        l.VerticalOptions = LayoutOptions.Center;
                        single.Children.Add(i);
                        single.Children.Add(l);
                        single.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = new Command(() =>
                            {
                                Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new add_hospital_account()));
                            })
                        });
                        row.Children.Add(new Frame() { CornerRadius = 5, HasShadow = true, Content = single, Margin = 0 });

                    }
                    else
                    {
                        single = new StackLayout() { Orientation = StackOrientation.Vertical };
                        Label hospital = new Label() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = ja[c].hospital_name.ToString() };
                        i.Source = ja[c].type_description + ".png";
                        i.HeightRequest = 90;
                        i.HorizontalOptions = LayoutOptions.Center;
                        l.Text = ja[c].first_name + " " + ja[c].last_name;
                        String id = ja[c].user_hospital_id.ToString();
                        String pass = ja[c].password.ToString();
                        String account_id = ja[c].id.ToString();
                        String url = ja[c].hospital_url;
                        l.HorizontalOptions = LayoutOptions.Center;
                        l.VerticalOptions = LayoutOptions.Center;
                        single.Children.Add(i);
                        single.Children.Add(l);

                        single.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = new Command(() =>
                            {
                                Debug.WriteLine(ja.ToString(), ";;;;;;;;;;" + c);
                                Device.BeginInvokeOnMainThread(() => open_master(account_id, id, pass,url));
                            })
                        });
                        row.Children.Add(new Frame() { CornerRadius = 5, HasShadow = true, Content = single, Margin = 0 });
                    }
                    c++;
                }
                all.Children.Add(row);
                if (x == (countall / 2) - 1 && accountN % 2 == 0)
                {
                    row = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 6, BackgroundColor = Color.Transparent, Margin = new Thickness(2, 2, 2, 4) };
                    single = new StackLayout() { Orientation = StackOrientation.Vertical };
                    Image i = new Image();
                    i.Source = "http://icons.iconarchive.com/icons/paomedia/small-n-flat/1024/sign-add-icon.png";
                    i.HeightRequest = 90;
                    i.WidthRequest = 125;

                    Label l = new Label();
                    l.Text = "Add";
                    l.HorizontalOptions = LayoutOptions.Center;
                    l.VerticalOptions = LayoutOptions.Center;
                    single.Children.Add(i);
                    single.Children.Add(l);
                    single.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new add_hospital_account()));
                        })
                    });
                    row.Children.Add(new Frame() { CornerRadius = 5, HasShadow = true, Content = single, Margin = 0 });
                    all.Children.Add(row);
                }
            }
            CardsScroll.Content = all;
            hideLoading();
        }

        private void open_master(String account_id,string user_hospital_id, string user_password, String url)
        {
            CrossSecureStorage.Current.SetValue("account_id", account_id);
            CrossSecureStorage.Current.SetValue("hid", user_hospital_id);
            CrossSecureStorage.Current.SetValue("Hpass", user_password);
            CrossSecureStorage.Current.SetValue("url", url);
            Application.Current.MainPage = new NavigationPage(new Master_Pages.main());
        }
        public void getUrl (int hospital_id){

        }
        public void getLabData(int id, String url, int hospital_id, String Hpass) {
            IList<ExerciseClass> allData = new ObservableCollection<ExerciseClass>();
            var client = new RestClient("http://" + url + ".php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("method", "g_l");
            request.AddParameter("id", hospital_id);
            request.AddParameter("pass", Hpass);


            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + ":::::::::::::::");
                    if (checkRespone(response))
                    {
                        Debug.WriteLine("66666666666666666666666666");
                        JArray ja = JArray.Parse(response.Content);
                        saveLabDataAsync(ja, id);

                    }
                });
            }
            else
            {
                alert("", "", "ok");
                hideLoading();
            }
        }
        public void getPharmacyData(int id, String url,int hospital_id,String Hpass)
        {
            Debug.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@"+ url + ".php?x=" + random.rand());
            IList<pharmacy> allData = new ObservableCollection<pharmacy>();
            var client = new RestClient("http://" + url+".php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("method", "g_ph");
            request.AddParameter("id", hospital_id);
            request.AddParameter("pass", Hpass);

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "{{{{{{{{{{{{{{{{{{{{{{{{{{{{{");
                    if (checkRespone(response))
                    {
                        Debug.WriteLine("sssssssssssssssssssssssssssssssssssssssssss");
                        JArray ja = JArray.Parse(response.Content);
                        savePharmacyDataAsync(ja,id);

                    }
                });
            }
            else
            {
                alert("", "", "ok");
                hideLoading();
            }
        }
        public async void saveLabDataAsync(JArray ja,int account_id) {
            for (int z=0;z<ja.Count;z++) {
                await App.Database.SaveLabAsync(new ILab()
                {
                    result_id = (int)ja[z]["id"],
                    test_name = ja[z]["test_name"].ToString(),
                    result = ja[z]["result"].ToString(),
                    RV = ja[z]["RV"].ToString(),
                    account_id = account_id,
                    unit = ja[z]["unit"].ToString(),
                    date = ja[z]["date"].ToString(),
                    analysis_name = ja[z]["analysis_name"].ToString(),
                    reviewed = ja[z]["reviewed"].ToString(),
                });
            }
        }
        public async void savePharmacyDataAsync(JArray ja, int account_id)
        {
            for (int z = 0; z < ja.Count; z++)
            {
                await App.Database.SaveItemAsync(new Ipharmacy()
                {
                    M_id = (int)ja[z]["id"],
                    item_des = ja[z]["item_des"].ToString(),
                    notes = ja[z]["notes"].ToString(),
                    account_id = account_id,
                    dose = ja[z]["dose"].ToString(),
                    date = ja[z]["date"].ToString(),
                });
            }
        }
        public async void getAccountsAsync()
        {
            await App.checkFirstAsync();
           // Debug.WriteLine("##############" + await App.checkStatus("sync_accounts"));
            showLoading();
            if ( await App.checkStatus("sync_accounts"))
            {
            var client = new RestClient("http://smartcare-health.com/phr/master/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);

            request.AddParameter("method", "g_u_a");
            request.AddParameter("id", CrossSecureStorage.Current.GetValue("id"));

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "999999999999999999999999999999" + CrossSecureStorage.Current.GetValue("id"));
                    if (checkRespone(response))
                    {

                        JArray ja = JArray.Parse(response.Content);
                        
                            // Debug.WriteLine("im in");
                            // CrossSecureStorage.Current.SetValue("id", response.Content.Replace("true", ""));
                            saveAccountsOffAsync(ja);

                            // Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Pages.Accounts()));
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
             else
             {
                List<IAccounts> list = await App.Database.GetAccountsAsync();
                DrawAccountsOff(list);
             }
        }
        public async void saveAccountsOffAsync(JArray ja) {
            for (int x=0;x<ja.Count;x++)
            {
                await App.Database.SaveAccountAsync(new Database.IAccounts() { id = 0, user_hospital_id = int.Parse(ja[x]["user_hospital_id"].ToString())
                    , hospital_name = ja[x]["hospital_name"].ToString()
                    , hospital_url = ja[x]["hospital_url"].ToString()
                    ,password=ja[x]["password"].ToString()
                    ,first_name=ja[x]["first_name"].ToString()
                    ,last_name=ja[x]["last_name"].ToString()
                    ,type_description=ja[x]["type_description"].ToString() });  
            }
            List<IAccounts> list = await App.Database.GetAccountsAsync();
            for (int x = 0; x < ja.Count; x++)
            {
              getPharmacyData(list[x].id,list[x].hospital_url,list[x].user_hospital_id,list[x].password);
            }
            for (int y = 0; y < ja.Count; y++) {
                getLabData(list[y].id, list[y].hospital_url, list[y].user_hospital_id, list[y].password);

            }
            App.changeStatus("sync_accounts");
            App.getAlarmsAsync();
            DrawAccountsOff(list);

        }
        
        public void DrawAccountsOff(List<IAccounts> list)
        {
            //List<IAccounts> list = await App.Database.GetAccountsAsync();
            Device.BeginInvokeOnMainThread(() => gridcook(list));
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

using NControl.Controls;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TodoLocalized.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TodoLocalized.Master_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class main : MasterDetailPage
    {
        RandomInt random = new RandomInt();
        class TimerExampleState
        {
            public int counter = 0;

        }
        Master m;
        public main()
        {
            m = new Master();
            this.Master = m;

            Device.BeginInvokeOnMainThread(() => this.Detail = new Pages.lab_results());
            InitializeComponent();
            getPickersAsync();
            
            m.ListView.ItemSelected += OnItemSelected;
            // NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

        }
       
        public async void getPickersAsync() {

           
            var client = new RestClient("http://smartcare-health.com/phr/hospital/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            List<String> list = new List<String>();
            request.AddParameter("method", "get_clinic");
            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine("$$$$$$$$$$$$$$$$$$"+response.Content);
                    if (checkRespone(response))
                    {
                        Debug.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%5"+response.Content);
                        CrossSecureStorage.Current.SetValue("clinic_picker", response.Content);
                    }
                });
            }
            else
            {
                alert("", "", "ok");

            }
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

        /*public void refreshMaster(Object o)
        {
            if (Application.Current.Properties.ContainsKey("lang") && Application.Current.Properties["lang"] != null)
            {
                Debug.WriteLine("hfsdffffffdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
                m = new Master();
                Device.BeginInvokeOnMainThread(() => { this.Master = m; });
                Application.Current.Properties["lang"] = null;
                m.ListView.ItemSelected += OnItemSelected;

            }
        }*/
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterPageItem item)
            {
                if (item.Title == "Sign out")
                {
                    CrossSecureStorage.Current.SetValue("hid", "");
                    CrossSecureStorage.Current.SetValue("Hpass", "");
                    CrossSecureStorage.Current.SetValue("id", "");
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Pages.Login()));
                }
                else
                {
                    Detail = ((Page)Activator.CreateInstance(item.TargetType));
                    this.Title = item.Title;
                    m.ListView.SelectedItem = null;
                    IsPresented = false;
                }

            }
        }

        private IList<pharmacy> getPhData()
        {
            IList<pharmacy> allData = new ObservableCollection<pharmacy>();
            var client = new RestClient("http://smartcare-health.com/phr/hospital/method.php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("method", "g_ph");
            request.AddParameter("id", CrossSecureStorage.Current.GetValue("hid"));
            request.AddParameter("pass", CrossSecureStorage.Current.GetValue("Hpass"));

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content + "999999999999999999999999999999");
                    if (checkRespone(response))
                    {
                        Debug.WriteLine("sssssssssssssssssssssssssssssssssssssssssss");
                        JArray ja = JArray.Parse(response.Content);
                        Debug.WriteLine("4444444444444444444444" + ja.Count);
                        DateTime date;
                        bool color = false;
                        for (int y = ja.Count - 1; y >= 0; y--)
                        {
                            IList<pharmacy> day = new ObservableCollection<pharmacy>();
                            if (!ja[y]["date"].ToString().Contains("null"))
                            {
                                Debug.WriteLine("=======================" + y + "77777777777777" + ja[y]);
                                date = DateTime.Parse(ja[y]["date"].ToString());
                                pharmacy exDay = new pharmacy() { date = date.Year + "/" + date.Month + "/" + date.Day };
                                Debug.WriteLine("555555555555555555" + date.Date);
                                for (int z = 0; z <= y; z++)
                                {
                                    if (((DateTime)ja[z]["date"]).Date == date.Date && !ja[z]["date"].ToString().Contains("null"))
                                    {
                                        day.Add(new pharmacy()
                                        {
                                            id = (int)ja[z]["id"],
                                            item_des = ja[z]["item_des"].ToString(),
                                            notes = ja[z]["notes"].ToString(),
                                            dose = ja[z]["dose"].ToString(),
                                            date = ja[z]["date"].ToString(),

                                        });
                                        /* await App.Database.SaveItemAsync(new Ipharmacy()
                                         {
                                             id = (int)ja[z]["id"],
                                             item_des = ja[z]["item_des"].ToString(),
                                             notes = ja[z]["notes"].ToString(),
                                             dose = ja[z]["dose"].ToString(),
                                             date = ja[z]["date"].ToString(),

                                         });*/
                                        ja[z]["date"] = "null";
                                        if (z == y)
                                        {
                                            Debug.WriteLine("1111111111111111111111");
                                            exDay.day = day;
                                            exDay.length = day.Count * 80;
                                            exDay.color = (color ? "#ecf0f1" : "#ffffff");
                                            color = !color;


                                            allData.Add(exDay);
                                        }
                                    }
                                }
                            }

                        };
                        //hideLoading();

                    }
                });
            }
            else
            {
                alert("", "", "ok");
               // hideLoading();
            }
            return allData;

        }


    /*    public void showLoading()
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

        }*/

        /*private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            hideLoading();

        }*/
       /* public void hideLoading()
        {
            Device.BeginInvokeOnMainThread(() => overlay.IsVisible = false);
            Device.BeginInvokeOnMainThread(() => stack.IsVisible = true);
        }*/
       
       
      


    }
}
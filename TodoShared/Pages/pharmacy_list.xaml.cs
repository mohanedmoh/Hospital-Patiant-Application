using TodoLocalized.Model;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TodoLocalized.Database;
using TodoLocalized.Model.DbModel;
using NControl.Controls;

namespace TodoLocalized.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pharmacy_list : ContentPage
    {
        private static System.Timers.Timer aTimer;
        RandomInt random = new RandomInt();


        public pharmacy_list()
        {
            InitializeComponent();
            //  stack.ItemSelected += Stack_ItemSelected;
            bigstack.HeightRequest = relative.Height;
            bigstack.WidthRequest = relative.WidthRequest;
            ActionButton ab = makeFAB();
            ab.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    openAddForm("Pharmacy");
                })
            });
            ab.BackgroundColor = Color.Transparent;
            ab.HeightRequest = 80;
            ab.HasShadow = true;
            relative.Children.Add(ab, () => new Rectangle(((relative.Width / 4) * 3) + 10, (relative.Height / 1) - (70), 70, 70));

            GetPhDataAsync();
        }
       
        public async void sub_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new Popups.pharmaAlarm((pharmacy)e.SelectedItem)));
            //((ListView)sender).SelectedItem = null;
        }

        private void Stack_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            stack.SelectedItem = null;

           // throw new NotImplementedException();
        }
        public ActionButton makeFAB()
        {
            var abex = new ActionButton
            {
                ButtonColor = Color.FromHex("#485992"),
                HeightRequest = 32,
                HasShadow = false,
                Command = new Command(() =>
                {
                    openAddForm("Pharmacy");
                }),


                ButtonIcon = FontAwesomeLabel.FAMedkit,
            };

            return abex;
        }
        public void openAddForm(String id)
        {
            Navigation.PushAsync(new Pages.manualResultForm(id));
        }
        private async void GetPhDataAsync()
        {
            List<Ipharmacy> list = await App.Database.GetItemAsyncByAccountID(int.Parse(CrossSecureStorage.Current.GetValue("account_id")));
            Device.BeginInvokeOnMainThread(() => BindingContext = drawTimeLine(list));





        }

        public IList<pharmacy> drawTimeLine(List<Ipharmacy> ja) {
            IList<pharmacy> allData = new ObservableCollection<pharmacy>();

            Debug.WriteLine("4444444444444444444444" + ja.Count);
            DateTime date;
            bool color = false;
            for (int y = ja.Count - 1; y >= 0; y--)
            {
                IList<pharmacy> day = new ObservableCollection<pharmacy>();
                if (!ja[y].date.ToString().Contains("null"))
                {
                    Debug.WriteLine("=======================" + y + "77777777777777" + ja[y]);
                    date = DateTime.Parse(ja[y].date.ToString());
                    pharmacy exDay = new pharmacy() { date = date.Year + "/" + date.Month + "/" + date.Day };
                    Debug.WriteLine("555555555555555555" + date.Date);
                    for (int z = 0; z <= y; z++)
                    {
                        if ((Convert.ToDateTime(ja[z].date)).Date == date.Date && !ja[z].date.ToString().Contains("null"))
                        {
                            day.Add(new pharmacy()
                            {
                                id = (int)ja[z].M_id,
                                item_des = ja[z].item_des.ToString(),
                                notes = ja[z].notes.ToString(),
                                dose = ja[z].dose.ToString(),
                                date = ja[z].date.ToString(),

                            });
                          
                            ja[z].date = "null";
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
            hideLoading();
            return allData;
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
            // Device.BeginInvokeOnMainThread(() => DisplayAlert(title, message, button));
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
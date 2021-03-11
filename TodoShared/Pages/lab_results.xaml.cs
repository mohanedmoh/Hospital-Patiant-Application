using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLocalized.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using Plugin.Connectivity;
using RestSharp;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Plugin.SecureStorage;
using TodoLocalized.Model.DbModel;
using NControl.Controls;

namespace TodoLocalized.Pages
{
    public partial class lab_results : ContentPage
    {
        private static System.Timers.Timer aTimer;
        RandomInt random = new RandomInt();


        public lab_results()
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
                    openAddForm("Lab");
                })
            });
            ab.BackgroundColor = Color.Transparent;
            ab.HeightRequest = 80;
            relative.Children.Add(ab, () => new Rectangle(((relative.Width / 4) * 3) + 10, (relative.Height / 1) - (70), 70,70));

            getLabDataOffAsync();
        }

        private async void sub_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new Popups.Sub((ExerciseClass)e.SelectedItem)));
        }
        public void openAddForm(String id)
        {
            Navigation.PushAsync(new Pages.manualResultForm(id));
        }
        private void Stack_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            throw new NotImplementedException();
        }
        private async void getLabDataOffAsync()
        {

            List<ILab> list = await App.Database.GetLabAsyncByAccountID(int.Parse(CrossSecureStorage.Current.GetValue("account_id")));
            Device.BeginInvokeOnMainThread(() => BindingContext = drawTimeLine(list));
        }
       
        private IList<ExerciseClass> drawTimeLine(List<ILab> ja) {
            IList<ExerciseClass> allData = new ObservableCollection<ExerciseClass>();

            Debug.WriteLine("4444444444444444444444" + ja.Count);
            DateTime date;
            bool color = false;
            for (int y = ja.Count - 1; y >= 0; y--)
            {
                IList<ExerciseClass> day = new ObservableCollection<ExerciseClass>();
                if (!ja[y].date.ToString().Contains("null"))
                {
                    Debug.WriteLine("=======================" + y + "77777777777777" + ja[y]);
                    date = DateTime.Parse(ja[y].date.ToString());
                    ExerciseClass exDay = new ExerciseClass() { date = date.Year + "/" + date.Month + "/" + date.Day };
                    Debug.WriteLine("555555555555555555" + date.Date);
                    for (int z = 0; z <= y; z++)
                    {
                        if ((Convert.ToDateTime(ja[z].date).Date == date.Date && !ja[z].date.ToString().Contains("null")))
                        {
                            day.Add(new ExerciseClass()
                            {
                                id = (int)ja[z].result_id,
                                test_name = ja[z].test_name.ToString(),
                                result = ja[z].result.ToString(),
                                RV = ja[z].RV.ToString(),
                                unit = ja[z].unit.ToString(),
                                date = ja[z].date.ToString(),
                                analysis_name = ja[z].analysis_name.ToString(),
                                reviewed = ja[z].reviewed.ToString(),
                            });
                            ja[z].date = "null";
                            if (z == y)
                            {
                                Debug.WriteLine("1111111111111111111111");
                                exDay.ex = day;
                                exDay.length = day.Count * 80;
                                exDay.color = (color ? "#ecf0f1" : "#ffffff");
                                color = !color;
                                allData.Add(exDay);
                            }
                        }
                    }
                }

            }
            return allData;
        }
        private IList<ExerciseClass> fixData(JArray ja)
        {
            IList<ExerciseClass> allData = new ObservableCollection<ExerciseClass>();

            Debug.WriteLine("4444444444444444444444" + ja.Count);
            DateTime date;
            for (int y = ja.Count - 1; y >= 0; y--)
            {
                IList<ExerciseClass> day = new ObservableCollection<ExerciseClass>();
                if (!ja[y]["date"].ToString().Contains("null"))
                {
                    Debug.WriteLine("=======================" + y + "77777777777777" + ja[y]);
                    date = DateTime.Parse(ja[y]["date"].ToString());
                    ExerciseClass exDay = new ExerciseClass() { date = date.Year + "/" + date.Month + "/" + date.Day };
                    Debug.WriteLine("555555555555555555" + date.Date);
                    for (int z = 0; z <= y; z++)
                    {
                        if (((DateTime)ja[z]["date"]).Date == date.Date && !ja[z]["date"].ToString().Contains("null"))
                        {
                            day.Add(new ExerciseClass()
                            {
                                id = (int)ja[z]["id"],
                                test_name = ja[z]["test_name"].ToString(),
                                result = ja[z]["result"].ToString(),
                                RV = ja[z]["RV"].ToString(),
                                unit = ja[z]["unit"].ToString(),
                                date = ja[z]["date"].ToString(),
                                analysis_name = ja[z]["analysis_name"].ToString(),
                                reviewed = ja[z]["reviewed"].ToString(),
                            });
                            ja[z]["date"] = "null";
                            if (z == y)
                            {
                                Debug.WriteLine("1111111111111111111111");
                                exDay.ex = day;
                                allData.Add(exDay);
                            }
                        }
                    }
                }

            }
            return allData;

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
                    openAddForm("Lab");
                }),


                ButtonIcon = FontAwesomeLabel.FAMedkit,
            };
            
            return abex;
        }
        public void showLoading()
        {


            // Show your overlay
          //  overlay.IsVisible = true;
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
           // Device.BeginInvokeOnMainThread(() => overlay.IsVisible = false);
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
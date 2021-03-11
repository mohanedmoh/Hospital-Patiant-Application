using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using TodoLocalized.Model;
using TodoLocalized.Pages.Cards;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class search_dr : ContentPage
    {

        RandomInt random = new RandomInt();
        bool done = false;
        List<String> list;
        List<int> listId;


        public search_dr ()
		{
			InitializeComponent ();
            fillPicker();
            PickerCtl.SelectedIndexChanged += PickerCtl_SelectedIndexChanged;
            this.Content.BackgroundColor = Color.FromHex("#ecf0f1");


        }

        private void PickerCtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(PickerCtl.SelectedIndex + "****************************" + listId[PickerCtl.SelectedIndex]);
            getCardData(listId[PickerCtl.SelectedIndex].ToString());
        }

        public void getCardData(String id)
        {

            var client = new RestClient("http://" + CrossSecureStorage.Current.GetValue("url") + ".php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            List<String> list = new List<String>();
            request.AddParameter("method", "get_dr");
            request.AddParameter("clinic_id",id );
            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine("###############"+response.Content);
                    if (checkRespone(response))
                    {
                        JArray ja = JArray.Parse(response.Content);
                        List<doctor> doctors = new List<doctor>();
                        for (int x = ja.Count - 1; x >= 0; x--)
                        {
                            String temp = ja[x]["emp_name"].ToString();
                            if (temp != "null") {
                                List<day_week> listDay = new List<day_week>();

                                for (int y = x; y >= 0; y--)
                                {
                                    if (temp != "null" && ja[y]["emp_name"].ToString() == temp)
                                    {
                                        DateTime.TryParse(ja[y]["start_time"].ToString(), out DateTime dts);
                                        DateTime.TryParse(ja[y]["end_time"].ToString(), out DateTime dte);

                                        listDay.Add(new day_week() { emp_id=ja[y]["emp_id"].ToString(),day_doc_id= ja[y]["day_doc_id"].ToString(),day_of_week=getDayName(ja[y]["day_of_week"].ToString()),start_time=dts.ToString("HH:mm"),end_time=dte.ToString("HH:mm"),shift= ja[y]["shift"].ToString() });
                                        ja[y]["emp_name"] = "null";
                                    }
                                }
                                doctors.Add(new doctor() { name = temp, hospital_name = "مدينة البراحه الطبيه", clinic_name = ja[x]["CLINIC_TYPE_NAME"].ToString(), image = "doctor.png",listDay=listDay });

                            }
                        }
                        getList(doctors);
                    }
                });
            }
            else
            {
                alert("", "", "ok");
              
            }
            
        }
        public String getDayName(String s) {
            switch (s)
            {
                case "0":
                    { return "الأحد"; }
                case "1":
                    { return "الأثنين"; }
                case "2":
                    { return "الثلاثاء"; }
                case "3":
                    { return "الأربعاء"; }
                case "4":
                    { return "الخميس"; }
                case "5":
                    { return "الجمعة"; }
                case "6":
                    { return "السبت"; }
            }
            return "";
        }
        public void fillPicker() {
            list = new List<String>();
            listId = new List<int>();
            if (CrossSecureStorage.Current.HasKey("clinic_picker"))
            {
                Debug.WriteLine("^^^^^^^^^^"+ CrossSecureStorage.Current.GetValue("clinic_picker").ToString());
                JArray ja = JArray.Parse(CrossSecureStorage.Current.GetValue("clinic_picker").ToString());

                for (int x = 0; x < ja.Count; x++)
                {
                    list.Add(ja[x]["CLINIC_TYPE_NAME"].ToString());
                    listId.Add((int)(ja[x]["CLINIC_TYPE_ID"]));
                }
                Device.BeginInvokeOnMainThread(() => PickerCtl.ItemsSource = list);
            }
            else { }

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

        public void getList(List<doctor> list)
        {
            Debug.WriteLine("qqqqqqqqqqqqq" + list.Count());
            ObservableCollection<Card> items = new ObservableCollection<Card>();
            for (int i = 0; i < list.Count(); i++)
            {
                items.Add(new Card()
                {
                    name = list[i].name,
                    hospital = list[i].hospital_name,
                    clinic = list[i].clinic_name,
                    Status = CardStatus.Alert,
                    listD = list[i].listDay,
                    Source = list[i].image,
                });
            }
            cookCards(items);
        }
        public void cookCards(ObservableCollection<Card> cards)
        {

            var cardstack = new StackLayout
            {
                Spacing = 15,
                Padding = new Thickness(10),
                VerticalOptions = LayoutOptions.StartAndExpand,
            };


            foreach (var card in cards)
            {

                CardView cv = new CardView(card);

                cardstack.Children.Add(cv);
            }





            Device.BeginInvokeOnMainThread(() =>

         Device.BeginInvokeOnMainThread(() => CardsScroll.Content = new StackLayout()
         {
             Children =
             {
                    cardstack,
                 }
         }));

            done = true;

        }
    }
}
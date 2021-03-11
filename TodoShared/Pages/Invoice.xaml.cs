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
using TodoLocalized.Model;
using TodoLocalized.Pages.invoices_card;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Invoice : ContentPage
	{
        RandomInt random = new RandomInt();
        public Invoice ()
		{

			InitializeComponent ();
            Debug.WriteLine( CrossSecureStorage.Current.GetValue("url")+"VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            getCardData();

        }
        public void getCardData()
        {

            var client = new RestClient("http://" + CrossSecureStorage.Current.GetValue("url") + ".php?x="  + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            List<String> list = new List<String>();
            request.AddParameter("method", "get_invoices");
            request.AddParameter("id", CrossSecureStorage.Current.GetValue("hid"));
            request.AddParameter("pass", CrossSecureStorage.Current.GetValue("Hpass"));

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine("###############" + response.Content);
                    if (checkRespone(response))
                    {
                       
                        JArray ja = JArray.Parse(response.Content);
                        List<invoice> invoices = new List<invoice>();
                        for (int x = 0; x < ja.Count; x++)
                        {
                           invoices.Add(new invoice() {id=ja[x]["ticket_id"].ToString(), invoice_name = ja[x]["issued_from"].ToString(), entered_date = ja[x]["entered_date"].ToString(), paid_amount = ja[x]["paid_amount"].ToString(), req_amount = ja[x]["req_amount"].ToString(), total_amount = ja[x]["total_amount"].ToString(), status = ja[x]["CONFIRM_STATUS"].ToString() });
                        }
                        Debug.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                        getList(invoices);
                        Debug.WriteLine(2);
                    }
                });
            }
            else
            {
                alert("", "", "ok");

            }

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
        public void alert(String title, String message, String button)
        {
            //  Device.BeginInvokeOnMainThread(() => DisplayAlert(title, message, button));
        }
        public void getList(List<invoice> list)
        {
            Debug.WriteLine("qqqqqqqqqqqqq" + list.Count());
            ObservableCollection<Card> items = new ObservableCollection<Card>();
            for (int i = 0; i < list.Count(); i++)
            {
                items.Add(new Card()
                {
                    id=list[i].id,
                    invoice_name = list[i].invoice_name,
                    date = list[i].entered_date,
                    total_amount=list[i].total_amount,
                    paid_amount=list[i].paid_amount,
                    status=list[i].status,
                    req_amount = list[i].req_amount,
                    Source= "billb.png",

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







            Device.BeginInvokeOnMainThread(() => CardsScroll.Content = new StackLayout()
            {
                Children = { cardstack, }
               
            });

          

        }
    }
}

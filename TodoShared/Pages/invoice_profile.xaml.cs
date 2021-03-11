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
using TodoLocalized.Model;
using TodoLocalized.Pages.invoices_card;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class invoice_profile : ContentPage
	{
        RandomInt random = new RandomInt();
        public invoice_profile ()
		{
			InitializeComponent ();
		}
        public invoice_profile(Card card)
        {
            InitializeComponent();
            getTableData(card.id);
            Device.BeginInvokeOnMainThread(() => setText(card));
        }
        public void setText(Card card)
        {
            nameL.Text = card.invoice_name;
            total.Text += card.total_amount;
            paid.Text += card.paid_amount;
            requested.Text += card.req_amount;
            date.Text += card.date;
            status.Text += card.status;



        }
        public void getTableData(String id)
        {

            var client = new RestClient("http://" + CrossSecureStorage.Current.GetValue("url") + ".php?x=" + random.rand());
            var request = new RestRequest("resource/{id}", Method.POST);
            List<String> list = new List<String>();
            request.AddParameter("method", "get_invoice_details");
            request.AddParameter("invoice_id", id);

            if (isOnline())
            {
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine("###############" + response.Content);
                    if (checkRespone(response))
                    {

                        JArray ja = JArray.Parse(response.Content);
                        Device.BeginInvokeOnMainThread(()=> TableView.Content=cookTable(ja));
                     
                    
                        Debug.WriteLine(2);
                    }
                });
            }
            else
            {
                alert("", "", "ok");

            }

        }
        public TableView cookTable(JArray ja)
        {
            var layout = new StackLayout();


            var table = new TableView
            {
                Intent = TableIntent.Settings,
                HeightRequest = ja.Count() * 74
            };
            List<ViewCell> vc = new List<ViewCell>();

            layout = new StackLayout() { Orientation = StackOrientation.Horizontal };
            
            layout.Children.Add(new Label()
            {
                Text = "Name",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(10, 0, 0, 0),
                TextColor = Color.FromHex("#4286f4"),
                VerticalOptions = LayoutOptions.Center
            });
            layout.Children.Add(new Label()
            {
                Text = "Group",
                Margin = new Thickness(0, 0, 10, 0),
                FontSize = 20,
                TextColor = Color.FromHex("#4286f4"),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            });
            layout.Children.Add(new Label()
            {
                Text = "Amount",
                Margin = new Thickness(0, 0, 10, 0),
                FontSize = 20,
                TextColor = Color.FromHex("#4286f4"),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            });
            vc.Add(new ViewCell() { View = layout });

            for (int i = 0; i < ja.Count(); i++)
            {
                layout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    layout.Children.Add(new Label()
                    {
                        Text = ja[i]["service_name"].ToString(),
                        FontSize = 18,
                        Margin = new Thickness(5, 0, 0, 0),
                        TextColor = Color.FromHex("#f35e20"),
                        VerticalOptions = LayoutOptions.Center
                    });

                layout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                layout.Children.Add(new Label()
                {
                    Text = ja[i]["service_group"].ToString(),
                    FontSize = 18,
                    Margin = new Thickness(5, 0, 0, 0),
                    TextColor = Color.FromHex("#f35e20"),
                    VerticalOptions = LayoutOptions.Center
                });

                layout.Children.Add(new Label()
                {
                    Text = ja[i]["tran_amount"].ToString(),
                    Margin = new Thickness(0, 0, 5, 0),
                    FontSize = 19,
                    TextColor = Color.FromHex("#503026"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                });
                vc.Add(new ViewCell() { View = layout });
            }


            table.Root = new TableRoot() { new TableSection("") { vc } };

            return table;
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
    }
}
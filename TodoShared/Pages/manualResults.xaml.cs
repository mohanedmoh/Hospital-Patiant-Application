using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class manualResults : ContentPage
	{
		public manualResults ()
		{
			InitializeComponent ();
            makeForms();
		}
        public void makeForms()
        {

            StackLayout all = new StackLayout() { Spacing = 10, BackgroundColor = Color.FromHex("#ecf0f1") };
            all.Children.Add(makeRow(makeSingle("Lab Result", "lab.png", "Lab"), makeSingle("Add Pharmacy", "drug.png", "Pharmacy")));
            Device.BeginInvokeOnMainThread(()=>CardsScroll.Content = all);
        }
        public StackLayout makeRow(StackLayout first, StackLayout scnd)
        {
            StackLayout row;
            row = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 6, BackgroundColor = Color.Transparent, Margin = new Thickness(2, 2, 2, 2) };
            row.Children.Add(new Frame() { CornerRadius = 5, HasShadow = true, Content = first, Margin = 0 });
            row.Children.Add(new Frame() { CornerRadius = 5, HasShadow = true, Content = scnd, Margin = 0 });
            return row;
        }
        public StackLayout makeSingle(String title, String imageSource, String id)
        {
            StackLayout single;
            Image i = new Image();
            Label l = new Label();
            single = new StackLayout() { Orientation = StackOrientation.Vertical };
            Label hospital = new Label() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "" };
            i.Source = imageSource;
            i.HeightRequest = 90;
            i.HorizontalOptions = LayoutOptions.Center;
            l.Text = title;

            l.HorizontalOptions = LayoutOptions.Center;
            l.VerticalOptions = LayoutOptions.Center;
            single.Children.Add(i);
            single.Children.Add(l);

            single.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(() => openManualResult(id));
                })
            });
            return single;
        }
        public void openManualResult(String id)
        {

            Navigation.PushAsync(new manualResultForm(id));
        }
    }
}
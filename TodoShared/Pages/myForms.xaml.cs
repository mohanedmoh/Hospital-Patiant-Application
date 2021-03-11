using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLocalized.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class myForms : ContentPage
	{
		public myForms ()
		{
			InitializeComponent ();
            makeForms();

        }
        public void makeForms() {

            StackLayout all = new StackLayout() { Spacing = 10, BackgroundColor = Color.FromHex("#ecf0f1") };
            all.Children.Add(makeRow(makeSingle("Blood Pressure", "blood.png","blood"), makeSingle("Diabetes", "diabetes.png","diabetes")));
            CardsScroll.Content = all;
        }
        public void openTimeline(String id) {
            IList<FormData> all = new ObservableCollection<FormData>();
            IList<FormData> list = new ObservableCollection<FormData>();
            list.Add(new FormData() { result1="122",result2="50",date="2017/09/08"});
            IList<FormData> list1 = new ObservableCollection<FormData>();

            list1.Add(new FormData() { result1 = "122", result2 = "50", date = "2017/09/09" });
            list1.Add(new FormData() { result1 = "122", result2 = "0", date = "2017/09/09" });
            IList<FormData> list2 = new ObservableCollection<FormData>();

            list2.Add(new FormData() { result1 = "122", result2 = "50", date = "2017/09/10" });
            IList<FormData> list3 = new ObservableCollection<FormData>();

            list3.Add(new FormData() { result1 = "122", result2 = "50", date = "2017/09/19" });

            all.Add(new FormData() { subR = list, test1 = "Systolic", test2 = "Diastolic",length=4 });
            all.Add(new FormData() { subR = list1, test1 = "Systolic", test2 = "Diastolic", length = 4 });
            all.Add(new FormData() { subR = list2, test1 = "Systolic", test2 = "Diastolic", length = 4 });
            all.Add(new FormData() { subR = list3, test1 = "Systolic", test2 = "Diastolic", length = 4 });
            


            switch (id) {
                case "blood": { Navigation.PushAsync(new FormTimeline(all,2)); }break;
                case "diabetes": { }break;
            }
        }
        public StackLayout makeRow(StackLayout first,StackLayout scnd) {
            StackLayout row;
            row = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 6, BackgroundColor = Color.Transparent, Margin = new Thickness(2, 2, 2, 2) };
            row.Children.Add(new Frame() { CornerRadius = 5, HasShadow = true, Content = first, Margin = 0 });
            row.Children.Add(new Frame() { CornerRadius = 5, HasShadow = true, Content =scnd, Margin = 0 });
            return row;
        }
        public StackLayout makeSingle(String title,String imageSource,String id) {
            StackLayout single;
            Image i = new Image();
            Label l = new Label();
            single = new StackLayout() { Orientation = StackOrientation.Vertical };
            Label hospital = new Label() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "" };//////////////
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
                    Device.BeginInvokeOnMainThread(() => openTimeline(id));
                })
            });
            return single;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLocalized.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class dr_profile : ContentPage
	{
		public dr_profile ()
		{
			InitializeComponent ();
		}
        public dr_profile(String name, String hospital, String clinic, List<day_week> list)
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(() =>setText(name,hospital,clinic,list));
        }
        public void setText(String name, String hospital, String clinic,List<day_week> list) {
            nameL.Text = name;
            hospitalL.Text += hospital;
            clinicL.Text += clinic;
            for (int x= list.Count-1; x>=0;x--)
            {
                Label day = new Label() {HorizontalOptions=LayoutOptions.EndAndExpand,Margin=new Thickness(0,0,15,0), FontSize= Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor=Color.FromHex("#757575") };
                day.Text =  list[x].day_of_week + " :  " + list[x].start_time  + " - " + list[x].end_time +"    "+ (list[x].shift);
                day_week.Children.Add(day);
            }



        }
    }
}
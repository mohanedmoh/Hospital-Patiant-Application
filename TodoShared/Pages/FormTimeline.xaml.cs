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
	public partial class FormTimeline : ContentPage
	{
		public FormTimeline ()
		{
			InitializeComponent ();
		}
        public FormTimeline(IList<FormData> all,int hided)
        {
            InitializeComponent();
            BindingContext = all;
            
            hide(hided + 1);
        }
        private void sub_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            throw new NotImplementedException();
            //await Navigation.PushAsync(new NavigationPage(new Popups.Sub((ExerciseClass)e.SelectedItem)));
        }

        private void Stack_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            throw new NotImplementedException();
        }
        public void hide(int s) {
            switch (s)
            {
                case 1 : { h1.IsVisible = false; }break;
                case 2 : { h2.IsVisible = false; }break;
                case 3 : { h3.IsVisible = false; }break;
            }
        }
	}
}
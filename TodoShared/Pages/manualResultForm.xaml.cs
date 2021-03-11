using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLocalized.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class manualResultForm : ContentPage
	{
        String ImageG;
		public manualResultForm ()
		{
			InitializeComponent ();
		}
        public manualResultForm(String id)
        {
            InitializeComponent();
            fillPicker(id);
            image.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    TakePhoto();
                })
            });
         
            add_manual.Clicked += Add_manual_Clicked;
            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ImageG = Convert.ToBase64String((byte[])args);
                    image.Source = ImageSource.FromStream(() => new MemoryStream((byte[])args));
                });
            });

        }

        private void Add_manual_Clicked(object sender, EventArgs e)
        {
            saveForm(PickerCtl.SelectedItem.ToString(),comments.Text,ImageG,imageDate.Date);
        }

        public void fillPicker(String id)
        {
            List<String> list = new List<string>();
            list.Add("Lab");
            list.Add("Pharmacy");
            Device.BeginInvokeOnMainThread(() => PickerCtl.ItemsSource = list);
            Device.BeginInvokeOnMainThread(() => PickerCtl.SelectedIndex = getIndex(id,list));
        }
        public int getIndex(String s,List<String> list)
        {
            int i = 0;
            for( i = 0; i < list.Count; i++)
            {
                if (s == list[i]) break;
            }
            return i;
        }
        public void saveForm(String type,String comments,String image,DateTime date)
        {
            App.Database.SaveFormAsync(new Model.DbModel.IManualForm() { type=type,comments=comments,image=image,account_id= int.Parse(CrossSecureStorage.Current.GetValue("account_id")) });
        }
        void TakePhoto()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var action = await DisplayActionSheet("Add Photo", "Cancel", null, "Choose Existing", "Take Photo");
                if (action == "Choose Existing")
                {
                    DependencyService.Get<imagePicker>().BringUpGallery();
                }
                else if (action == "Take Photo")
                {
                    DependencyService.Get<imagePicker>().BringUpCameraAsync();
                }
            });
        }
    }
}
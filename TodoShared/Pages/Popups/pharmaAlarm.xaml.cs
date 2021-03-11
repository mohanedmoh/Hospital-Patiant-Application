
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using TodoLocalized.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.Diagnostics;
using TodoLocalized.Model.DbModel;
using System.Collections.Generic;
using Plugin.SecureStorage;
using System.IO;

namespace TodoLocalized.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pharmaAlarm : ContentPage
    {
        private pharmacy ph;
        /*Picker PickerCtl;
        Button Submit_alarm;
        Entry everyEntry, periodEntry, doseEntry;*/
        public pharmaAlarm()
        {
            InitializeComponent();
            //Submit_alarm.Clicked += Submit_alarm_ClickedAsync;
        }
        public pharmaAlarm(pharmacy ph)
        {
            InitializeComponent();
            this.ph = ph;
            PickerCtl.ItemsSource = fillDosePicker();
            //setAll();
            //takeImage.Clicked += TakeImage_Clicked;
            Submit_alarm.Clicked += Submit_alarm_ClickedAsync;

            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
              {
                  Device.BeginInvokeOnMainThread(() =>
                  {
                      image.Source = ImageSource.FromStream(() => new MemoryStream((byte[])args));
                  });
              });



        }
        public void setAll() {
           // stackContent.Children.Add(new GeneralCards.CardView(new GeneralCards.Card() { layout = getAlarmStack() }));
        }

        private async void Submit_alarm_ClickedAsync(object sender, EventArgs e)
        {
            Debug.WriteLine(PickerCtl.SelectedItem.ToString()+"TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT" + CrossSecureStorage.Current.GetValue("account_id"));
            double.TryParse(everyEntry.Text, out double every);
            double.TryParse(periodEntry.Text, out double period);

            IMedicalAlarm alarm = new IMedicalAlarm()
            {
                dose = doseEntry.Text,
                dose_type = PickerCtl.SelectedItem.ToString(),
                medical_name = ph.item_des,
                start_date = DateTime.Now,
                end_date = DateTime.Now.AddMinutes(period * 24 * 60),
                next_alarm_date = DateTime.Now.AddMinutes(every * 60),
                account_id = int.Parse(CrossSecureStorage.Current.GetValue("account_id")),
                M_id = ph.id,
                alarm_status = 1,
                alarm_type = 1,
                every = every * 60,
                image = "",
                period = double.Parse(periodEntry.Text),

            };
            if (!(await haveAlarmAsync(int.Parse(CrossSecureStorage.Current.GetValue("account_id")), ph.id)))
            {
               
                
                await App.Database.SaveAlarmAsync(alarm);
                App.makeNoti(alarm.next_alarm_date,alarm.medical_name,alarm.dose + " x " + alarm.dose_type, alarm.id);
                //  App.Remind
                Debug.WriteLine("fffffffffffDone");
                Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            }
            else {
              //  await App.Database.SaveAlarmAsync(alarm);
                Debug.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& HAVE ALARM");

            }

        }
        /*public StackLayout getAlarmStack() {
            StackLayout all = new StackLayout();
            Label item_des = new Label() { Text = ph.item_des, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Medium,typeof(Label)) };

            StackLayout dose = new StackLayout() { Orientation=StackOrientation.Horizontal};
            Label doseL = new Label() { Text="Dose : ",FontSize=Device.GetNamedSize(NamedSize.Medium,typeof(Label) )};
            doseEntry = new Entry() {Placeholder="1",PlaceholderColor=Color.FromHex("#bababa"),FontSize=16,BackgroundColor=Color.FromHex("#fafafa"),WidthRequest=90 };
            PickerCtl = new Picker() { Title="Select Dose type :",HorizontalOptions=LayoutOptions.FillAndExpand,BackgroundColor=Color.FromHex("#34495e"),TextColor=Color.Black};
            PickerCtl.ItemsSource = fillDosePicker();
            dose.Children.Add(PickerCtl);
            dose.Children.Add(doseEntry);
            dose.Children.Add(doseL);



            StackLayout period = new StackLayout() { Orientation=StackOrientation.Horizontal};
            Label periodL = new Label() { Text="Period :",FontSize=Device.GetNamedSize(NamedSize.Medium,typeof(Label))};
            periodEntry = new Entry() { Placeholder = "1", PlaceholderColor = Color.FromHex("#bababa"), FontSize = 16, BackgroundColor = Color.FromHex("#fafafa"), WidthRequest = 90 };
            period.Children.Add(periodEntry);
            period.Children.Add(periodL);


            StackLayout every = new StackLayout() { Orientation=StackOrientation.Horizontal};
            Label everyL = new Label() { Text = "Every : ", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            everyEntry = new Entry() { Placeholder = "12", PlaceholderColor = Color.FromHex("#bababa"), FontSize = 16, BackgroundColor = Color.FromHex("#fafafa"), WidthRequest = 90 };
            every.Children.Add(everyEntry);
            every.Children.Add(everyL);


            StackLayout image = new StackLayout() { Orientation=StackOrientation.Horizontal};
            Image imageI = new Image() { Source="drug.png",Margin=new Thickness(15,5,5,5)};
            Button imageB = new Button() { Text="Take Image",Margin=new Thickness(5,5,15,5)};
            image.Children.Add(imageB);
            image.Children.Add(imageI);

            Submit_alarm = new Button() {Text="Submit",TextColor=Color.FromHex("#485992") };

            all.Children.Add(Submit_alarm);
            all.Children.Add(image);
            all.Children.Add(every);
            all.Children.Add(period);
            all.Children.Add(dose);
            all.Children.Add(item_des);




            return all;
        }*/
        public async Task<bool> haveAlarmAsync(int account_id, int m_id)
        {
            bool have = true;
            IMedicalAlarm i = new IMedicalAlarm();
            try
            {
                i = await App.Database.GetAlarmByMedicalAndAccount(account_id, m_id);

                if (i == null) { 
                    have = false;
                 
                }
                else
                {
                    have = true;
                    changeText(i);
                  
                }
            }
            catch(Exception e) { }
            return have;

        }
        public void changeText(IMedicalAlarm i) {

            periodEntry.Text = i.period.ToString();
            doseEntry.Text = i.dose.ToString();
            everyEntry.Text = i.every.ToString();


        }
        public List<String> fillDosePicker() {
            List<String> list = new List<string>();
            list.Add("Large Spoon");
            list.Add("Small Spoon");
            list.Add("Pill");
            list.Add("Other");
            return list;
        }

        async
                /*  protected async override Task OnAppearingAnimationEnd()
     {
       //  var translateLength = 400u;




     }*/
                 void TakePhoto(object sender, EventArgs args)
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
              /* public void TakePhoto(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions{
                Directory = "Image",
                Name="test.png"
            });
            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;

            });
        }*/
    }
}
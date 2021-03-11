using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLocalized.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoLocalized.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sub : PopupPage
    {
        public Sub()
        {
            InitializeComponent();
        }
        public Sub(ExerciseClass ex)
        {
            InitializeComponent();
            BindingContext = ex;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;


        }

        /*  protected async override Task OnAppearingAnimationEnd()
          {
            //  var translateLength = 400u;




          }*/

        protected async override Task OnDisappearingAnimationBegin()
        {
            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;



            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private void CloseAllPopup()
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
        }
    }
}
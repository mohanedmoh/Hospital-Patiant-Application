using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using TodoLocalized;
using Foundation;
using UIKit;
using Com.OneSignal;
using NControl.Controls.iOS;

namespace TodoLocalized
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            OneSignal.Current.StartInit("6ebc3a25-58c2-4c9e-ab8f-5368be261dca")
                  .EndInit();
            NControls.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}

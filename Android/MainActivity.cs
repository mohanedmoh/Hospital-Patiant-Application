using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.SecureStorage;
using Com.OneSignal;
using NControl.Controls.Droid;
using Plugin.LocalNotifications;
using Plugin.Permissions;
using Android.Content;
using Android.Provider;
using Android.Database;

namespace TodoLocalized
{
    [Activity(Label = "App8", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
           
            SecureStorageImplementation.StoragePassword = "123456";
            base.OnCreate(bundle);
            OneSignal.Current.StartInit("6ebc3a25-58c2-4c9e-ab8f-5368be261dca")
                  .EndInit();
            NControls.Init();
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 10)
            {
                if (resultCode == Result.Ok)
                {
                    if(data.Data != null)
                    {
                        
                        Android.Net.Uri uri = data.Data;
                        int orentation = getOrientation(uri);

                        BitmapWorkerTask task = new BitmapWorkerTask(this.ContentResolver, uri);
                        task.Execute(orentation);
                    }
                }
            }

        }
        public int getOrientation(Android.Net.Uri photoUri)
        {
            ICursor cursor = Application.ApplicationContext.ContentResolver.Query(photoUri, new String[] { MediaStore.Images.ImageColumns.Orientation }, null, null,null);

            if(cursor.Count != 1)
            {
                return -1;
            }

            cursor.MoveToFirst();
            return cursor.GetInt(0);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}


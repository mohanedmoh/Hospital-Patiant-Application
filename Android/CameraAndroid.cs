using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using TodoLocalized.Model;
using TodoLocalized;
using Android.Provider;

[assembly: Dependency(typeof(CameraAndroid))]
namespace TodoLocalized
{
    public class CameraAndroid : imagePicker
    {
        public void BringUpCameraAsync()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            ((Activity)Forms.Context).StartActivityForResult(intent, 10);
        }

        public void BringUpGallery()
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);

            ((Activity)Forms.Context).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 10);
        }
    }
    
}
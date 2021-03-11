using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using TodoLocalized.Model;
using Xamarin.Forms;
using AVFoundation;
using CoreGraphics;

[assembly: Dependency(typeof(imagePicker))]
namespace TodoLocalized
{
    public class CameraIOS : imagePicker
    {
        public CameraIOS(){}
        public async void BringUpCameraAsync()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                var access = await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);

                if (access)
                {
                    GotAccessToCamera();
                }
            }
            else
            {
                GotAccessToCamera();
            }
        }

        public void BringUpGallery()
        {
            var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.PhotoLibrary, MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary) };
            imagePicker.AllowsEditing = true;

            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController!=null) {
                vc = vc.PresentedViewController;
            }

            vc.PresentViewController(imagePicker, true, null);
            imagePicker.FinishedPickingMedia += (sender, e) =>
            {
                UIImage originalImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
                if (originalImage != null)
                {
                    var pngImage = originalImage.AsPNG();
                    byte[] myByteArray = new byte[pngImage.Length];
                    System.Runtime.InteropServices.Marshal.Copy(pngImage.Bytes, myByteArray, 0, Convert.ToInt32(pngImage.Length));

                    MessagingCenter.Send<byte[]>(myByteArray, "ImageSelected");

                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    vc.DismissViewController(true, null);
                });
            };
            imagePicker.Canceled += (sender, e) => vc.DismissViewController(true, null);
        }

        private void GotAccessToCamera()
        {
            var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.PhotoLibrary, MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary) };


            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while(vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            vc.PresentViewController(imagePicker, true, null);

            imagePicker.FinishedPickingMedia += (sender, e) =>
            {
                UIImage image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));

                UIImage rotateImage = RotateImage(image, image.Orientation);
                rotateImage = rotateImage.Scale(new CoreGraphics.CGSize(rotateImage.Size.Width, rotateImage.Size.Height), 0.5f);
                var jpegImage = rotateImage.AsJPEG();
                byte[] myByteArray = new byte[jpegImage.Length];
                System.Runtime.InteropServices.Marshal.Copy(jpegImage.Bytes, myByteArray, 0, Convert.ToInt32(jpegImage.Length));

                MessagingCenter.Send<byte[]>(myByteArray, "ImageSelected");

                Device.BeginInvokeOnMainThread(() =>
                {
                    vc.DismissViewController(true, null);
                });


            };
            imagePicker.Canceled += (sender, e) => vc.DismissViewController(true, null);
        }

        double radians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private UIImage RotateImage(UIImage src,UIImageOrientation orientation)
        {
            UIGraphics.BeginImageContext(src.Size);
            if (orientation==UIImageOrientation.Right)
            {
                CGAffineTransform.MakeRotation((nfloat)radians(90));
            }
            else if (orientation == UIImageOrientation.Left)
            {
                CGAffineTransform.MakeRotation((nfloat)radians(-90));
            }
            else if (orientation == UIImageOrientation.Down)
            {
            }
            else if (orientation == UIImageOrientation.Up)
            {
                CGAffineTransform.MakeRotation((nfloat)radians(90));
            }

            src.Draw(new CGPoint(0, 0));
            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;

        }
    }
}
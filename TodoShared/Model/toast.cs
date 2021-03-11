
using System;
using Xamarin.Forms;

namespace TodoLocalized.Model
{
    public static class XFToast
    {
        public static void ShortMessage(string message)
        {
            //DependencyService.Get<IMessage>().ShortAlert(message);
        }

        public static void LongMessage(string message)
        {
           // DependencyService.Get<IMessage>().LongAlert(message);
        }

        internal static void shortMessage(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
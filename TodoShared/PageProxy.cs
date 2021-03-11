using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TodoLocalized
{
    public class PageProxy : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return (this).OnBackButtonPressed();
        }
    }
}

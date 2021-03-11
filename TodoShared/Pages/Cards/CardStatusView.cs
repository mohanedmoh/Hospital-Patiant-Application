using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TodoLocalized.Pages.Cards
{
    public class CardStatusView : ContentView
    {
        List<Color> color = new List<Color>() {
                Color.FromRgb(140,158,255),
                Color.FromRgb(98,0,234),
                Color.FromRgb(46,125,50),
                Color.FromRgb(255,167,38),
                Color.FromRgb(38,50,56),
                Color.FromRgb(62,39,35),
                Color.FromRgb(174,234,0),
                Color.FromRgb(63,81,181),
            };
        public CardStatusView(Card card)
        {
            var statusBoxView = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            Random rnd = new Random();

            statusBoxView.BackgroundColor = Color.Transparent;
            
            

            Content = statusBoxView;
        }
    }
}
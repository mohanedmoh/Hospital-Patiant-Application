using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace TodoLocalized.Pages.Cards
{
    public class CardDetailsView : ContentView
    {
        public CardDetailsView(Card card)
        {
            BackgroundColor = Color.White;

            Label clinic = new Label()
            {
                FormattedText = card.clinic,
                FontSize = 16,
                Margin = new Thickness(0,0,2,0),
                TextColor = StyleKit.LightTextColor,
                FontAttributes = FontAttributes.Bold      
                
            };
            clinic.HorizontalOptions = LayoutOptions.StartAndExpand;
            clinic.VerticalOptions = LayoutOptions.Center;

            Label name = new Label()
            {
                FormattedText = card.name,
                FontSize = 15,
                TextColor =Color.Black // StyleKit.LightTextColor,
            };
            name.VerticalOptions= LayoutOptions.Center;
            name.HorizontalOptions= LayoutOptions.StartAndExpand;
            Label hospital = new Label()
            {
                FormattedText = card.hospital,
                FontSize = 15,
                TextColor = Color.Black // StyleKit.LightTextColor,
            };
            hospital.VerticalOptions = LayoutOptions.Center;
            hospital.HorizontalOptions = LayoutOptions.StartAndExpand;

            Image icon = new Image() {
                Source = card.Source,
                HeightRequest = 228,
                WidthRequest = 208,
                Margin = new Thickness(0,0,20,0),
                HorizontalOptions=LayoutOptions.Center
               
            };

            var stack = new StackLayout()
            {
                Spacing = 6,
                Padding = new Thickness(1, 5, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    name,
                    clinic,
                    hospital

                },
                HeightRequest = 150,
                WidthRequest=300,
                Orientation = StackOrientation.Vertical,
                HorizontalOptions=LayoutOptions.Center
            };

            var grid = new Grid() {
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = new GridLength (60, GridUnitType.Absolute) },
                    new ColumnDefinition{ Width = new GridLength (70, GridUnitType.Absolute) }
                }
            };
            
            grid.HeightRequest = 100;
            grid.WidthRequest = 400;
            grid.Children.Add(stack, 1, 4, 0, 1);
            grid.Children.Add(icon,  0, 1, 0, 1);
           
         
            Content = new Frame { Content= grid,HasShadow=true, CornerRadius=9,Padding=0 };
        }

        
    }
}
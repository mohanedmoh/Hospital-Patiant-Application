using TodoLocalized;
using System.Collections.Generic;
using System.Diagnostics;
using TodoLocalized.Pages;


using Xamarin.Forms;

namespace TodoLocalized.Master_Pages
{
    public class Master : ContentPage
    {
        public ListView ListView { get { return listView; } }

        ListView listView;

        public Master()
        {
            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Accounts",
                    IconSource = "accounts.png",
                     TargetType = typeof(Accounts)
                },
                new MasterPageItem
                {
                    Title = "Lab Results",
                    IconSource = "lab.png",
                    TargetType = typeof(lab_results)
                },
                new MasterPageItem
                {
                    Title = "Medication",
                    IconSource = "drug.png",
                    TargetType = typeof(pharmacy_list)
                },
                new MasterPageItem
                {
                    Title = "My Forms",
                    IconSource = "forms.png",
                    TargetType = typeof(myForms)
                },
                new MasterPageItem
                {
                    Title = "Invoice",
                    IconSource = "invoice.png",
                    TargetType = typeof(invoice_tab)
                },
                new MasterPageItem
                {
                    Title = "Add Manual",
                    IconSource = "user.png",
                    TargetType = typeof(manualResults)
                },
                new MasterPageItem
                {
                    Title = "PHR",
                    IconSource = "about.png",
                    TargetType = typeof(PHR)
                },
                new MasterPageItem
                {
                    Title = "Doctors",
                    IconSource = "doctor.png",
                    TargetType = typeof(search_dr)
                },
                new MasterPageItem
                {
                    Title = "About",
                    IconSource = "about.png",
                    TargetType = typeof(About)
                },
                new MasterPageItem
                {
                    Title = "Sign out",
                    IconSource = "out.png",
                    TargetType = typeof(Login)
                }
            };

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { Margin = new Thickness(3, 0, 0, 0), VerticalOptions = LayoutOptions.FillAndExpand, TextColor = Color.White, FontSize = 14 };
                    label.SetBinding(Label.TextProperty, "Title");
                    grid.RowSpacing = 3;
                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);
                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.Default
            };

            Icon = "menu.png";

            Title = "Personal Organiser";

            Image imageU = new Image();
            imageU.Source = "user.png";
            imageU.HorizontalOptions = LayoutOptions.Center;
            imageU.VerticalOptions = LayoutOptions.Center;
            imageU.Margin = new Thickness(0, 30, 0, 0);
            imageU.HeightRequest = 128;
            StackLayout side = new StackLayout();
            side.Children.Add(imageU);
            side.Children.Add(listView);
            
            Content = new ScrollView() { Content=side};
            Content.BackgroundColor = Color.FromHex("#34495e");


        }
    }
}

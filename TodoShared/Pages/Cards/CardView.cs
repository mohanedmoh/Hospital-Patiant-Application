using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TodoLocalized.Model;
using Xamarin.Forms;

namespace TodoLocalized.Pages.Cards
{

    public class CardView : ContentView
    {
        public CardView(Card card)
        {
     
            

            CardDetailsView cardD = new CardDetailsView(card);

            cardD.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    openProfile(card.name,card.hospital,card.clinic,card.listD);
                })
            });




            Content = cardD;

        }
        public void openProfile(String name,String hospital,String clinic,List<day_week> list) {
            Navigation.PushAsync(new Pages.dr_profile(name, hospital, clinic,list));
        }
       

    }
}
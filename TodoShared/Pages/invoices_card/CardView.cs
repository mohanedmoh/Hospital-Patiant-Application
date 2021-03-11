using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TodoLocalized.Model;
using Xamarin.Forms;

namespace TodoLocalized.Pages.invoices_card
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
                    openInvoice(card);
                })
            });




            Content = cardD;

        }
        public void openInvoice(Card card) {
            Navigation.PushAsync(new Pages.invoice_profile(card));
        }
       

    }
}
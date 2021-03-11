using System;
using System.Collections.Generic;
using TodoLocalized.Model;
using Xamarin.Forms;

namespace TodoLocalized.Pages.Cards
{
    public class Card
    {
        public String Source { get; set; }
        public CardStatus Status { get; set; }

        public String name { get; set; }
        public String clinic { get; set; }
        public String hospital { get; set; }
        public List<day_week> listD { get; set; }



    }

    public enum CardStatus
    {
        Alert,
        Completed,
        Unresolved
    }
}

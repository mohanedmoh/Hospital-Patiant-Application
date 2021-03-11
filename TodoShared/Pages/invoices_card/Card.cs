using System;

namespace TodoLocalized.Pages.invoices_card
{
    public class Card
    {
        public String Source { get; set; }
        //public CardStatus Status { get; set; }
        public String id { get; set; }
        public String invoice_name { get; set; }
        public String total_amount { get; set; }
        public String paid_amount { get; set; }
        public String req_amount { get; set; }
        public String date { get; set; }
        public String status { get; set; }



    }

    public enum CardStatus
    {
        Alert,
        Completed,
        Unresolved
    }
}

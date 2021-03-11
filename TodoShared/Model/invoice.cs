using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model
{
    public class invoice
    {
        public String id { get; set; }
        public String invoice_name { get; set; }
        public String total_amount { get; set; }
        public String paid_amount { get; set; }
        public String req_amount { get; set; }
        public String entered_date { get; set; }
        public String status { get; set; }
    }
}

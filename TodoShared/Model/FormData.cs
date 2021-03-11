using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model
{
    public class FormData
    {
        public int id { get; set; }
        public String test1 { get; set; }
        public String test2 { get; set; } = "";
        public String test3 { get; set; } = "";
        public String result1 { get; set; }
        public String result2 { get; set; } = "";
        public String result3 { get; set; } = "";
        public String date { get; set; }
        public IList<FormData> subR{get; set;}
        public int length { get; set; }
        public bool IsLast { get; set; } = false;
        public String color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model
{
    public class doctor
    {
        public String name { get; set; }
        public String hospital_name { get; set; }
        public String image { get; set; }
        public String clinic_name { get; set; }
        public List<day_week> listDay { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoLocalized.Model
{
    public class ExerciseClass
    {
        public string date { get; set; }
        public string test_name { get; set; }
        public string result { get; set; }
        public string RV { get; set; }
        public string unit { get; set; }
        public string analysis_name { get; set; }
        public string reviewed { get; set; }
        public int id { get; set; }
        public IList<ExerciseClass> ex { get; set; }
        public int length { get; set; }
        public bool IsLast { get; set; } = false;
        public String color { get; set; }
    }
}

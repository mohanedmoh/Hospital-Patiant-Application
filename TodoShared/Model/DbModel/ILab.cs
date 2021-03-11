using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model.DbModel
{
    [SQLite.Table("lab")]
    public class ILab
    {
        [SQLite.AutoIncrement,SQLite.PrimaryKey]
        public int id { get; set; }
        public int result_id { get; set; }
        public int account_id { get; set; }
        public string date { get; set; }
        public string test_name { get; set; }
        public string result { get; set; }
        public string RV { get; set; }
        public string unit { get; set; }
        public string analysis_name { get; set; }
        public string reviewed { get; set; }
        public int length { get; set; }
        public bool IsLast { get; set; } = false;
        public String color { get; set; }
    }
}

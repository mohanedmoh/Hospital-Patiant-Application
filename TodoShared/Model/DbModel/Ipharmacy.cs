using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model.DbModel
{
    [Table("pharmacy")]
    public class Ipharmacy
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }
        public int M_id { get; set; }
        public string item_des { get; set; }
        public string notes { get; set; }
        public string dose { get; set; }
        public string date { get; set; }
        public int length { get; set; }
        public String color { get; set; }
        public int account_id { get; set; }
        public bool IsLast { get; set; } = false;
        public int Done { get; set; } = 1;
    }
}

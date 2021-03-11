using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model.DbModel
{
    [Table("manual_form")]
    public class IManualForm
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int account_id { get; set; }
        public String image { get; set; }
        public String comments { get; set; }
        public String type { get; set; }
        public DateTime date { get; set; }

    }
}

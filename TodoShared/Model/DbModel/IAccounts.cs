using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Database
{
    [SQLite.Table("Accounts")]
    public class IAccounts
    {
        [SQLite.PrimaryKey,SQLite.AutoIncrement]
        public int id { get; set; }
        public String  hospital_name{ get; set; }
        public String hospital_url { get; set; }
        public int user_hospital_id { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String password { get; set; }
        public String type_description { get; set; }
    }
}

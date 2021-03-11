using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model.DbModel
{
    [SQLite.Table("user")]
    public class user
    {
        [SQLite.AutoIncrement,SQLite.PrimaryKey]
        public int id { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String gender { get; set; }
        public String username { get; set; }
        public String password { get; set; }

        public String phone { get; set; }
        public String email { get; set; }
        public String birth_date{ get; set; }

    }
}

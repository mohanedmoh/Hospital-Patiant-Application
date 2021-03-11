using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model.DbModel
{
    [SQLite.Table("FirstRun")]
    public class IFirstRun
    {
        [SQLite.AutoIncrement,SQLite.PrimaryKey]
        public int? id { get; set; }
        public String key { get; set; }
        public String value { get; set; } = null;
    }
}

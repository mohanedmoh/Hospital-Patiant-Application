using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoLocalized.Model.DbModel
{
    [Table("MedicalAlarm")]
    public class IMedicalAlarm
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int M_id { get; set; }
        public String medical_name { get; set; }
        public int account_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime next_alarm_date { get; set; }
        public double every { get; set; }
        public String image { get; set; }
        public int alarm_type { get; set; }
        public int alarm_status { get; set; }
        public int hospital_id { get; set; }
        public String dose { get; set; }
        public String dose_type { get; set; }
        public double period { get; set; }

   
    }
}

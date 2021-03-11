using Android.App;
using Android.Content;
using Android.OS;
using Com.OneSignal;
using Plugin.LocalNotifications;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoLocalized.Database;
using TodoLocalized.Model.DbModel;
using Xamarin.Forms;

namespace TodoLocalized
{
    public partial class App : Xamarin.Forms.Application
    {
        static DatabaseFunctions database;
        public App()
        {
            InitializeComponent();
            if (CrossSecureStorage.Current.HasKey("id"))
             {
                 if (CrossSecureStorage.Current.HasKey("hid") && CrossSecureStorage.Current.HasKey("Hpass"))
                 {
                     MainPage = new NavigationPage(new Master_Pages.main());
                 }
                 else
                 {
                     MainPage = new NavigationPage(new Pages.Accounts());
                 }
             
             }
             else
             {
                 MainPage = new NavigationPage(new Pages.Login());
             }
            //makeNoti(Dat,"","");
            //MainPage = new NavigationPage(new Pages.Accounts());

            OneSignal.Current.StartInit("6ebc3a25-58c2-4c9e-ab8f-5368be261dca").EndInit();
        }
        public static async void getAlarmsAsync()
        {
            List<IAccounts> list = await database.GetAccountsAsync();
            for (int x = 0 ; x < list.Count ; x++)
            {
                List<IMedicalAlarm> mList = await database.GetAlarmByAccount(list[x].user_hospital_id);
                if (mList != null)
                {
                    for (int y = 0; y < mList.Count; y++)
                    {
                        makeNoti(mList[y].next_alarm_date, mList[y].medical_name,mList[y].dose+" x "+mList[y].dose_type,mList[y].id);
                        await changeIfEndAsync(mList[y]);
                    }
                }
            }
        }
        public static async Task<bool> changeIfEndAsync(IMedicalAlarm alarm)
        {
            if ((alarm.end_date - alarm.next_alarm_date).TotalMinutes <= 0)
            {
                alarm.alarm_status = 0;
                await App.database.SaveAlarmAsync(alarm);
                return true;
            }
            else {
                return false;
            }
        }
        public static void makeNoti(DateTime dateTime, string title, string message,int alarmId)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android: { Remind(dateTime, title, message,alarmId); } break;
                case Device.iOS: { }break;
            }
        }
        public static async void setNextAlarmAsync(int alarmid)
        {
            IMedicalAlarm alarm = await database.GetAlarmAsync(alarmid);
            alarm.next_alarm_date = alarm.next_alarm_date.AddMinutes(alarm.every);
            bool end = await changeIfEndAsync(alarm);
            if (!end)
            {
                System.Diagnostics.Debug.WriteLine(alarm.next_alarm_date+"#############################"+alarm.end_date);
                await database.SaveAlarmAsync(alarm);
                makeNoti(alarm.next_alarm_date, alarm.medical_name, alarm.dose + " x " + alarm.dose_type, alarm.id);
            }
            
        }
        public static void Remind(DateTime dateTime, string title, string message,int alarmId)
        {

            Intent alarmIntent = new Intent(packageContext: Forms.Context, type: typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            alarmIntent.PutExtra("alarmId", alarmId);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

            long time =(long)(dateTime - DateTime.Now).TotalSeconds;
      
            if (time > 1)
            {
                alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + time * 1000, pendingIntent);
            }
            else
            {
                alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 1 * 1000, pendingIntent);
            }
            
        }
        public static DatabaseFunctions Database
             {
                 get
                 {
                     if (database == null)
                     {
                    database = new DatabaseFunctions(DependencyService.Get<IFileHelper>().GetLocalFilePath("smartCare.db3"));
                         System.Diagnostics.Debug.WriteLine("%%%%%%%%%%%%%%%%%%%%%%5"+database);
                    
                }

                return database;
                 }
             }
        
        public void Toast(String s, String x) { }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static async Task<bool> checkFirstAsync()
        {
            
           // int.TryParse(idNewS,out idNew);

            if ((await App.Database.GetAllFirst()).Count == 0)
            {

                await App.Database.SaveFirst(new Model.DbModel.IFirstRun() { id = 0, key = "first_time", value = "1" });
                await App.Database.SaveFirst(new Model.DbModel.IFirstRun() {id=0, key = "sync_accounts", value = "0" });
              
                System.Diagnostics.Debug.WriteLine("&^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^"+"DONE");
                return true;
            }
            else
            {
                return false;
            }

        }
        public static async Task<bool> checkStatus(String key)
        {
            //System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@@@@"+( await App.Database.GetFirst(key)).value);
            if(((await App.Database.GetFirst(key)).value.Equals("0")) && !await checkFirstAsync())
            {
                return true;
            }
            
            else
            {
                return false;
            }
        }
        public static async void changeStatus(String key) {
            IFirstRun firstRun = (await App.Database.GetFirst(key));
            firstRun.value = "1";

            System.Diagnostics.Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"+firstRun.id);
            await App.Database.SaveFirst(firstRun);
        }
    }
}

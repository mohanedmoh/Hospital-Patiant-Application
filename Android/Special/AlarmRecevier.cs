using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using System;

namespace TodoLocalized
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Full , "BackgroundReceiver");
            KeyguardManager keyguardManager = (KeyguardManager)context.GetSystemService(Context.KeyguardService);
          //  KeyguardManager.KeyguardLock keyguardLock = keyguardManager.NewKeyguardLock("TAG");
          //  keyguardLock.DisableKeyguard();
            wakeLock.Acquire();

            // Run your code here

           

            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");
            int id = intent.GetIntExtra("alarmId",0);

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);
            var manager = NotificationManagerCompat.From(context);

            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

           
            //Generate a notification with just short text and small icon
            var builder = new NotificationCompat.Builder(context)
                            .SetContentIntent(contentIntent)
                            .SetSmallIcon(Resource.Drawable.logo)
                            .SetContentTitle(title)
                            .SetContentText(message)
                            .SetStyle(style)
                           
                            .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm))
                            .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 })
                            .SetLights(Color.Red, 3000, 3000)
                            .SetOnlyAlertOnce(false)
                            .SetPriority(0)
                            .SetAutoCancel(false)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis());
                           // .Extend(wearableExtender);

            var notification = builder.Build();
            wakeLock.Release();

            manager.Notify(0, notification);
            App.setNextAlarmAsync(id);
        }
    }
}
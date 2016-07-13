using System;

using Android.App;
using Android.Content;

namespace TzFixer
{
	public class TimezoneChangeReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			DoWork();
		}

		public static void DoWork()
		{
			var prefs = Application.Context.GetSharedPreferences("TzFixerSettings", FileCreationMode.MultiProcess | FileCreationMode.Private);
			string timezone = prefs.GetString("Timezone", "none set");
			string old = Java.Util.TimeZone.Default.ID;

			if (timezone == "none set") return;

			if (timezone == old) return;

			// alarm stands for AlarmService
			AlarmManager alarmManager = (AlarmManager)Application.Context.GetSystemService("alarm");
			alarmManager.SetTimeZone(timezone);

			Console.WriteLine("Timezone changed from " + old + " to " + timezone);
		}
	}
}


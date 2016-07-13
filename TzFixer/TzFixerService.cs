using Android.OS;
using Android.App;
using Android.Content;
using Android.Widget;

namespace TzFixer
{
	[Service]
	[IntentFilter(new[] { Intent.ActionTimezoneChanged }, Priority = (int)IntentFilterPriority.HighPriority)]
	public class TzFixerService : Service
	{
		public TzFixerService()
		{
		}

		public override void OnCreate()
		{
			Toast.MakeText(Application.Context, "TzFixer service started", ToastLength.Short).Show();
			RegisterReceiver(new TimezoneChangeReceiver(), new IntentFilter(Intent.ActionTimezoneChanged));
			TimezoneChangeReceiver.DoWork();
			base.OnCreate();
		}

		public override IBinder OnBind(Intent intent)
		{
			return new Binder();
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			return StartCommandResult.Sticky;
		}
	}
}


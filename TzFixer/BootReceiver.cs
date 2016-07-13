using Android.App;
using Android.Content;

namespace TzFixer
{
	[BroadcastReceiver]
	[IntentFilter(new[] { Intent.ActionBootCompleted })]
	public class BootReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			context.StartService(new Intent(context, typeof(TzFixerService)));
		}
	}
}


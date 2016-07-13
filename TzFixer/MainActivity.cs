using Android.App;
using Android.Widget;
using Android.OS;
using Android.Preferences;
using Android.Content;

namespace TzFixer
{
	[Activity(Label = "TzFixer", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/Theme.AppCompat")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			var prefs = Application.Context.GetSharedPreferences("TzFixerSettings", FileCreationMode.MultiProcess | FileCreationMode.Private);
			var prefsEditor = prefs.Edit();

			SetContentView(Resource.Layout.Main);

			StartService(new Intent(this, typeof(TzFixerService)));

			Button button = FindViewById<Button>(Resource.Id.getTimezone);
			Button fixit = FindViewById<Button>(Resource.Id.fixTimezone);
			Button disablebutton = FindViewById<Button>(Resource.Id.disableButton);
			Button closebutton = FindViewById<Button>(Resource.Id.closeButton);
			TextView tztext = FindViewById<TextView>(Resource.Id.tzText);

			TextView fix = FindViewById<TextView>(Resource.Id.currentFixed);

			fix.Text = "Current fixed timezone: " + prefs.GetString("Timezone", "none set");

			button.Click += delegate
			{
				tztext.Text = Java.Util.TimeZone.Default.ID;
				fixit.Enabled = true;
			};

			fixit.Click += delegate
			{
				prefsEditor.PutString("Timezone", tztext.Text);
				prefsEditor.Apply();
				fixit.Enabled = false;
				fix.Text = "Current fixed timezone: " + prefs.GetString("Timezone", "none set");
			};

			closebutton.Click += delegate
			{
				this.Finish();
			};

			disablebutton.Click += delegate
			{
				prefsEditor.PutString("Timezone", "none set");
				prefsEditor.Apply();
				fix.Text = "Current fixed timezone: " + prefs.GetString("Timezone", "none set");
			};
		}
	}
}



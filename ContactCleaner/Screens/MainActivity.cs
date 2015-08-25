using Android.App;
using Android.OS;


namespace ContactCleaner
{
	[Activity (Label = "ContactCleaner", MainLauncher = true, Icon = "@drawable/icon")]		
	public class MainActivity : Activity
	{
		override protected void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			App.Instance.Init(this);
		}
	}
}


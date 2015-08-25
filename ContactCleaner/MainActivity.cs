
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Android.Util;
using Android.Content;
using Android.Database;
using Android.Provider;
using Android.Net;


namespace ContactCleaner
{
	[Activity (Label = "ContactCleaner", MainLauncher = true, Icon = "@drawable/icon")]		
	public class MainActivity : Activity
	{
		override protected void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			App.Current.Init(this);
		}
	}
}


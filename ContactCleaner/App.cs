using System;
using Android.App;
using Android.Views;
using Android.Widget;

namespace ContactCleaner
{
	public class App
	{
		private static readonly App _app = new App ();
		public ProgressShower ProgressShower { get; set;}
		public ViewGroup RelativeLayout { get; set;}
		public Popup Popup { get; set;}

		private App()
		{			
		}

		public void Init (Activity activity)
		{
			Popup= new Popup(activity);
			ProgressShower=new ProgressShower(activity);

			RelativeLayout = activity.FindViewById<ViewGroup>(Resource.Id.ltrel);

			Button _buttonStart = activity.FindViewById<Button> (Resource.Id.btn);

			_buttonStart.Click += (s,e) => (new ContactsHandler()).Start ();
		}

		public static App Current
		{
			get
			{
				return _app;
			}
		}
	}
}


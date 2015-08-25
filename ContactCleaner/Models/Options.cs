using System;
using Android.Widget;

namespace ContactCleaner.Models
{
	public class Options
	{
		public Options()
		{
			ByName = App.Current.RelativeLayout.FindViewById<CheckBox> (Resource.Id.byname).Checked;
			ByPhone = App.Current.RelativeLayout.FindViewById<CheckBox> (Resource.Id.byphone).Checked;
			Advanced = App.Current.RelativeLayout.FindViewById<CheckBox> (Resource.Id.advanced).Checked;
		}

		public bool ByName { get; set; }
		public bool ByPhone { get; set; }
		public bool Advanced { get; set; }
	}
}


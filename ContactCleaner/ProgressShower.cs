using System;
using Android.App;
using Android.Content;
using Android.Widget;

namespace ContactCleaner
{
	public class ProgressShower : ProgressDialog
	{
		public ProgressShower (Activity activity):base(activity)
		{			
			Indeterminate = true;
			SetProgressStyle(ProgressDialogStyle.Horizontal);
			SetButton("Cancel",(EventHandler<DialogClickEventArgs>)null);
		}

		public override void Show ()
		{
			base.Show ();
			Button _buttonCancel = GetButton ((int)DialogButtonType.Neutral);
			_buttonCancel.Click += (sender, args) =>
			{
				Dismiss();
			};
		}
	}
}


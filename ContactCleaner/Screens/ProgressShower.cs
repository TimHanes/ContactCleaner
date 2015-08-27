using System;
using Android.App;
using Android.Content;
using Android.Widget;

namespace ContactCleaner
{
	public class ProgressShower : ProgressDialog
	{
		EventHandler<DialogClickEventArgs> _click;

		public ProgressShower (Activity activity):base(activity)
		{	
			Indeterminate = true;
			SetProgressStyle(ProgressDialogStyle.Horizontal);
			SetButton("Cancel",_click);
		}

		public override void Show ()
		{
				base.Show ();
//				Button _buttonCancel = GetButton ((int)DialogButtonType.Neutral);
				_click += delegate {
				

					base.Dismiss ();
				};

		}
	}
}


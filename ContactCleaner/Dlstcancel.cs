
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ContactCleaner
{
			
	public class Dlstcancel : IDialogInterfaceOnClickListener
	{
		MainActivity main;
		override public void onClick(DialogInterface dialoginterface,int i)
		{
			main = main.GetMainActivity();
			main.cwr.onFinished();
			main.pd.dismiss();
		}
	}
}


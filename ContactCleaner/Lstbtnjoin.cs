
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
			
	public class Lstbtnjoin :  View.IOnClickListener
	{
		override public void onClick(View v)
		{
			MainActivity m;
			m = m.GetMainActivity ();
			try{
				m.p.MsgBoxClose();
				m.cwr.contactJoin(m.cwr.findUri[m.cwr.i]);
				m.cwr.onResume();
			}
			catch(Exception err)
			{
				Log.e("xxx",err.getMessage());
			}
		}
	}
}


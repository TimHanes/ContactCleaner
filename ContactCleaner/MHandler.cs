
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
using ContactCleaner;
namespace ContactCleaner
{
		
	public class MHandler : Handler
	{
		override public void	handleMessage(Message msg)
		{

			switch(msg.what)
			{
			case 0:
				tv.append(msg.obj.toString());
				break;
			case 1:
				pd.cancel();
				p.MsgBoxButtons("What do with:",msg.obj.toString()+" ?","Delete","Join","Ignore",lstbtndel,lstbtnjoin,lstbtnign,true);
				break;
			case 2:
				tv.setText(msg.obj.toString());
				break;
			case 3:
				pd.setMax(msg.arg1);
				pd.setProgress(msg.arg2);
				pd.setMessage(msg.obj.toString());
				pd.show();
				//p.MsgBoxProgress("Processing...",msg.obj.toString(),true);

				break;
			case 4:
				p.MsgBoxClose();
				pd.dismiss();
				break;
			case 5:
				p.pb.setMax(msg.arg1);
				p.pb.setProgress(msg.arg2);
				break;
			}
		}
	}
}


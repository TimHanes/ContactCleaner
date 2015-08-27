using System;
using Android.Widget;
using Android.App;
using Android.Views;
using Android.Util;
using Android.Content;

namespace ContactCleaner
{
	public class Popup 
	{
		public Dialog Dialog { get; set; }
		public TextView Text1 { get; set; }
		public TextView Text2 { get; set; }
		public Button Button1 { get; set; }
		public Button Button2 { get; set; }
		public Button Button3 { get; set; }
		public Button Button4 { get; set; }
		public EditText Edit1 { get; set; }
		public EditText Edit2 { get; set; }
		public ProgressBar ProgressBar { get; set; }
		public CheckBox CheckBoxSave { get; set; }
		public TextView CheckBoxSaveText { get; set; }

		public	Popup (Activity activity)
		{
			Dialog = new Dialog(activity);
 			Dialog.SetContentView(Resource.Layout.Popup);//popup view is the layout you created
			Text1 = (TextView)Dialog.FindViewById(Resource.Id.mbtext1);
			Text2 = (TextView)Dialog.FindViewById(Resource.Id.mbtext2);
			Button4=(Button)Dialog.FindViewById(Resource.Id.mbbtn4);
			Button3=(Button)Dialog.FindViewById(Resource.Id.mbbtn3);
			Button1=(Button)Dialog.FindViewById(Resource.Id.mbbtn1);
			Button2=(Button)Dialog.FindViewById(Resource.Id.mbbtn2);
			CheckBoxSave = (CheckBox)Dialog.FindViewById(Resource.Id.checksave);
			CheckBoxSaveText=(TextView)Dialog.FindViewById(Resource.Id.chvsave);
			Edit1=(EditText)Dialog.FindViewById(Resource.Id.mbedit1);
			Edit2=(EditText)Dialog.FindViewById(Resource.Id.mbedit2);
			ProgressBar=(ProgressBar)Dialog.FindViewById(Resource.Id.mbpbar);
		}

		public void invisible()
		{
			Text1.Visibility = ViewStates.Gone;
			Text2.Visibility = ViewStates.Gone;
			Button4.Visibility = ViewStates.Gone;
			Button3.Visibility = ViewStates.Gone;
			Button1.Visibility = ViewStates.Gone;//=(Button)dialog.findViewById(R.id.mbbtn1);
			Button2.Visibility = ViewStates.Gone;//=(Button)dialog.findViewById(R.id.mbbtn2);
			CheckBoxSave.Visibility = ViewStates.Gone;// = (CheckBox)dialog.findViewById(R.id.checksave);
			CheckBoxSaveText.Visibility = ViewStates.Gone;//=(TextView)dialog.findViewById(R.id.chvsave);
			Edit1.Visibility = ViewStates.Gone;//=(EditText)dialog.findViewById(R.id.mbedit1);
			Edit2.Visibility = ViewStates.Gone;//=(EditText)dialog.findViewById(R.id.mbedit2);
			ProgressBar.Visibility = ViewStates.Gone;//=(ProgressBar)dialog.findViewById(R.id.mbpbar);
		}

		public void MsgBox(String msg)
		{
			invisible();
			Text1.Visibility = ViewStates.Visible;
			Text1.SetText(msg, TextView.BufferType.Normal);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBox(String title,String msg)
		{
			invisible();
			Dialog.SetTitle(title);
			Text1.Visibility = ViewStates.Visible;
			Text1.SetText(msg, TextView.BufferType.Normal);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxButtons(String title,String msg,object btn2lst)
		{
			invisible();
			Dialog.SetTitle(title);
			Button1.SetText("CANCEL", TextView.BufferType.Normal);
			Button2.SetText("OK", TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;

			Button1.Click += delegate {  };
			Button2.Click += delegate {  };

//			btn1.SetOnClickListener (View.IOnClickListener );//{ public void onClick(View v){dialog.cancel();}});
//			btn2.SetOnClickListener(btn2lst);
			Text1.SetText(msg, TextView.BufferType.Normal);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}

		}

		public void MsgBoxButtons(String title,String msg,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst)
		{
			invisible();
			Dialog.SetTitle(title);
			Button1.SetText("CANCEL", TextView.BufferType.Normal);
			Button2.SetText("OK", TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Text1.SetText(msg, TextView.BufferType.Normal);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}

		}
		public void MsgBoxButtons(String title,String msg,String button1name,String button2name,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst)
		{
			invisible();
			Dialog.SetTitle(title);
			Button1.SetText(button1name, TextView.BufferType.Normal);
			Button2.SetText(button2name, TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Button1.SetOnClickListener(btn1lst);
			Button2.SetOnClickListener(btn2lst);
			Text1.SetText(msg, TextView.BufferType.Normal);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxButtons(String title,String msg,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst,View.IOnClickListener btn3lst)
		{
			invisible();
			Dialog.SetTitle(title);
			Button1.SetText("CANCEL", TextView.BufferType.Normal);
			Button2.SetText("OK", TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Button1.SetOnClickListener(btn1lst);
			Button2.SetOnClickListener(btn2lst);
			Button3.Visibility = ViewStates.Visible;
			Button3.SetOnClickListener(btn3lst);
			Text1.SetText(msg, TextView.BufferType.Normal);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxButtons(String title,String msg,String button1name,String button2name,String button3name,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst,View.IOnClickListener btn3lst)
		{
			invisible();
			Dialog.SetTitle(title);
			Button1.SetText(button1name, TextView.BufferType.Normal);
			Button2.SetText(button2name, TextView.BufferType.Normal);
			Button3.SetText(button3name, TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Button3.Visibility = ViewStates.Visible;
			Button1.SetOnClickListener(btn1lst);
			Button3.SetOnClickListener(btn3lst);
			Button2.SetOnClickListener(btn2lst);
			Text1.SetText(msg, TextView.BufferType.Normal);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxButtons(String title,String msg,String button1name,String button2name,String button3name,EventHandler btn1lst, EventHandler btn2lst, EventHandler btn3lst,bool usesave)
		{
			invisible();
			Dialog.SetTitle(title);
			Button1.SetText(button1name, TextView.BufferType.Normal);
			Button2.SetText(button2name, TextView.BufferType.Normal);
			Button3.SetText(button3name, TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Button3.Visibility = ViewStates.Visible;
			Button1.Click += btn1lst;
			Button2.Click += btn2lst;
			Button3.Click += btn3lst;
			Text1.SetText(msg, TextView.BufferType.Normal);
			if(usesave)
			{
				CheckBoxSaveText.Visibility = ViewStates.Visible;
				CheckBoxSave.Visibility = ViewStates.Visible;
			}
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxLogin(View.IOnClickListener btn2lst)
		{
			invisible();
			Dialog.SetTitle("Login/Password");
			Button1.SetText("CANCEL", TextView.BufferType.Normal);
			Button2.SetText("OK", TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Text1.SetText("Login", TextView.BufferType.Normal);
			Text2.Visibility = ViewStates.Visible;
			Text2.SetText("Password", TextView.BufferType.Normal);
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Edit1.Visibility = ViewStates.Visible;
			Edit2.Visibility = ViewStates.Visible;
//			btn1.SetOnClickListener (new View.IOnClickListener ());//{public void onClick(View v){dialog.cancel();}});
//			btn2.SetOnClickListener(btn2lst);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxLogin(String login, String psw, View.IOnClickListener btn2lst)
		{
			invisible();
			Dialog.SetTitle("Login/Password");
			Edit1.SetText(login, TextView.BufferType.Normal);
			Edit2.SetText(psw, TextView.BufferType.Normal);
			Button1.SetText("CANCEL", TextView.BufferType.Normal);
			Button2.SetText("OK", TextView.BufferType.Normal);
			Text1.Visibility = ViewStates.Visible;
			Text1.SetText("Login", TextView.BufferType.Normal);
			Text2.Visibility = ViewStates.Visible;
			Text2.SetText("Password", TextView.BufferType.Normal);
			Button1.Visibility = ViewStates.Visible;
			Button2.Visibility = ViewStates.Visible;
			Edit1.Visibility = ViewStates.Visible;
			Edit2.Visibility = ViewStates.Visible;
//			btn1.SetOnClickListener (new View.IOnClickListener ());//{public void onClick(View v){dialog.cancel();}});
//			btn2.SetOnClickListener(btn2lst);
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxProgress(String title,String msg,bool horizontal, int max, int progress, EventHandler btn4lst)
		{
			invisible();
			Dialog.SetTitle(title);
			Text1.Visibility = ViewStates.Visible;
			Text1.SetText(msg, TextView.BufferType.Normal);
			ProgressBar.Visibility = ViewStates.Visible;
			ProgressBar.HorizontalScrollBarEnabled = horizontal;
			ProgressBar.Progress = progress;
			ProgressBar.Max = max;
			ProgressBar.Indeterminate = false;
			Button4.Visibility = ViewStates.Visible;
			Button4.SetText("CANCEL", TextView.BufferType.Normal);
			Button4.Click += btn4lst;
			try
			{
				Dialog.Show();
			}
			catch(Exception err)
			{
				Log.Debug("xxx",err.Message);
			}
		}

		public void MsgBoxCencel()
		{
			Dialog.Cancel();
			Dialog.Dismiss();
			Dialog=null;
		}

		public void MsgBoxClose()
		{
			Dialog.Hide();
		} 
	}



}


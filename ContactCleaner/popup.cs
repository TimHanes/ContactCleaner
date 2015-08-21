﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ContactCleaner;
namespace ContactCleaner
{
	public class popup
	{
		public Dialog dialog;
		public Activity activity;
		public TextView txt1;
		public TextView txt2;
		public Button btn1;
		public Button btn2;
		public Button btn3;
		public EditText edit1;
		public EditText edit2;
		public ProgressBar pb;
		public CheckBox chbsave;
		public TextView chvsave;
		popup (Activity iactivity)
		{
			activity=iactivity;
			dialog = new Dialog(activity);
			dialog.setContentView(R.layout.popup);//popup view is the layout you created
			txt1 = (TextView)dialog.findViewById(R.id.mbtext1);
			txt2 = (TextView)dialog.findViewById(R.id.mbtext2);
			btn3=(Button)dialog.findViewById(R.id.mbbtn3);
			btn1=(Button)dialog.findViewById(R.id.mbbtn1);
			btn2=(Button)dialog.findViewById(R.id.mbbtn2);
			chbsave = (CheckBox)dialog.findViewById(R.id.checksave);
			chvsave=(TextView)dialog.findViewById(R.id.chvsave);
			edit1=(EditText)dialog.findViewById(R.id.mbedit1);
			edit2=(EditText)dialog.findViewById(R.id.mbedit2);
			pb=(ProgressBar)dialog.findViewById(R.id.mbpbar);
		}
		public void invisible()
		{
			txt1.setVisibility(View.GONE);
			txt2.setVisibility(View.GONE);
			btn3.setVisibility(View.GONE);
			btn1.setVisibility(View.GONE);//=(Button)dialog.findViewById(R.id.mbbtn1);
			btn2.setVisibility(View.GONE);//=(Button)dialog.findViewById(R.id.mbbtn2);
			chbsave.setVisibility(View.GONE);// = (CheckBox)dialog.findViewById(R.id.checksave);
			chvsave.setVisibility(View.GONE);//=(TextView)dialog.findViewById(R.id.chvsave);
			edit1.setVisibility(View.GONE);//=(EditText)dialog.findViewById(R.id.mbedit1);
			edit2.setVisibility(View.GONE);//=(EditText)dialog.findViewById(R.id.mbedit2);
			pb.setVisibility(View.GONE);//=(ProgressBar)dialog.findViewById(R.id.mbpbar);
		}
		public void MsgBox(String msg)
		{
			invisible();
			txt1.setVisibility(View.VISIBLE);
			txt1.setText(msg);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}
		public void MsgBox(String title,String msg)
		{
			invisible();
			dialog.setTitle(title);
			txt1.setVisibility(View.VISIBLE);
			txt1.setText(msg);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}
		public void MsgBoxButtons(String title,String msg,View.IOnClickListener btn2lst)
		{
			invisible();
			dialog.setTitle(title);
			btn1.setText("CANCEL");
			btn2.setText("OK");
			txt1.setVisibility(View.VISIBLE);
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			btn1.setOnClickListener(new View.IOnClickListener());
			btn2.setOnClickListener(btn2lst);
			txt1.setText(msg);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}

		}
		public void MsgBoxButtons(String title,String msg,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst)
		{
			invisible();
			dialog.setTitle(title);
			btn1.setText("CANCEL");
			btn2.setText("OK");
			txt1.setVisibility(View.VISIBLE);
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			btn1.setOnClickListener(btn1lst);
			btn2.setOnClickListener(btn2lst);
			txt1.setText(msg);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}

		}
		public void MsgBoxButtons(String title,String msg,String button1name,String button2name,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst)
		{
			invisible();
			dialog.setTitle(title);
			btn1.setText(button1name);
			btn2.setText(button2name);
			txt1.setVisibility(View.VISIBLE);
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			btn1.setOnClickListener(btn1lst);
			btn2.setOnClickListener(btn2lst);
			txt1.setText(msg);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}
		public void MsgBoxButtons(String title,String msg,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst,View.IOnClickListener btn3lst)
		{
			invisible();
			dialog.setTitle(title);
			btn1.setText("CANCEL");
			btn2.setText("OK");
			txt1.setVisibility(View.VISIBLE);
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			btn1.setOnClickListener(btn1lst);
			btn2.setOnClickListener(btn2lst);
			btn3.setVisibility(View.VISIBLE);
			btn3.setOnClickListener(btn3lst);
			txt1.setText(msg);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}

		}
		public void MsgBoxButtons(String title,String msg,String button1name,String button2name,String button3name,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst,View.IOnClickListener btn3lst)
		{
			invisible();
			dialog.setTitle(title);
			btn1.setText(button1name);
			btn2.setText(button2name);
			btn3.setText(button3name);
			txt1.setVisibility(View.VISIBLE);
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			btn3.setVisibility(View.VISIBLE);
			btn1.setOnClickListener(btn1lst);
			btn3.setOnClickListener(btn3lst);
			btn2.setOnClickListener(btn2lst);
			txt1.setText(msg);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}
		public void MsgBoxButtons(String title,String msg,String button1name,String button2name,String button3name,View.IOnClickListener btn1lst,View.IOnClickListener btn2lst,View.IOnClickListener btn3lst,bool usesave)
		{
			
			invisible();
			dialog.setTitle(title);
			btn1.setText(button1name);
			btn2.setText(button2name);
			btn3.setText(button3name);
			txt1.setVisibility(View.VISIBLE);
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			btn3.setVisibility(View.VISIBLE);
			btn1.setOnClickListener(btn1lst);
			btn3.setOnClickListener(btn3lst);
			btn2.setOnClickListener(btn2lst);
			txt1.setText(msg);
			if(usesave)
			{
				chvsave.setVisibility(View.VISIBLE);
				chbsave.setVisibility(View.VISIBLE);
			}
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}
		public void MsgBoxLogin(View.IOnClickListener btn2lst)
		{
			invisible();
			dialog.setTitle("Login/Password");
			btn1.setText("CANCEL");
			btn2.setText("OK");
			txt1.setVisibility(View.VISIBLE);
			txt1.setText("Login");
			txt2.setVisibility(View.VISIBLE);
			txt2.setText("Password");
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			edit1.setVisibility(View.VISIBLE);
			edit2.setVisibility(View.VISIBLE);
			btn1.setOnClickListener (new View.OnClickListener ());//{public void onClick(View v){dialog.cancel();}});
			btn2.setOnClickListener(btn2lst);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}
		public void MsgBoxLogin(String login, String psw, View.IOnClickListener btn2lst)
		{
			invisible();
			dialog.setTitle("Login/Password");
			edit1.setText(login);
			edit2.setText(psw);
			btn1.setText("CANCEL");
			btn2.setText("OK");
			txt1.setVisibility(View.VISIBLE);
			txt1.setText("Login");
			txt2.setVisibility(View.VISIBLE);
			txt2.setText("Password");
			btn1.setVisibility(View.VISIBLE);
			btn2.setVisibility(View.VISIBLE);
			edit1.setVisibility(View.VISIBLE);
			edit2.setVisibility(View.VISIBLE);
			btn1.setOnClickListener(new View.OnClickListener());
			btn2.setOnClickListener(btn2lst);
			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}
		public void MsgBoxProgress(String title,String msg,bool horizontal)
		{
			invisible();
			dialog.setTitle(title);
			txt1.setVisibility(View.VISIBLE);
			txt1.setText(msg);
			pb.setVisibility(View.VISIBLE);

			try
			{
				dialog.show();
			}
			catch(Exception err)
			{
				Log.d("xxx",err.getMessage());
			}
		}

		public void MsgBoxCencel()
		{
			dialog.cancel();
			dialog.dismiss();
			dialog=null;
		}
		public void MsgBoxClose()
		{
			dialog.hide();
		}
	}


}


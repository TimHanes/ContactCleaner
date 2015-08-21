
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
using System.Threading;

namespace ContactCleaner
{
			
	public	class contactsWorkRunnable 
	{
		MainActivity main;
		private Object mPauseLock;
		private bool mPaused;
		private bool mFinished;
		public Handler handler;
		ProgressDialog pd;
		//		private Activity activity;
		Context ctx;
		public Uri[] findUri;
		public int i=0;
		public contactsWorkRunnable() {
			main = main.GetMainActivity ();
			mPauseLock = new Object();
			mPaused = false;
			mFinished = false;
			//	pd=new ProgressDialog(ctx);
		}
		//	Uri[] findUri;
		//	int i=0;
		int lastSel=0;
		String currname="";
		//ContentResolver cr = getContentResolver();
		public void run() 
		{
			//			TextView tv=(TextView)activity.findViewById(R.id.tv);
			CheckBox byname=(CheckBox)findViewById(R.id.byname);
			CheckBox byphone=(CheckBox)findViewById(R.id.byphone);
			CheckBox advanced=(CheckBox)findViewById(R.id.advanced);
			//	pd.show();
			Cursor cur = getContentResolver().query(ContactsContract.Contacts.CONTENT_URI, null, null, null, null);
			if (cur.getCount() > 0) 
			{
				//				cur.moveToFirst();
				while (cur.moveToNext()&&!mFinished) 
				{
					// Do stuff.
					int id = cur.getInt(cur.getColumnIndex(ContactsContract.Contacts._ID));
					currname = cur.getString(cur.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME));
					//	Message.obtain(mhandler,2,"work with:"+id+"-"+name+"\r\n").sendToTarget();
					Message.obtain(mhandler,3,cur.getCount(),cur.getPosition(),currname+"\r\n"+((lastSel==0)||(!p.chbsave.isChecked())?"":"(default:"+(lastSel==1?"Delete":(lastSel==2?"Join":(lastSel==3?"Ignore":"Unknow")))+")")).sendToTarget();
					//	tv.append("work with:"+name+"\r\n");
					if((byname.isChecked())&(byphone.isChecked()))
					{
						findUri=getContactsUriByName(currname,false);
						checkContacts(id,cur);	
						String[] phones=phonesByCursor(cur).split("\r\n");
						for(int p=0;p<phones.length;p++)
						{
							findUri=getContactUrisByPhone(phones[p]);
							checkContacts(id,cur);	
						}
					}else
						if(byname.isChecked())
						{
							findUri=getContactsUriByName(currname,false);
							checkContacts(id,cur);		
						}else
							if(byphone.isChecked())
							{
								//	strin
								try{
									String[] phones=phonesByCursor(cur).split("\r\n");
									for(int p=0;p<phones.length;p++)
									{
										findUri=getContactUrisByPhone(phones[p]);
										if(findUri!=null)
											checkContacts(id,cur);	
									}
								}
								catch(Exception err)
								{
									Log.e("byphones",err.getMessage());
								}
							}


				}
				Message.obtain(mhandler,4).sendToTarget();
			}

		}
		void checkContacts(int id, CursorLoader cur)
		{

			for(i=0;i<=findUri.length-1;i++)
			{
				int fid=idByUri(findUri[i]);
				if(fid>id)
				{
					String fname=nameByUri(findUri[i]);
					//	Message.obtain(mhandler,0,"find:"+fid+"-"+fname+"\r\n").sendToTarget();//append to tv
					if(!p.chbsave.isChecked())
					{
						Message.obtain(mhandler,1,"Work with:"+currname+"\r\n"+phonesByCursor(cur)+"find:"+ fname+"\r\n"+phonesByUri(findUri[i])).sendToTarget();//
						mPaused=true;
					}
					else
					{
						switch(lastSel)
						{
						case 0:
							break;
						case 1:
							contactDelete(findUri[i]);
							break;
						case 2:
							contactJoin(findUri[i]);
							break;
						case 3:
							break;
						}
					}
				}

				synchronized(mPauseLock) 
				{
					while(mPaused) 
					{
						try 
						{
							mPauseLock.wait();
						} catch(InterruptedException e) 
						{}
					}
				}
			}		
		}
		public String phonesByUri(Uri uri)
		{
			Cursor cur=getContentResolver().query(uri, null, null, null, null);
			cur.moveToFirst();
			String phones="";
			int id = cur.getInt(cur.getColumnIndex(ContactsContract.Contacts._ID));
			if (Integer.parseInt(cur.getString(cur.getColumnIndex(ContactsContract.Contacts.HAS_PHONE_NUMBER))) > 0)					 
			{
				Cursor pCur = getContentResolver().query(ContactsContract.CommonDataKinds.Phone.CONTENT_URI,null,ContactsContract.CommonDataKinds.Phone.CONTACT_ID +" = "+id,null, null);
				while (pCur.moveToNext()) 
				{
					phones+=pCur.getString(pCur.getColumnIndex(ContactsContract.CommonDataKinds.Phone.DATA))+"\r\n";
				}
				pCur.close();
			}
			return phones;
		}
		public String phonesByCursor(CursorLoader cur)
		{
			String phones="";
			int id = cur.getInt(cur.getColumnIndex(ContactsContract.Contacts._ID));
			if (Integer.parseInt(cur.getString(cur.getColumnIndex(ContactsContract.Contacts.HAS_PHONE_NUMBER))) > 0)					 
			{
				Cursor pCur = getContentResolver().query(ContactsContract.CommonDataKinds.Phone.CONTENT_URI,null,ContactsContract.CommonDataKinds.Phone.CONTACT_ID +" = "+id,null, null);
				while (pCur.moveToNext()) 
				{
					phones+=pCur.getString(pCur.getColumnIndex(ContactsContract.CommonDataKinds.Phone.DATA))+"\r\n";
				}
				pCur.close();
			}
			return phones;
		}
		public void contactDelete(Uri uri)
		{
			lastSel=1;
			Message.obtain(mhandler,0,"deleted:"+ nameByUri(uri)+"\r\n").sendToTarget();//
			getContentResolver().delete(uri,null,null);
		}
		public void contactJoin(Uri uri)
		{
			lastSel=2;
			Message.obtain(mhandler,0,"joined:"+ nameByUri(uri)+"\r\n").sendToTarget();//
			//	cr.delete(findUri[i],null,null);
		}
		public void contactIgnore()
		{
			lastSel=3;
			//cr.delete(findUri[i],null,null);
		}
		/**
		 * Call this on pause.
		 */
		public void onPause() {
			synchronized(mPauseLock) {
				mPaused = true;
			}
		}
		/**
		 * Call this on resume.
		 */
		public void onResume() {
			synchronized(mPauseLock) {
				mPaused = false;
				mPauseLock.notifyAll();
			}
		}

		public void onFinished() {
			mFinished=true;
		}
	}
}


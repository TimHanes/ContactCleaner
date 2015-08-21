using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ContactCleaner;
using System.Linq;
using System.Collections;

namespace ContactCleaner
{
	
	public class MainActivity : Activity
	{
		static MainActivity main;

		void MainActivioty()
		{
			main = new MainActivity ();
		
		}

		public MainActivity GetMainActivity()
		{
			return main;
		}


		TextView tv;
		public ProgressDialog pd;
		MHandler mhandler = new MHandler();

		public popup p;
		public contactsWorkRunnable cwr;
		/** Called when the activity is first created. */

		override public void onCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			base.SetContentView(R.layout.main);
			tv = (TextView)base.FindViewById(R.id.tv);
			p = new popup(this);
			pd = new ProgressDialog(this);
			//	pd.setIndeterminate(true);
			pd.SetProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
			pd.SetButton("Cancel",dlstcancel);
		}

		Dlstcancel dlstcancel = new Dlstcancel();

		Lstbtndel lstbtndel =new Lstbtndel();
		Lstbtnigt lstbtnign = new Lstbtnigt();
		Lstbtnjoin lstbtnjoin = new Lstbtnjoin();


		public void bstart(View v)
		{
			//readcontacts();
			p.chbsave.setChecked(false);
			tv.setText("");
			cwr=new contactsWorkRunnable();
			try{
				Thread ct=new Thread(cwr);
				ct.setDaemon(true);
				ct.start();
			}
			catch(Exception err)
			{
				Log.e("xxx",err.getMessage());
			}
		}

		public String nameByUri(Uri uri)
		{
			Cursor crs=getContentResolver().query(uri,null,null,null,null);
			crs.moveToFirst();
			return crs.getString(crs.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME));
		}
		public int idByUri(Uri uri)
		{
			Cursor crs=getContentResolver().query(uri,null,null,null,null);
			crs.moveToFirst();
			return crs.getInt(crs.getColumnIndex(ContactsContract.Contacts._ID));
		}
		public Uri[] getContactUrisByPhone(String phone)
		{
			//	TextView tv=(TextView)findViewById(R.id.tv);
			Uri contactUri =Uri.withAppendedPath(PhoneLookup.CONTENT_FILTER_URI, Uri.encode(phone));
			Cursor cur =getContentResolver().query(contactUri, null, null, null,null);
			if((cur!=null)&(cur.getCount()>0))
			{
				Uri[] contactsUri=new Uri[cur.getCount()];

				try {
					if (cur.moveToFirst())
					{
						int c=0;
						do {
							String lookupKey = cur.getString(cur.getColumnIndex(ContactsContract.Contacts.LOOKUP_KEY));
							Uri uri =Uri.withAppendedPath(ContactsContract.Contacts.CONTENT_LOOKUP_URI, lookupKey);
							contactsUri[c]=uri;
							c++;
							//		tv.append("\r\n"+cur.getString(cur.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME)));
						} while(cur.moveToNext());
					}
				} catch (Exception e) {
					Log.e("xxx",e.getMessage());
				}
				cur.close();
				return contactsUri;
			}
			else
			{
				cur.close();
				return null;
			}


		}
		public Uri[] getContactsUriByName(String selname,bool ignorecase)
		{
			//Uri contactUri =Uri.withAppendedPath(PhoneLookup.CONTENT_FILTER_URI, Uri.encode(name));
			//TextView tv=(TextView)findViewById(R.id.tv);
			Uri[] contactsUri=null;
			try {
				Cursor cur = getContentResolver().query(ContactsContract.Contacts.CONTENT_URI,null, (!ignorecase)?(ContactsContract.Contacts.DISPLAY_NAME+" LIKE '"+selname+"'"):("UPPER("+ContactsContract.Contacts.DISPLAY_NAME+") LIKE UPPER('"+selname+"')"),null,null);
				contactsUri=new Uri[cur.getCount()];

				if (cur.moveToFirst())
				{
					int c=0;
					do {
						String lookupKey = cur.getString(cur.getColumnIndex(ContactsContract.Contacts.LOOKUP_KEY));
						Uri uri =Uri.withAppendedPath(ContactsContract.Contacts.CONTENT_LOOKUP_URI, lookupKey);
						contactsUri[c]=uri;
						c++;
						//		tv.append("\r\n"+cur.getString(cur.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME)));
					} while(cur.moveToNext());
				}
				cur.close();
			} catch (Exception e) {
				Log.e("xxx",e.getMessage());
			}
			return contactsUri;
		}
	}
	//	public static boolean deleteContact(Context ctx,String phone, String name) {
	//		Uri contactUri =Uri.withAppendedPath(PhoneLookup.CONTENT_FILTER_URI, Uri.encode(phone));
	//		Cursor cur =ctx.getContentResolver().query(contactUri, null, null, null,null);
	//		try {
	//			if (cur.moveToFirst()
	//			{
	//				do {
	//					if(cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)).equalsIgnoreCase(name)) {
	//						String lookupKey = cur.getString(cur.getColumnIndex(ContactsContract.Contacts.LOOKUP_KEY));
	//						Uri uri =Uri.withAppendedPath(ContactsContract.Contacts.CONTENT_LOOKUP_URI, lookupKey);
	//						ctx.getContentResolver().delete(uri, null, null);
	//						Log.i("xxx-deleted",name);
	//						return true;
	//					}
	//				//	Log.i("xxx-name",cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)));
	//				} while(cur.moveToNext());
	//			}
	//		} catch (Exception e) {
	//			//System.out.println(e.getStackTrace());
	//			Log.e("xxx",e.getMessage());
	//		}
	//		return false;
	//	}
	//	public static boolean deleteContact(Context ctx,String phone) {
	//		Uri contactUri =Uri.withAppendedPath(PhoneLookup.CONTENT_FILTER_URI, Uri.encode(phone));
	//		Cursor cur =ctx.getContentResolver().query(contactUri, null, null, null,null);
	//		try {
	//			if (cur.moveToFirst())
	//			{
	//				do {
	//					if(true)//cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)).equalsIgnoreCase(name)) 
	//					{
	//						String lookupKey = cur.getString(cur.getColumnIndex(ContactsContract.Contacts.LOOKUP_KEY));
	//						Uri uri =Uri.withAppendedPath(ContactsContract.Contacts.CONTENT_LOOKUP_URI, lookupKey);
	//						ctx.getContentResolver().delete(uri, null, null);
	//						Log.i("xxx-deleted",phone);
	//						return true;
	//					}
	//					//	Log.i("xxx-name",cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)));
	//				} while(cur.moveToNext());
	//			}
	//		} catch (Exception e) {
	//			//System.out.println(e.getStackTrace());
	//			Log.e("xxx",e.getMessage());
	//		}
	//		return false;
	//	}
	//	public static boolean deleteContactByName(Context ctx,String name) {
	//		Uri contactUri =Uri.withAppendedPath(PhoneLookup.CONTENT_FILTER_URI, Uri.encode(name));
	//		Cursor cur =ctx.getContentResolver().query(contactUri, null, null, null,null);
	//		try {
	//			if (cur.moveToFirst())
	//			{
	//				do {
	//					if(cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)).equalsIgnoreCase(name)) {
	//						String lookupKey = cur.getString(cur.getColumnIndex(ContactsContract.Contacts.LOOKUP_KEY));
	//						Uri uri =Uri.withAppendedPath(ContactsContract.Contacts.CONTENT_LOOKUP_URI, lookupKey);
	//						ctx.getContentResolver().delete(uri, null, null);
	//						Log.i("xxx-deleted",name);
	//						return true;
	//					}
	//					//	Log.i("xxx-name",cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)));
	//				} while(cur.moveToNext());
	//			}
	//		} catch (Exception e) {
	//			//System.out.println(e.getStackTrace());
	//			Log.e("xxx",e.getMessage());
	//		}
	//		return false;
	//	}
	//	void deleteAllContacts()
	//	{
	//		ContentResolver cr =getContentResolver();
	//		Cursor cur = cr.query(ContactsContract.Contacts.CONTENT_URI,null, null, null, null);
	//		while (cur.moveToNext()) {
	//			try{
	//				Log.i("contact-",cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)));
	//				String lookupKey =cur.getString(cur.getColumnIndex(ContactsContract.Contacts.LOOKUP_KEY));
	//				Uri uri =Uri.withAppendedPath(ContactsContract.Contacts.CONTENT_LOOKUP_URI, lookupKey);
	//				System.out.println("The uri is " + uri.toString());
	//				
	//				cr.delete(uri,null, null);
	//			}
	//			catch(Exception e)
	//			{
	//				System.out.println(e.getStackTrace());
	//			}
	//		}
	//	}
	//	void deleteDoubleContacts(boolean checkname,boolean checkphone, boolean consolid, boolean ask)
	//	{
	//		try{
	//		ContentResolver cr =getContentResolver();
	//		Cursor cur = cr.query(ContactsContract.Contacts.CONTENT_URI,null, null, null,checkname?PhoneLookup.DISPLAY_NAME:PhoneLookup.NUMBER);
	//		String tempname="temp string for contact name";
	//		String tempphone="temp string for contact phone";
	//		int del=0;
	//		TextView tv=(TextView)findViewById(R.id.tv);
	//		
	//		while (cur.moveToNext()) {
	//			
	//			try{
	//			
	//				String lookupKey =cur.getString(cur.getColumnIndex(ContactsContract.Contacts.LOOKUP_KEY));
	//				Uri uri =Uri.withAppendedPath(ContactsContract.Contacts.CONTENT_LOOKUP_URI, lookupKey);
	//				//System.out.println("The uri is " + uri.toString());
	//				String name=cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME));
	//				String phone=cur.getString(cur.getColumnIndex(PhoneLookup.HAS_PHONE_NUMBER));
	//				if((checkname|checkphone)&(((name.equals(tempname))|!checkname)&((phone.equals(tempphone))|!checkphone)))//|(((name.equals(tempname))&&checkname)&&((phone.equals(tempphone))&&checkphone)))
	//				{
	//					tv.append("\r\ndeleted contact-"+cur.getString(cur.getColumnIndex(PhoneLookup.DISPLAY_NAME)));
	//					Log.i("deleted contact-",cur.getString(cur.getColumnIndex(PhoneLookup.HAS_PHONE_NUMBER)));
	//					//cr.delete(uri,null, null);
	//					del++;
	//				}
	//				else 
	//				{
	//					tempname=name;
	//					tempphone=phone;
	//				}
	//			}
	//			catch(Exception e)
	//			{
	//				Log.e("xxx-error",e.getMessage());
	//			}
	//		}
	//	
	//		tv.append("\r\ndeleted "+del+" contacts from "+cur.getCount());
	//		Log.i("xxx","deleted "+del+" contacts from "+cur.getCount());
	//		}
	//		catch(Exception e)
	//		{
	//			Log.e("xxx-error",e.getMessage());
	//		}
	//	}
	//	
	//	Cursor emailCur = cr.query(
	//		ContactsContract.CommonDataKinds.Email.CONTENT_URI,
	//		null,
	//		ContactsContract.CommonDataKinds.Email.CONTACT_ID + " = ?",
	//		new String[]{id}, null);
	//	while (emailCur.moveToNext()) {
	//		// This would allow you get several email addresses
	//// if the email addresses were stored in an array
	//		String email = emailCur.getString(
	//			emailCur.getColumnIndex(ContactsContract.CommonDataKinds.Email.DATA));
	//		String emailType = emailCur.getString(
	//			emailCur.getColumnIndex(ContactsContract.CommonDataKinds.Email.TYPE));
	//	}
	//emailCur.close();

	//	String noteWhere = ContactsContract.Data.CONTACT_ID + " = ? AND " + ContactsContract.Data.MIMETYPE + " = ?";
	//	String[] noteWhereParams = new String[]{id,
	//		ContactsContract.CommonDataKinds.Note.CONTENT_ITEM_TYPE};
	//	Cursor noteCur = cr.query(ContactsContract.Data.CONTENT_URI, null, noteWhere, noteWhereParams, null);
	//	if (noteCur.moveToFirst()) {
	//		String note = noteCur.getString(noteCur.getColumnIndex(ContactsContract.CommonDataKinds.Note.NOTE));
	//	}
	//noteCur.close();
	//	
	//	String addrWhere = ContactsContract.Data.CONTACT_ID + " = ? AND " + ContactsContract.Data.MIMETYPE + " = ?";
	//	String[] addrWhereParams = new String[]{id,
	//		ContactsContract.CommonDataKinds.StructuredPostal.CONTENT_ITEM_TYPE};
	//	Cursor addrCur = cr.query(ContactsContract.Data.CONTENT_URI,
	//							  null, where, whereParameters, null);
	//	while(addrCur.moveToNext()) {
	//		String poBox = addrCur.getString(
	//			addrCur.getColumnIndex(ContactsContract.CommonDataKinds.StructuredPostal.POBOX));
	//		String street = addrCur.getString(
	//			addrCur.getColumnIndex(ContactsContract.CommonDataKinds.StructuredPostal.STREET));
	//		String city = addrCur.getString(
	//			addrCur.getColumnIndex(ContactsContract.CommonDataKinds.StructuredPostal.CITY));
	//		String state = addrCur.getString(
	//			addrCur.getColumnIndex(ContactsContract.CommonDataKinds.StructuredPostal.REGION));
	//		String postalCode = addrCur.getString(
	//			addrCur.getColumnIndex(ContactsContract.CommonDataKinds.StructuredPostal.POSTCODE));
	//		String country = addrCur.getString(
	//			addrCur.getColumnIndex(ContactsContract.CommonDataKinds.StructuredPostal.COUNTRY));
	//		String type = addrCur.getString(
	//			addrCur.getColumnIndex(ContactsContract.CommonDataKinds.StructuredPostal.TYPE));
	//	}
	//addrCur.close();
	//	
	//	String imWhere = ContactsContract.Data.CONTACT_ID + " = ? AND " + ContactsContract.Data.MIMETYPE + " = ?";
	//	String[] imWhereParams = new String[]{id,
	//		ContactsContract.CommonDataKinds.Im.CONTENT_ITEM_TYPE};
	//	Cursor imCur = cr.query(ContactsContract.Data.CONTENT_URI,
	//							null, imWhere, imWhereParams, null);
	//	if (imCur.moveToFirst()) {
	//		String imName = imCur.getString(
	//			imCur.getColumnIndex(ContactsContract.CommonDataKinds.Im.DATA));
	//		String imType;
	//		imType = imCur.getString(
	//			imCur.getColumnIndex(ContactsContract.CommonDataKinds.Im.TYPE));
	//	}
	//imCur.close();
	//	
	//	String orgWhere = ContactsContract.Data.CONTACT_ID + " = ? AND " + ContactsContract.Data.MIMETYPE + " = ?";
	//	String[] orgWhereParams = new String[]{id,
	//		ContactsContract.CommonDataKinds.Organization.CONTENT_ITEM_TYPE};
	//	Cursor orgCur = cr.query(ContactsContract.Data.CONTENT_URI,
	//							 null, orgWhere, orgWhereParams, null);
	//	if (orgCur.moveToFirst()) {
	//		String orgName = orgCur.getString(orgCur.getColumnIndex(ContactsContract.CommonDataKinds.Organization.DATA));
	//		String title = orgCur.getString(orgCur.getColumnIndex(ContactsContract.CommonDataKinds.Organization.TITLE));
	//	}
	//orgCur.close();

}



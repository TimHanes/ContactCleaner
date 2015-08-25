using System.Threading.Tasks;
using Android.OS;
using Android.Content;
using Android.App;
using Android.Widget;
using Android.Views;
using Android.Database;
using Android.Provider;
using Android.Util;
using Android.Net;
using ContactCleaner.Core;
using ContactCleaner.Models;

namespace ContactCleaner
{
	public class ContactsHandler : BaseThread
	{
		private bool _mFinished = false;
		Context _context = Application.Context;
		public MessageHandler _mhandler;
		ActionType _defaultAction = ActionType.None;
		public int currentContactIndex = 0;

		public ContactsHandler()
		{
			_mhandler = new MessageHandler(this);
		}

		public Uri[] FoundUri { get; private set; }

		override protected void Run()
		{
			Looper.Prepare();
			var options = new Options ();

			var loader = new CursorLoader (Application.Context, ContactsContract.Contacts.ContentUri, null, null, null, null);

			ICursor cur;
			cur = (ICursor)loader.LoadInBackground ();

			cur.MoveToFirst ();
								
			while (cur.MoveToNext () && !_mFinished) 
			{
				SearchDublicate (cur, options);
			}
	
			Message.Obtain(_mhandler,(int)MessageType.Finally).SendToTarget();
			base.Run ();
		}	

		private void SearchDublicate (ICursor cur, Options options)
		{
			var contact = new Contact (cur);

			DisplayInfo (cur, contact);

			if ((options.ByName) & (options.ByPhone)) {
				CheckByNameAndPhone (contact, cur);
				return;
			}

			if (options.ByName) {
				CheckByName (contact, cur);
				return;
			}
				
			if (options.ByPhone) {
				CheckByPhone (contact, cur);
			}
		}

		private void CheckByPhone(Contact contact, ICursor cur)
		{
			try {
				System.String[] phones = PhonesByCursor (cur).Split ('\r', '\n');
				for (int p = 0; p < phones.Length; p++) {
					FoundUri = GetContactUrisByPhone (phones [p]);
					if (FoundUri != null)
						CheckContacts (contact, cur);
				}
			}
			catch (System.Exception err) {
				Log.Error ("byphones", err.Message);
			}
		}

		private void CheckByName(Contact contact, ICursor cur)
		{
			FoundUri = GetContactsUriByName (contact.Name, false);
			CheckContacts (contact, cur);
		}

		private void CheckByNameAndPhone(Contact contact, ICursor cur)
		{
			FoundUri = GetContactsUriByName (contact.Name, false);
			CheckContacts (contact, cur);
			System.String[] phones = PhonesByCursor (cur).Split ('\r', '\n');
			for (int p = 0; p < phones.Length; p++) {
				FoundUri = GetContactUrisByPhone (phones [p]);
				CheckContacts (contact, cur);
			}
		}

		private void DisplayInfo (ICursor cur, Contact contact)
		{
			Message.Obtain (_mhandler, (int)MessageType.SetTextToLogView, "work with:" + contact.Id + "-" + contact.Name + "\r\n").SendToTarget ();
			Message.Obtain (_mhandler, (int)MessageType.ShowProgress, cur.Count, cur.Position, contact.Name + "\r\n" + ((_defaultAction == ActionType.None) ? "" : "(default:" + _defaultAction.ToString () + ")")).SendToTarget ();
		}
 
			
		private void CheckContacts(Contact contact,ICursor cur)
		{
			if (FoundUri != null) {
				for ( currentContactIndex = 0; currentContactIndex <= FoundUri.Length - 1; currentContactIndex++) {
					CompaireContact(contact, cur);	
				}
			}
		}		

		private void CompaireContact(Contact contact, ICursor cur)
		{
			int fid = IdByUri (FoundUri [currentContactIndex]);
			if (fid > contact.Id) {
				System.String fname = NameByUri (FoundUri [currentContactIndex]);
				Message.Obtain (_mhandler, (int)MessageType.AddToLogView, "find:" + fid + "-" + fname + "\r\n").SendToTarget ();//append to tv
				if (!App.Current.Popup.CheckBoxSave.Checked) {
					Message.Obtain (_mhandler, (int)MessageType.ShowPopupForChooseAction, "Work with:" + contact.Name + "\r\n" + PhonesByCursor (cur) + "find:" + fname + "\r\n" + PhonesByUri (FoundUri [currentContactIndex])).SendToTarget ();//
					this.Pause();
				} else {

					switch (_defaultAction) {
					case ActionType.None:
						break;
					case ActionType.Delete:
						ContactDelete(FoundUri[currentContactIndex]);
						break;
					case ActionType.Join:
						ContactJoin(FoundUri[currentContactIndex]);
						break;
					case ActionType.Ignore:
						break;
					}

				}

			}
		}

		public System.String PhonesByUri(Uri uri)
		{
			var loader = new CursorLoader (_context, uri, null, null, null, null);
			ICursor cur;
			cur = (ICursor)loader.LoadInBackground ();

			cur.MoveToFirst();
			System.String phones="";
			int id = cur.GetInt(cur.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.Id));
			if (System.Int16.Parse(cur.GetString(cur.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber))) > 0)					 
			{
				var _loader = new CursorLoader (_context, ContactsContract.CommonDataKinds.Phone.ContentUri,null,ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId +" = "+id,null, null);
				ICursor pCur;
				pCur = (ICursor)_loader.LoadInBackground ();
				while (pCur.MoveToNext()) 
				{
					phones+=pCur.GetString(pCur.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Data))+"\r\n";
				}
				pCur.Close();
			}
			return phones;
		}

		public System.String PhonesByCursor(ICursor cur)
		{
			System.String phones="";
			int id = cur.GetInt(cur.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.Id));
			if (System.Int16.Parse(cur.GetString(cur.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber))) > 0)					 
			{
				var loader = new CursorLoader (_context, ContactsContract.CommonDataKinds.Phone.ContentUri,null,ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId +" = "+id,null, null);
				ICursor pCur;
				pCur = (ICursor)loader.LoadInBackground();

			while (pCur.MoveToNext()) 
				{
					phones+=pCur.GetString(pCur.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Data))+"\r\n";
				}
				pCur.Close();
			}
			return phones;
		} 

		public void ContactDelete(Uri uri)
		{
			_defaultAction=ActionType.Delete;
			Message.Obtain(_mhandler,(int)MessageType.AddToLogView,"deleted:"+ NameByUri(uri)+"\r\n").SendToTarget();//
			_context.ContentResolver.Delete(uri,null,null);
		}

		public void ContactJoin(Uri uri)
		{
			_defaultAction=ActionType.Join;
			Message.Obtain(_mhandler,(int)MessageType.AddToLogView,"joined:"+ NameByUri(uri)+"\r\n").SendToTarget();//
		}

		public void ContactIgnore()
		{
			_defaultAction=ActionType.Ignore;
		}	

		public void OnFinished() {
			_mFinished=true;
		}
	

		public static void DeleteContact(Context context, string lookupKey)
		{
			var uri = Uri.WithAppendedPath(ContactsContract.Contacts.ContentLookupUri, lookupKey);
			context.ContentResolver.Delete(uri, null, null);
		}

	
 		public Uri[] GetContactsUriByName(System.String selname,bool ignorecase)
		{
				var loader = new CursorLoader (_context, ContactsContract.Contacts.ContentUri, null,
					             (!ignorecase) ? (ContactsContract.Contacts.InterfaceConsts.DisplayName + " LIKE '" + selname + "'") : ("UPPER(" + ContactsContract.Contacts.InterfaceConsts.DisplayName + ") LIKE UPPER('" + selname + "')"), null, null);
				ICursor cur;
				cur = (ICursor)loader.LoadInBackground ();


				Uri[] contactsUri = new Uri[cur.Count];

				if (cur.MoveToFirst ()) {
					int c = 0;
					do {
						System.String lookupKey = cur.GetString (cur.GetColumnIndex (ContactsContract.Contacts.InterfaceConsts.LookupKey));
						Uri uri = Uri.WithAppendedPath (ContactsContract.Contacts.ContentLookupUri, lookupKey);
						contactsUri [c] = uri;
						c++;
					} while(cur.MoveToNext ());
				}
				cur.Close ();
			return contactsUri;
		}
	

		 public int IdByUri(Uri uri)
		{
			var loader = new CursorLoader (_context, uri, null, null, null, null);
			ICursor cur;
			cur = (ICursor)loader.LoadInBackground();
			cur.MoveToFirst();
			return cur.GetInt(cur.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.Id));
		} 

		public System.String NameByUri(Uri uri)
		{
			var loader = new CursorLoader (_context, uri, null, null, null, null);
			ICursor cur;
			cur = (ICursor)loader.LoadInBackground();

						
			cur.MoveToFirst();
			return cur.GetString(cur.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayName));
		}

		public Uri[] GetContactUrisByPhone(System.String phone)
		{
					Uri contactUri = Uri.WithAppendedPath(ContactsContract.PhoneLookup.ContentFilterUri, Uri.Encode(phone));
					
			var loader = new CursorLoader (_context, contactUri, null, null, null, null);
			ICursor cur;
			cur = (ICursor)loader.LoadInBackground();


			if((cur!=null)&(cur.Count>0))
					{
						Uri[] contactsUri=new Uri[cur.Count];
		
						try {
							if (cur.MoveToFirst())
							{
								int c=0;
								do {
									System.String lookupKey = cur.GetString(cur.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.LookupKey));
									Uri uri = Uri.WithAppendedPath(ContactsContract.Contacts.ContentLookupUri, lookupKey);
									contactsUri[c]=uri;
									c++;
									//		tv.append("\r\n"+cur.getString(cur.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME)));
								} while(cur.MoveToNext());
							}
						} catch (System.Exception e) {
							Log.Error("xxx",e.Message);
						}
						cur.Close();
						return contactsUri;
					}
				 	else
					{
						cur.Close();
						return null;
					}
		
		}

	} 
}


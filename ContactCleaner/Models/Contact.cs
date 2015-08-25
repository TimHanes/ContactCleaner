using System;
using Android.Database;
using Android.Provider;

namespace ContactCleaner
{
	public class Contact
	{
		public Contact (ICursor cur)
		{
			Id = cur.GetInt (cur.GetColumnIndex (ContactsContract.Contacts.InterfaceConsts.Id));
			Name = cur.GetString (cur.GetColumnIndex (ContactsContract.Contacts.InterfaceConsts.DisplayName));
		}

		public int Id { get; set; }
		public string Name { get; set; }
	}
}


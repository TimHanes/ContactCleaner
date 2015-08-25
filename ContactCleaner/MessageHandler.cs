using System;
using Android.OS;
using Android.Widget;
using Android.App;
using Android.Content;
using Java.Text;

namespace ContactCleaner
{
	public class MessageHandler : Handler
	{
		private TextView _logView;
		private ContactsHandler _contactsHandler;

		public MessageHandler(ContactsHandler contactsHandler)
		{
			_contactsHandler = contactsHandler;
			_logView = App.Current.RelativeLayout.FindViewById<TextView>(Resource.Id.tv);
			App.Current.Popup.invisible ();
		}

		override public void HandleMessage(Message msg)
		{
			switch (msg.What) {
			case (int)MessageType.AddToLogView:
				AddToLogView (msg);
				break;
			case (int)MessageType.ShowPopupForChooseAction:
				ShowPopupForChooseAction (msg);
				break;
			case (int)MessageType.SetTextToLogView:
				SetTextToLogView (msg);
				break;
			case (int)MessageType.ShowProgress:
				ShowProgress(msg);
				break;
			case (int)MessageType.Finally:
				Finally(msg);
				break;
			case (int)MessageType.SetProgressBar:
				SetProgressBar(msg);
				break;
			}
		}

		private void SetProgressBar(Message msg)
		{
			App.Current.Popup.ProgressBar.Max = msg.Arg1;
			App.Current.Popup.ProgressBar.Progress = msg.Arg2;
		}

		private void Finally(Message msg)
		{
			App.Current.Popup.MsgBoxClose ();
			App.Current.ProgressShower.Dismiss ();
		}

		private void ShowProgress(Message msg)
		{
			App.Current.ProgressShower.Max = msg.Arg1;
			App.Current.ProgressShower.Progress = msg.Arg2;
			App.Current.ProgressShower.SetMessage (msg.Obj.ToString ());
			App.Current.ProgressShower.Show ();
			App.Current.Popup.MsgBoxProgress("Processing...",msg.Obj.ToString(),true);
		}

		private void SetTextToLogView(Message msg)
		{
			_logView.SetText (msg.Obj.ToString (), TextView.BufferType.Normal);
		}

		private void AddToLogView(Message msg)
		{
			_logView.Append (msg.Obj.ToString ());
		}

		private void ShowPopupForChooseAction(Message msg)
		{
			App.Current.ProgressShower.Cancel ();

			EventHandler _onButtonDelete = delegate {
				App.Current.Popup.MsgBoxClose();
				_contactsHandler.ContactDelete(
					_contactsHandler.FoundUri[_contactsHandler.currentContactIndex]);
				_contactsHandler.OnResume();
				_contactsHandler.Resume();
			};

			EventHandler _onButtonJoin = delegate {
				App.Current.Popup.MsgBoxClose();
				_contactsHandler.ContactJoin (
					_contactsHandler.FoundUri [_contactsHandler.currentContactIndex]);
				_contactsHandler.OnResume();
				_contactsHandler.Resume();
			};

			EventHandler _onButtonIgnore = delegate {
				App.Current.Popup.MsgBoxClose();
				_contactsHandler.ContactIgnore();
				_contactsHandler.OnResume();
				_contactsHandler.Resume();
			};

			App.Current.Popup.MsgBoxButtons ("What do with:", msg.Obj.ToString () + " ?", "Delete", "Join", "Ignore", _onButtonDelete, _onButtonJoin, _onButtonIgnore, true);
		}


	}
}


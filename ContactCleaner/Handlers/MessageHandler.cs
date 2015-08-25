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
			_logView = App.Instance.RelativeLayout.FindViewById<TextView>(Resource.Id.tv);
			App.Instance.Popup.invisible ();
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
			App.Instance.Popup.ProgressBar.Max = msg.Arg1;
			App.Instance.Popup.ProgressBar.Progress = msg.Arg2;
		}

		private void Finally(Message msg)
		{
			App.Instance.Popup.MsgBoxClose ();
			App.Instance.ProgressShower.Dismiss ();
		}

		private void ShowProgress(Message msg)
		{
			App.Instance.ProgressShower.Max = msg.Arg1;
			App.Instance.ProgressShower.Progress = msg.Arg2;
			App.Instance.ProgressShower.SetMessage (msg.Obj.ToString ());
			App.Instance.ProgressShower.Show ();
			App.Instance.Popup.MsgBoxProgress("Processing...",msg.Obj.ToString(),true);
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
			App.Instance.ProgressShower.Cancel ();

			EventHandler _onButtonDelete = delegate {
				App.Instance.Popup.MsgBoxClose();
				_contactsHandler.ContactDelete(
					_contactsHandler.FoundUri[_contactsHandler.currentContactIndex]);
				_contactsHandler.Resume();
			};

			EventHandler _onButtonJoin = delegate {
				App.Instance.Popup.MsgBoxClose();
				_contactsHandler.ContactJoin (
					_contactsHandler.FoundUri [_contactsHandler.currentContactIndex]);
				_contactsHandler.Resume();
			};

			EventHandler _onButtonIgnore = delegate {
				App.Instance.Popup.MsgBoxClose();
				_contactsHandler.ContactIgnore();
				_contactsHandler.Resume();
			};

			App.Instance.Popup.MsgBoxButtons ("What do with:", msg.Obj.ToString () + " ?", "Delete", "Join", "Ignore", _onButtonDelete, _onButtonJoin, _onButtonIgnore, true);
		}


	}
}


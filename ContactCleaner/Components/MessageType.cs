using System;

namespace ContactCleaner
{
	public enum MessageType
	{
		Unknown = 0,
		AddToLogView = 1,
		ShowPopupForChooseAction = 2,
		SetTextToLogView = 3,
		ShowProgress = 4,
		Finally = 5,
		SetProgressBar = 6
	}
}


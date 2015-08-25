using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;
using ContactCleaner.Core;

namespace ContactCleaner.UITests
{
	[TestFixture]
	public class BaseThreadTests
	{
		//AndroidApp app;
		BaseThread testThread;

		[SetUp]
		public void BeforeEachTest ()
		{
			testThread = new BaseThread ();
		}

		[Test]
		public void OnPauseShouldTurnToPause ()
		{
			// Arrange

			// Act
			testThread.OnPause();

			// Assert
			Assert.AreEqual(true, testThread.IsPaused);
		}

		[Test]
		public void OnResumeShouldContinueThread ()
		{
			// Arrange

			// Act
			testThread.OnResume();

			// Assert
			Assert.AreEqual(false, testThread.IsPaused);
		}
	}
}


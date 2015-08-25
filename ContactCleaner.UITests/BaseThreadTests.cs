using System;
using System.Threading;
using ContactCleaner.Core;
using NUnit.Framework;


namespace ContactCleaner.UITests
{
	[TestFixture]
	public class BaseThreadTests
	{
		//AndroidApp app;
		BaseThread testThread;
		ThreadState _state1;
		ThreadState _state2;

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
			testThread.Start();
			_state1 = testThread.IsThreadState;
			Thread.Sleep (500);
			_state2 = testThread.IsThreadState;
		

			// Assert
			Assert.AreEqual(ThreadState.WaitSleepJoin, _state1);
			Assert.AreEqual(ThreadState.Stopped, _state2);
		}

		[Test]
		public void OnResumeShouldContinueThread ()
		{
			// Arrange

			// Act
			testThread.Start();
//			testThread.Pause ();
			testThread.Resume ();
			_state1 = testThread.IsThreadState;
			Thread.Sleep (500);
			_state2 = testThread.IsThreadState;

			// Assert
			Assert.AreEqual(ThreadState.WaitSleepJoin, _state1);
			Assert.AreEqual(ThreadState.Stopped, _state2);
		}

		[Test]
		public void OnStartShouldExecutedThreadAndInEndOnPause ()
		{
			// Arrange

			// Act
			testThread.Start();
			testThread.Pause ();
			_state1 = testThread.IsThreadState;
			testThread.Resume ();
			Thread.Sleep (500);
			_state2 = testThread.IsThreadState;

			// Assert
			Assert.AreEqual(ThreadState.Running, _state1);
			Assert.AreEqual(ThreadState.Stopped, _state2);
		}

	}
}


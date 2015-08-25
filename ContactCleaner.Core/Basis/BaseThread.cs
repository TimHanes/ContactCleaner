using System;
using System.Threading;

namespace ContactCleaner.Core
{
	public class BaseThread {
		private Thread m_thread;
		AutoResetEvent autoEvent;

		public BaseThread() {
			autoEvent = new AutoResetEvent(false);
			m_thread = new Thread( Run );
		}

		public ThreadState IsThreadState {
			get { 
				return m_thread.ThreadState;
			}
		}

		public void Start() {
			m_thread.Start();
		}

		public void Pause() {
			autoEvent.WaitOne();
		}

		public void Resume() {
			autoEvent.Set();
		}

		protected virtual void Run()
		{
//			autoEvent.WaitOne();
			Thread.Sleep (50);
			autoEvent.Set();
		}
	}
}


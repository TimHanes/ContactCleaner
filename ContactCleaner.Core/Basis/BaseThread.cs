using System;
using System.Threading;

namespace ContactCleaner.Core
{
	public class BaseThread {
		
		private Thread m_thread;
		ManualResetEvent autoEvent;

		public BaseThread() {
			autoEvent = new ManualResetEvent(true);
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
			autoEvent.Reset();

		}

		public void Resume() {
			autoEvent.Set();

		}

		protected virtual void Run()
		{
//			this.Pause ();
			autoEvent.WaitOne();
//
//			this.Pause();
		}
	}
}


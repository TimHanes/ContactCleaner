using System;
using System.Threading;

namespace ContactCleaner
{
	public class BaseThread {
		private Thread m_thread;
		AutoResetEvent autoEvent;
		private object mPauseLock = new object();
		private bool mPaused = false;

		public BaseThread() {
			autoEvent = new AutoResetEvent(false);
			m_thread = new Thread( Run );
		}

		public bool IsPaused
		{
			get{ 
				return mPaused;
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

		public void OnPause() 
		{
			lock(mPauseLock)
			{
				mPaused = true;
			}
		}
		//		 * Call this on resume.

		public void OnResume() {
			lock(mPauseLock) {
				mPaused = false;

			}
		}

		protected virtual void Run(){
			lock(mPauseLock) 
			{
				while(mPaused) 
				{
					try 
					{
						this.Pause();
					} 
					catch(System.Exception e) 
					{				
					}
				}
			}
		}
	}
}


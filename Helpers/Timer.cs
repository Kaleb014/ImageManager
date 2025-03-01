using System.Windows.Threading;

namespace ImageManager.Helpers
{
	internal class CustomTimer
	{
		DispatcherTimer _timer;
		int _elapsedTime = 0;
		int _targetTime = 0;
		int _days;
		int _hours;
		int _minutes;
		int _seconds;
		int _milliseconds;
		Action _timerAction = null;

		public Action TimerAction
		{
			get { return _timerAction; }
			set { if (_timerAction != value) _timerAction = value; }
		}

		public DispatcherTimer Timer
		{
			get { return _timer; }
			set { if (_timer != value) _timer = value; }
		}

		public int TargetTime
		{
			get { return _targetTime; }
			set { if (_targetTime != value) _targetTime = value; }
		}

		public int ElapsedTime
		{
			get { return _elapsedTime; }
			set { if (_elapsedTime != value) _elapsedTime = value; }
		}

		public int Days
		{
			get { return _days; }
			set { if (_days != value) _days = value; }
		}

		public int Hours
		{
			get { return _hours; }
			set { if (_hours != value) _hours = value; }
		}

		public int Minutes
		{
			get { return _minutes; }
			set { if (_minutes != value) _minutes = value; }
		}

		public int Seconds
		{
			get { return _seconds; }
			set { if (_seconds != value) _seconds = value; }
		}

		public int Milliseconds
		{
			get { return _milliseconds; }
			set { if (_milliseconds != value) _milliseconds = value; }
		}

		public CustomTimer(int targetTime = 1, int days = 0, int hours = 0,
		int minutes = 0, int seconds = 0, int milliseconds = 0, Action timerAction = null)
		{
			Days = days;
			Hours = hours;
			Minutes = minutes;
			Seconds = seconds;
			Milliseconds = milliseconds;
			TargetTime = targetTime;
			TimerAction = timerAction;

			Timer = new DispatcherTimer();
			Timer.Interval = new TimeSpan(Days, Hours, Minutes, Seconds, Milliseconds);
			Timer.Tick += TimerTick;
			StartTimer();
		}

		private void TimerTick(object sender, EventArgs e)
		{
			ElapsedTime += 1;

			if (ElapsedTime == TargetTime)
			{
				if (TimerAction != null)
					TimerAction();

				Timer.Stop();
			}
		}

		private void StartTimer()
		{
			Timer.Start();
		}
	}
}

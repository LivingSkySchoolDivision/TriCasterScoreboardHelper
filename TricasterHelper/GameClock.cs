using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TricasterHelper
{
    class GameClock
    {
        public bool CountsDown { get; set; }

        private short hours;
        private byte minutes;
        private byte seconds;

        public int Hours
        {
            get { return this.hours; }
            set { this.hours = (short)value; }
        }

        public int Minutes
        {
            get { return this.minutes; }
            set
            {
                // If theres an attempt to add more than 60 minutes, add hours where it would make sense to (using recursion!)
                if (value >= 60)
                {
                    this.Hours += 1;
                    this.Minutes += (value - 60);
                }
                else
                {
                    this.Minutes = value;
                }

                if (this.Minutes >= 60)
                {
                    this.Minutes -= 60;
                    this.Hours += 1;
                }
            }
        }

        public int Seconds
        {
            get { return this.seconds; }
            set
            {
                // If theres an attempt to add more than 60 seconds, add minutes where it would make sense to (using recursion!)
                if (value >= 60)
                {
                    this.Minutes += 1;
                    this.Seconds += (value - 60);
                }
                else
                {
                    this.seconds = (byte)value;
                    if (this.Seconds > 60)
                    {
                        this.Minutes += 1;
                    }
                }

                if (this.Seconds >= 60)
                {
                    this.Seconds -= 60;
                    this.Minutes += 1;
                }

                if (this.Seconds < 0)
                {
                    this.Seconds = 0;
                }

                if ((this.Seconds <= 0) && (this.Minutes <= 0) && (this.Minutes <=0))
                {
                    this.Stop();
                }
            }
        }

        private Timer timer;

        private bool timerRunning;

        private void timerElapsedHandler(object source, ElapsedEventArgs e)
        {
            if (timerRunning)
            {
                if (this.CountsDown)
                {
                    this.Seconds--;
                }
                else
                {
                    this.Seconds++;
                }
            }
        }
        
        public void Set(int hours, int minutes, int seconds)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public void Set(int minutes, int seconds)
        {
            this.Hours = 0;
            this.Minutes = minutes;
            this.Seconds = seconds;

        }

        public void Start()
        {
            this.timer.Start();
            this.timerRunning = true;
        }

        public void Stop()
        {
            this.timer.Stop();
            this.timerRunning = false;
        }

        public bool IsRunning()
        {
            return this.timerRunning;
        }

        public GameClock(int hours, int minutes, int seconds)
        {
            this.CountsDown = true;
            this.timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timerElapsedHandler);
            timer.Enabled = true;
            this.timerRunning = false;
            this.Set(hours, minutes, seconds);
        }

        public GameClock(int minutes, int seconds)
        {
            this.CountsDown = true;
            this.timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timerElapsedHandler);
            timer.Enabled = true;
            this.timerRunning = false;
            this.Set(minutes, seconds);
        }



    }
}

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
        private Timer timer;

        private bool timerRunning;
        public bool CountsDown { get; set; }
        private const int timer_max_seconds = int.MaxValue - 1;
        private const int timer_min_seconds = 0;

        public int TotalSeconds { get; set; }

        public int Hours
        {
            get 
            { 
                // How many hours are there in a huge ammount of seconds
                return (int)((double)this.TotalSeconds / 3600);                
            }
        }

        public int Minutes
        {
            get 
            {
                return (int)((double)(this.TotalSeconds - (this.Hours * 3600)) / 60);
            }
        }

        public int TotalMinutes
        {
            get
            {
                return (int)((double)(this.TotalSeconds) / 60);
            }
        }

        public int Seconds
        {
            get 
            {
                return this.TotalSeconds - (this.Hours * 3600) - (this.Minutes * 60);
            }
        }

        public void AddSeconds(int seconds)
        {
            this.TotalSeconds += seconds;
            if (this.TotalSeconds > timer_max_seconds)
            {
                this.TotalSeconds = timer_max_seconds;
            }

            if (this.TotalSeconds < timer_min_seconds)
            {
                this.TotalSeconds = timer_min_seconds;
            }
        }

        private void timerElapsedHandler(object source, ElapsedEventArgs e)
        {
            if (timerRunning)
            {
                if (this.CountsDown)
                {
                    this.AddSeconds(-1);
                }
                else
                {
                    this.AddSeconds(1);
                }
            }

            timer.Stop();
            timer.Start();
        }
        
        public void Set(int hours, int minutes, int seconds)
        {
            this.TotalSeconds = (hours*60*60) + (minutes*60) + seconds;
        }

        public void Set(int minutes, int seconds)
        {
            this.TotalSeconds = (minutes * 60) + seconds;
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

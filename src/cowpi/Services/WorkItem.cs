using System.Diagnostics;
using System.Timers;

namespace cowpi.Services
{
    public class WorkItem
    {
        private Timer timer;
        private Stopwatch stopwatch = new Stopwatch();
        private StateEnum state = StateEnum.Waiting;

        public string Skill { get; set; }
        public decimal Minutes { get; set; }

        public StateEnum State { get { return state; } }

        public decimal Completed {  get
            {
                return (decimal)stopwatch.Elapsed.TotalMinutes;
            }
        }
        internal void Start()
        {
            timer = new Timer((double)Minutes * 60.0 * 1000.0);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            stopwatch.Start();
            this.state = StateEnum.Active;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.stopwatch.Stop();
            this.state = StateEnum.Completed;
            timer.Elapsed -= Timer_Elapsed;
            timer.Dispose();
            timer = null;
        }
    }

}

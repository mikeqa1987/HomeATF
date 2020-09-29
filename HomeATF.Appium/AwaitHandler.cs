using HomeATF.Appium.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HomeATF.Appium
{
    public class AwaitHandler : IAwaitHandler
    {
        private readonly ISettings settings;
        private readonly ILogger logger;
        public AwaitHandler(ISettings settings, ILogger logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public int DefaultTimeDelay => this.settings.DefaultTimeDelay;

        public void WaitFor(TimeSpan time)
        {
            this.logger.Trace("{waiter}: Waiting for max waiting time {time}", nameof(AwaitHandler), time);
            Thread.Sleep(time);
        }

        public bool WaitFor(Func<bool> function, TimeSpan maxTime)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.Add(maxTime);
            bool result = false;

            this.logger.Trace("{waiter}: Waiting for function execution with max waiting time {time}", nameof(AwaitHandler), maxTime);

            while (!result && DateTime.Now < endTime)
            {
                result = function();
                Thread.Sleep(300);
            }

            return result;
        }

        public void WaitForDefaultDelay()
        {
            this.WaitFor(TimeSpan.FromSeconds(this.DefaultTimeDelay));
        }
    }
}

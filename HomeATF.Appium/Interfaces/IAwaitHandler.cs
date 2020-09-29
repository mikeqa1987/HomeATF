using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium.Interfaces
{
    public interface IAwaitHandler
    {
        void WaitFor(TimeSpan time);

        bool WaitFor(Func<bool> function, TimeSpan maxTime);

        void WaitForDefaultDelay();

    }
}

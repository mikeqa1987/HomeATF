using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium.Interfaces
{
    public interface ITestContext
    {
        ISettings Settings { get; }

        ILogger Logger { get; }

        IAwaitHandler AwaitingService { get; }

        bool StartSession();

        bool EndSession();

        void UploadFile(string fileName);

        WindowsRootWindow GetRoot(string title);
    }
}

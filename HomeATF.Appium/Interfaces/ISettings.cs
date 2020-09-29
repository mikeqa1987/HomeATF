using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium.Interfaces
{
    public interface ISettings
    {
        string CurrentTestName { get; }

        string ApplicationPath { get; }

        string ApplicationTitle { get; }

        string ServerUrl { get; }

        string ServerName { get; }

        string PlatformName { get; }

        string LogFilePath { get; }

        string ScreenshotsFolder { get; }

        int TryAttemptsFindOrClose { get; }

        int DefaultTimeDelay { get;  }

        int NewCommandTimeout { get; }
    }
}

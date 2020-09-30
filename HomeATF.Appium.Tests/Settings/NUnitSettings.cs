using System;
using System.Collections.Generic;
using System.Linq;
using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GenericsLearn.Tests.Settings
{
    public class NUnitSettings : ISettings
    {
        private readonly NUnit.Framework.TestParameters testParameters;

        public NUnitSettings(NUnit.Framework.TestParameters testParameters)
        {
            this.testParameters = testParameters;
        }

        public string CurrentTestName => TestContext.CurrentContext.Test.Name;

        public string ApplicationPath => this.testParameters[nameof(this.ApplicationPath)];

        public string ApplicationTitle => this.testParameters[nameof(this.ApplicationTitle)];

        public string ServerUrl => this.testParameters[nameof(this.ServerUrl)];

        public string ServerName => this.testParameters[nameof(this.ServerName)];

        public string PlatformName => this.testParameters[nameof(this.PlatformName)];

        public string LogFilePath => this.testParameters[nameof(this.LogFilePath)];

        public string ScreenshotsFolder => this.testParameters[nameof(this.ScreenshotsFolder)];

        public int TryAttemptsFindOrClose => Convert.ToInt32(this.testParameters[nameof(this.TryAttemptsFindOrClose)]);

        public int DefaultTimeDelay => Convert.ToInt32(this.testParameters[nameof(this.DefaultTimeDelay)]);

        public int NewCommandTimeout => Convert.ToInt32(this.testParameters[nameof(this.NewCommandTimeout)]);
    }
}

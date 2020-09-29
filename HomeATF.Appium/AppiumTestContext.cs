using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Diagnostics;
using HomeATF.Appium.Interfaces;

namespace HomeATF.Appium
{
    public class AppiumTestContext : ITestContext
    {
        private readonly AppiumSessionHandler sessionHandler;
        private readonly Navigator navigator;

        public AppiumTestContext(
            AppiumSessionHandler appiumSessionHandler, 
            Navigator navService,
            ILogger logger,
            ISettings settings)
        {
            this.sessionHandler = appiumSessionHandler;
            this.navigator = navService;
            this.Logger = logger;
            this.Settings = settings;
        }

        public ISettings Settings { get; }

        public ILogger Logger { get; }

        public IAwaitHandler AwaitingService => this.navigator.AwaitService;

        public bool StartSession()
        {
            this.Logger.Trace($"{nameof(AppiumTestContext)} -- Connecting to remote Appium server ...");

            var appStarted = this.sessionHandler.StartSession();

            this.Logger.Trace($"{nameof(AppiumTestContext)} -- Are we connected: {appStarted}");

            var applicationLaunched = navigator.LaunchApplication(this.Settings.ApplicationPath, this.Settings.ApplicationTitle);
            return appStarted && applicationLaunched;
        }

        public WindowsRootWindow GetRoot(string title)
        {
            return this.navigator.GetRoot(title);
        }

        public bool EndSession()
        {
            this.navigator.CloseApplication(this.Settings.ApplicationTitle);
            this.sessionHandler.EndSession();
            return true;
        }

        public void UploadFile(string fileName)
        {
            this.sessionHandler.CurrentSession.PushFile(fileName, "Trololo testing push file " + DateTime.Now.ToString());
        }
    }
}

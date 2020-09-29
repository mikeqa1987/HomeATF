using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using HomeATF.Appium.Interfaces;
using NLog;

namespace HomeATF.Appium
{
    public class AppiumSessionHandler
    {
        private const string AppCapability = "app";
        private const string DeviceNameCapability = "deviceName";
        private const string PlatformNameCapability = "platformName";
        private const string NewCommandTimeoutCapability = "NewCommandTimeout";

        private readonly ILogger logger;
        private readonly ISettings settings;
        private static AppiumSessionHandler sessionHandlerSingle;

        public WindowsDriver<WindowsElement> CurrentSession { get; private set; }

        private AppiumSessionHandler(ISettings settings, ILogger logger)
        {
            this.logger = logger;
            this.settings = settings;
        }

        public static AppiumSessionHandler GetInstance(ISettings settings, ILogger logger)
        {
            if (sessionHandlerSingle != null)
                return sessionHandlerSingle;

            return sessionHandlerSingle = new AppiumSessionHandler(settings, logger);
        }

        public bool StartSession()
        {
            var url = this.settings.ServerUrl;
            var deviceName = this.settings.ServerName;
            var platformName = this.settings.PlatformName;
            var desktopLaunched = true;

            WindowsDriver<WindowsElement> appDriver = this.StartAppSession(url, deviceName, platformName, "Root");
            this.CurrentSession = appDriver;
            desktopLaunched = appDriver != null;

            return desktopLaunched;
        }

        private WindowsDriver<WindowsElement> StartAppSession(string url, string deviceName, string platformName, string path)
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability(AppCapability, path);
            options.AddAdditionalCapability(DeviceNameCapability, deviceName);
            options.AddAdditionalCapability(PlatformNameCapability, platformName);
            options.AddAdditionalCapability(NewCommandTimeoutCapability, this.settings.NewCommandTimeout);

            string resultUrl = $"{url}/wd/hub";

            this.logger.Info($"{nameof(AppiumSessionHandler)}.{nameof(StartAppSession)} Connecting to URL: {resultUrl}");

            var appDriverSession = new WindowsDriver<WindowsElement>(new Uri(resultUrl), options, TimeSpan.FromSeconds(60));

            return appDriverSession;
        }

        public bool EndSession()
        {
            this.logger.Info("{shandler}: Closing connection to remote server {server}", nameof(AppiumSessionHandler), this.settings.ServerUrl);
            this.CurrentSession.CloseApp();
            this.CurrentSession.Quit();

            return true;
        }
    }
}

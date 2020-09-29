using HomeATF.Appium.Interfaces;
using NLog;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    internal class AppiumWrapperContext : IUiWrapperContext
    {
        internal AppiumWrapperContext(
            string windowName,
            WindowsDriver<WindowsElement> driver,
            ILogger logger,
            ISettings settings,
            AppiumUiWrapperFactory wrapperFactory)
        {
            this.WindowName = windowName;
            this.Settings = settings;
            this.Logger = logger;
            this.Driver = driver;
            this.WrapperFactory = wrapperFactory;
        }

        public string WindowName { get; }

        public ILogger Logger { get; }

        public ISettings Settings { get; }

        public AppiumUiWrapperFactory WrapperFactory { get; }

        public WindowsDriver<WindowsElement> Driver { get; }
    }
}

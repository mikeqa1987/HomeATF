using Castle.Core.Internal;
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
    public class AppiumUiWrapperFactory
    {
        private const string DesktopClassName = "#32769";
        public AppiumUiElementWrapper CreateDesktopWrapper(
            WindowsElement wElement,
            WindowsDriver<WindowsElement> appDriver,
            ISettings settings,
            ILogger logger)
        {
            return this.CreateWrapper(wElement, appDriver, DesktopClassName, settings, logger);
        }

        public AppiumUiElementWrapper CreateWrapper(
            WindowsElement wElement,
            WindowsDriver<WindowsElement> appDriver,
            string windowName,
            ISettings settings,
            ILogger logger)
        {
            if (wElement == null)
                throw new ArgumentNullException(nameof(wElement));
            if (appDriver == null)
                throw new ArgumentNullException(nameof(appDriver));

            var wContext = new AppiumWrapperContext(windowName, appDriver, logger, settings, this);

            return new AppiumUiElementWrapper(wElement, wContext);        
        }

        public AppiumUiElementWrapper CreateWrapper(WindowsElement wElement, AppiumUiElementWrapper root, string windowName = default)
        {
            if (wElement == null)
                throw new ArgumentNullException(nameof(wElement));
            if (root == null)
                throw new ArgumentNullException(nameof(wElement));

            var wName = windowName.IsNullOrEmpty() ? root.WrapperContext.WindowName : windowName;
            var appDriver = root.WrapperContext.Driver;
            var logger = root.WrapperContext.Logger;
            var settings = root.WrapperContext.Settings;

            return new AppiumUiElementWrapper(wElement, new AppiumWrapperContext(wName, appDriver, logger, settings, this));

        }

    }
}

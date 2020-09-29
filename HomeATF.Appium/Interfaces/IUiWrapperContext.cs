using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using OpenQA.Selenium.Appium.Windows;

namespace HomeATF.Appium.Interfaces
{
    public interface IUiWrapperContext
    {
        string WindowName { get; }

        ILogger Logger { get; }

        ISettings Settings { get; }

        AppiumUiWrapperFactory WrapperFactory { get; }

        WindowsDriver<WindowsElement> Driver { get; }
    }
}

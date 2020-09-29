using NLog;
using HomeATF.Appium.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace HomeATF.Appium
{
    public class Navigator
    {
        private AppiumSessionHandler sessionHandler;
        private AppiumUiWrapperFactory wFactory;
        private IElementFactory elementFactory;
        private ISettings settings;
        private ILogger logger;
        private IAwaitHandler waitHandler;
        private const string DesktopClassName = "#32769";

        public Navigator(AppiumSessionHandler sessionHandler, AppiumUiWrapperFactory factory, ISettings settings, ILogger logger, IElementFactory eFactory)
        {
            this.sessionHandler = sessionHandler;
            this.wFactory = factory;
            this.settings = settings;
            this.logger = logger;
            this.elementFactory = eFactory;
        }

        public IAwaitHandler AwaitService
        {
            get
            {
                if (this.waitHandler != null)
                {
                    return this.waitHandler;
                }

                return this.waitHandler = new AwaitHandler(this.settings, this.logger);
            }
        }

        public bool LaunchApplication(string appPath, string appTitle)
        {
            var desktop = this.GetDektopRoot();

            desktop.PressKeysCombination(Keys.Windows, 'r');

            IUiElementWrapper runner = null;
            int attempts = 0;

            while (runner == null && attempts < 15)
            {
                runner = this.GetApplicationRoot("Run");
            }

            runner.SendKeys(appPath);
            runner.PressKey(Keys.Enter);

            IUiElementWrapper result = null;

            while (result == null && attempts < 15)
            {
                result = this.GetApplicationRoot(appTitle);
            }

            return result != null;
        }

        public bool CloseApplication(string appTitle)
        {
            var desktop = this.GetDektopRoot();

            desktop.PressKeysCombination(Keys.Windows, 'r');

            IUiElementWrapper runner = null;
            int attempts = 0;

            while (runner == null && attempts < this.settings.TryAttemptsFindOrClose)
            {
                runner = this.GetApplicationRoot("Run");
            }

            runner.SendKeys($"taskkill /IM notepad.exe /F");
            runner.PressKey(Keys.Enter);

            return true;
        }

        private IUiElementWrapper GetDektopRoot()
        {
            return this.GetApplicationRoot(DesktopClassName);
        }

        public WindowsRootWindow GetRoot(string title)
        {
            return new WindowsRootWindow(this.GetApplicationRoot(title), this, this.logger, this.elementFactory);
        }

        private IUiElementWrapper GetApplicationRoot(string title)
        {
            return this.GetApplicationRoot(title, 5);
        }

        private IUiElementWrapper GetApplicationRoot(string title, int reryAttempts = 5)
        {
            if (title == DesktopClassName)
            {
                WindowsElement root = this.sessionHandler.CurrentSession.FindElementByClassName(title);
                var desktopWrapper = this.wFactory.CreateDesktopWrapper(root,
                    this.sessionHandler.CurrentSession,
                    this.settings,
                    this.logger);

                return desktopWrapper;
            }
            else
            {
                this.logger.Trace("{navigator} Starting search window root with title: {title} ", nameof(Navigator), nameof(Navigator), title);
                var timer = Stopwatch.StartNew();

                WindowsElement element = null;
                int attemts = 0;

                while (element == null && attemts < reryAttempts)
                {
                    try
                    {
                        element = this.sessionHandler.CurrentSession.FindElementByName(title);
                    }
                    catch
                    { }
                }

                var appElementWrapper = this.wFactory.CreateDesktopWrapper(element,
                    this.sessionHandler.CurrentSession,
                    this.settings,
                    this.logger);
                this.logger.Trace("{navigator} Finished search window root with title: {title} Time Elapsed: {time} ms", nameof(Navigator), title, timer.ElapsedMilliseconds);

                return appElementWrapper;
            }


        }

        public IEnumerable<IUiElementWrapper> FindAllElements(Element rootElement, IEnumerable<(string key, object value)> searchConditions)
        {
            var el = rootElement.UiInstance as AppiumUiElementWrapper;
            var timer = Stopwatch.StartNew();

            var result = el.FindAll(searchConditions);

            timer.Stop();

            this.logger.Trace("{navigator} Finished search elements withs conditions: {conditions} Time Elapsed: {time} ms", nameof(Navigator), searchConditions, timer.ElapsedMilliseconds);

            return result;
        }

        public IUiElementWrapper FindElement(Element rootElement, IEnumerable<(string key, object value)> searchConditions)
        {
            var el = rootElement.UiInstance as AppiumUiElementWrapper;
            var timer = Stopwatch.StartNew();

            var result = el.FindFirst(searchConditions);

            timer.Stop();

            this.logger.Trace("{navigator} Finished search first element with conditions: {conditions} Time Elapsed: {time} ms", nameof(Navigator), searchConditions, timer.ElapsedMilliseconds);

            return result;
        }

        public IEnumerable<IUiElementWrapper> FindAllByCustomCondition(Element rootElement, string key, object value)
        {
            var element = rootElement.UiInstance as AppiumUiElementWrapper;

            return element.FindAllByCustomCondition(key, value);
        }
    }
}

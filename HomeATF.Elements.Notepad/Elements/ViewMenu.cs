using NLog;
using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using System;

namespace HomeATF.Elements.Notepad
{
    public class ViewMenu : Element
    {
        protected IUiElementWrapper uiWrapper;
        protected IAwaitHandler awaitHandler;
        protected ILogger logger;

        public ViewMenu(
            IUiElementWrapper wrapper,
            Navigator navigator,
            IElementFactory eFactory,
            ILogger logger) : base(wrapper, navigator, logger, eFactory)
        {
            this.uiWrapper = wrapper;
            this.logger = logger;
            this.awaitHandler = navigator.AwaitService;
        }

        public bool EnableStatusBar()
        {
            this.logger.Trace($"{nameof(ViewMenu)}: Enable Status Bar");

            this.Click();

            var statusBar = this.FindElement<Element>((By.AutomationIdProperty, "27"));

            statusBar.Click();
            return true;
        }

    }
}


using NLog;
using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using System;

namespace HomeATF.Elements.Notepad
{
    public class FileMenu : Element
    {
        protected IUiElementWrapper uiWrapper;
        protected IAwaitHandler awaitHandler;
        protected ILogger logger;

        public FileMenu(
            IUiElementWrapper wrapper,
            Navigator navigator,
            IElementFactory eFactory,
            ILogger logger) : base(wrapper, navigator, logger, eFactory)
        {
            this.uiWrapper = wrapper;
            this.logger = logger;
            this.awaitHandler = navigator.AwaitService;
        }

        public void ClickSave()
        {
            this.Click();
            this.FindElement<Element>((By.LocalizedControlTypeProperty, "menu item"), (By.AutomationIdProperty, "3")).Click();
            awaitHandler.WaitFor(TimeSpan.FromSeconds(2));
        }
        public void ClickOpen()
        {
            this.Click();
            var openMenuItem = this.FindElement<Element>((By.LocalizedControlTypeProperty, "menu item"), (By.AutomationIdProperty, "2"));

            this.awaitHandler.WaitFor(() => { return openMenuItem.UiInstance.IsVisible; }, TimeSpan.FromSeconds(30));

            openMenuItem.Click();
        }

    }
}

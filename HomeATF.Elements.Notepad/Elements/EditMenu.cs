using NLog;
using HomeATF.Appium;
using HomeATF.Appium.Interfaces;

namespace HomeATF.Elements.Notepad.Elements
{
    public class EditMenu : Element
    {
        protected IUiElementWrapper uiWrapper;
        protected ILogger logger;

        public EditMenu(
            IUiElementWrapper wrapper,
            Navigator navigator,
            IElementFactory eFactory,
            ILogger logger) : base(wrapper, navigator, logger, eFactory)
        {
            this.uiWrapper = wrapper;
            this.logger = logger;
        }

        public void Do()
        {

        }
    }
}

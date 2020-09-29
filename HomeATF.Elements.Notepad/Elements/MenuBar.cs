using NLog;
using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Elements.Notepad
{
    public class MenuBar : Element
    {
        protected IUiElementWrapper uiWrapper;
        protected ILogger logger;

        public MenuBar(
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

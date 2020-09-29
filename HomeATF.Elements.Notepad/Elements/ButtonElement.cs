using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using NLog;

namespace HomeATF.Elements.Notepad
{
    public class ButtonElement : Element
    {
        private IUiElementWrapper wrapper;
        private ILogger logger;
        public ButtonElement(IUiElementWrapper uiWrapper, Navigator navigator, IElementFactory eFactory, ILogger logger) 
            : base(uiWrapper, navigator, logger, eFactory)
        {
            this.logger = logger;
            this.wrapper = uiWrapper;
        }
    }
}

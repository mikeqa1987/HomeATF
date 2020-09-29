using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Elements.Notepad
{
    public class TextEditor : Element
    {
        protected IUiElementWrapper uiWrapper;
        protected ILogger logger;

        public TextEditor(
            IUiElementWrapper wrapper,
            Navigator navigator,
            IElementFactory eFactory,
            ILogger logger) : base(wrapper, navigator, logger, eFactory)
        {
            this.uiWrapper = wrapper;
            this.logger = logger;
        }

        public string CurrentText
        {
            get 
            {
               return this.UiInstance.GetPropertyValue("Value.Value");
            }
        }

        public void TypeText(string text)
        {
            this.SendKeys(text);
        }
    }
}

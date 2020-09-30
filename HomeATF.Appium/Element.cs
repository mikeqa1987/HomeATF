using HomeATF.Appium.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    public class Element
    {
        private readonly Navigator navigator;

        private readonly ILogger logger;

        private readonly IElementFactory elementFactory;

        public Element(IUiElementWrapper uiInstance, Navigator navigator, ILogger logger, IElementFactory eFactory)
        {
            this.UiInstance = uiInstance ?? throw new ArgumentNullException(nameof(IUiElementWrapper));
            this.navigator = navigator ?? throw new ArgumentNullException(nameof(Navigator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(ILogger));
            this.elementFactory = eFactory;
        }
        public IUiElementWrapper UiInstance { get; }

        public IDimensions Dimensions => this.UiInstance.Dimensions;

        public bool IsEnabled 
        { 
            get 
            {
                this.WaitService.WaitForDefaultDelay();
                return this.UiInstance.IsEnabled;
            }
        }


        public IAwaitHandler WaitService
        {
            get
            { 
                return this.navigator.AwaitService;
            }
        }

        public string Name => this.UiInstance.Name;

        public string RuntimeId => this.UiInstance.RuntimeId;

        public IEnumerable<T> FindAllElements<T>(params (string key, object value)[] searchConditions) where T : Element
        {
            var conditions = searchConditions?.ToList();

            var wrapperItems = this.navigator.FindAllElements(this, conditions);
            var result = new List<T>();

            result = wrapperItems?.Select(w =>
            {
                return this.CreateElementFromParent<T>(w);

            }).ToList();

            return result;
        }

        public T FindElement<T>(params (string key, object value)[] searchConditions) where T : Element
        {

            var conditions = new List<(string key, object value)>();
            var defaultConditions = this.elementFactory.DefaultConditionsToSearchElement(typeof(T));
            conditions.AddRange(defaultConditions);

            if (searchConditions != null && searchConditions.Any())
            {
                conditions.AddRange(searchConditions.ToList());
            }
                
            var wrapper = this.navigator.FindElement(this, conditions);

            return this.CreateElementFromParent<T>(wrapper);
        }

        public IEnumerable<T> FindElementsByCustomCondition<T>(string key, object value) where T : Element
        {
            var wrappersCollection = this.navigator.FindAllByCustomCondition(this, key, value);

            return wrappersCollection?.Select(e => { return this.CreateElementFromParent<T>(e); });
        }

        private T CreateElementFromParent<T>(IUiElementWrapper wrapper) where T : Element
        {
            Type t = typeof(T);

            if (t == typeof(Element))
            {
                return new Element(wrapper, this.navigator, this.logger, this.elementFactory) as T;
            }

            else
            {
                return this.elementFactory.CreateElement<T>(wrapper, this.navigator, this.elementFactory, this.logger);
            }
        }

        public void Click()
        {
            this.WaitForEnabled();
            this.UiInstance.Click();
        }

        public void Click(int xOffset, int yOffset)
        {
            this.WaitForEnabled();
            this.UiInstance.Click(MouseButton.Left, xOffset, yOffset);
        }

        public void PressKeysCombination(Keys key, char c)
        {
            this.UiInstance.PressKeysCombination(key, c);
        }

        public void SendKeys(string symbols)
        {
            this.UiInstance.SendKeys(symbols);
        }

        public void PressKey(Keys key)
        {
            this.WaitForEnabled();
            this.UiInstance.PressKey(key);
        }

        public void WaitForEnabled() => this.WaitService.WaitFor(() => this.IsEnabled, TimeSpan.FromMilliseconds(2000));

        public bool TakeScreenshot(string filename)
        {
            this.WaitForEnabled();

            string resultFilename = filename + DateTime.Now.ToString("_ddMMyyyy_hhmmss") + ".png";

            return this.UiInstance.TakeScreenshot(resultFilename);
        }


    }
}

using Castle.Core.Internal;
using HomeATF.Appium.Interfaces;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    public class AppiumUiElementWrapper : IUiElementWrapper
    {
        private readonly WindowsElement windowsElement;
        private readonly int attemptsToSearch;
        private string nameProperty;
        public static readonly string NameProperty = By.NameProperty;
        public static readonly string RuntimeIdProperty = By.RuntimeIdProperty;

        internal IUiWrapperContext WrapperContext { get; }

        public string Name
        {
            get
            {
                if (this.nameProperty.IsNullOrEmpty())
                {
                    return nameProperty = this.GetPropertyValue(NameProperty);
                }
                return nameProperty;
            }
        }

        public string RuntimeId => this.GetPropertyValue(RuntimeIdProperty);

        public IDimensions Dimensions
        {
            get
            {
                var elementRect = this.windowsElement.Rect;
                int width = elementRect.Width;
                int height = elementRect.Height;

                return new Dimensions(width, height);
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.GetPropertyValue("BoundingRectangle") != null;
            }
        }

        private WindowsDriver<WindowsElement> Driver => this.WrapperContext.Driver;

        private ILogger Logger => this.WrapperContext.Logger;

        private ISettings Settings => this.WrapperContext.Settings;

        public AppiumUiElementWrapper(WindowsElement wElement, IUiWrapperContext wrapperContext)
        {
            this.windowsElement = wElement;
            this.WrapperContext = wrapperContext;
            this.attemptsToSearch = this.Settings.TryAttemptsFindOrClose;
        }

        public bool IsEnabled
        {
            get
            {
                var isEnabled = this.GetPropertyValue("IsEnabled");
                return bool.Parse(isEnabled);
            }
        }

        public IUiElementWrapper FindFirst(IEnumerable<(string, object)> searchConditions)
        {
            this.Logger.Trace("{emitter} Starting search first element with conditions: {conditions}", nameof(AppiumUiElementWrapper), searchConditions);

            IWebElement element = null;
            int attempts = 0;
            var timer = Stopwatch.StartNew();

            while (element == null && attempts < this.attemptsToSearch)
            {
                attempts++;
                element = this.FindFirstInternal(searchConditions);

            }

            if (element == null)
            {
                this.Logger.Trace("Couldn't find any elements with search conditions: {conditions} and attempts count {attempts}", searchConditions, attemptsToSearch);
                return null;
            }

            timer.Stop();
            this.Logger.Trace($"Completed search. Elapsed time: {timer.ElapsedMilliseconds} ms");

            return this.WrapperContext.WrapperFactory.CreateWrapper((WindowsElement)element, this);
        }

        private AppiumWebElement FindFirstInternal(IEnumerable<(string, object)> searchConditions)
        {
            AppiumWebElement firstFound = null;

            if (searchConditions.HasOnlyOne())
            {
                var (key, value) = searchConditions.First();
                firstFound = this.FindFirstBySingleCondition(key, value);
                return firstFound;
            }
            else if (searchConditions == null && !searchConditions.Any())
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElement(OpenQA.Selenium.By.XPath(".//"));
                }
                else
                {
                    return this.Driver.FindElement(OpenQA.Selenium.By.XPath("//*"));
                }
            }
            else
            {
                firstFound = (AppiumWebElement)this.FindAllInternal(searchConditions).FirstOrDefault();
            }

            return firstFound;
        }
        private AppiumWebElement FindFirstBySingleCondition(string key, object value)
        {
            if (key == By.NameProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElementByName((string)value);
                }
                
                return this.Driver.FindElementByName((string)value);
            }

            if (key == By.ClassNameProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElementByClassName((string)value);
                }

                return this.Driver.FindElementByClassName((string)value);
            }

            if (key == By.LocalizedControlTypeProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElement(OpenQA.Selenium.By.XPath($@".//*[@LocalizedControlType='{value}']"));
                }

                return this.Driver.FindElement(OpenQA.Selenium.By.XPath($@".//*[@LocalizedControlType='{value}']"));
            }

            if (key == By.IsEnabledProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElement(OpenQA.Selenium.By.XPath($@".//*[@IsEnabled='{value}']"));
                }

                return this.Driver.FindElement(OpenQA.Selenium.By.XPath($@".//*[@IsEnabled='{value}']"));
            }

            if (key == By.AutomationIdProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElementByAccessibilityId((string)value);
                }

                return this.Driver.FindElementByAccessibilityId((string)value);
            }

            return null;
        }
        public IEnumerable<IUiElementWrapper> FindAll(IEnumerable<(string, object)> searchConditions)
        {
            this.Logger.Trace("{wrapper}: Starting search elements with conditions: {searchConditions}", nameof(AppiumUiElementWrapper), searchConditions);

            IEnumerable<IWebElement> elements = null;
            int attempts = 0;
            var timer = Stopwatch.StartNew();

            while (elements == null && attempts < 10)
            {
                attempts++;
                elements = this.FindAllInternal(searchConditions);

                if (elements == null || !elements.Any())
                {
                    this.Logger.Trace($"Couldn't find any elements with search conditions: {searchConditions} and attempts count 10");
                }
            }

            timer.Stop();
            this.Logger.Trace($"Completed search. Elapsed time: {timer.ElapsedMilliseconds} ms");
            return elements.Select(e =>
            {
                return this.WrapperContext.WrapperFactory.CreateWrapper((WindowsElement)e, this);
            }
            );

        }

        private IEnumerable<IWebElement> FindAllInternal(IEnumerable<(string, object)> searchConditions)
        {
            var foundItems = new List<IWebElement>();

            if (searchConditions == null || !searchConditions.Any())
            {
                if (this.windowsElement != null)
                {
                    this.Logger.Trace($"{nameof(AppiumUiElementWrapper)}: Start searching ALL elements with no conditions");
                    Stopwatch timer = Stopwatch.StartNew();

                    var elements = this.windowsElement.FindElements(OpenQA.Selenium.By.XPath("//node()"));

                    timer.Stop();

                    if (elements.Any())
                    {
                        foundItems.AddRange(elements);
                        this.Logger.Trace($"{nameof(AppiumUiElementWrapper)}: Completed searching ALL elements with no conditions in {timer.ElapsedMilliseconds} ms");
                    }
                    else
                    {
                        this.Logger.Trace($"{nameof(AppiumUiElementWrapper)}: Couldn't find any element with no conditions in {timer.ElapsedMilliseconds} ms");
                    }

                }
                else 
                {
                    this.Logger.Trace($"{nameof(AppiumUiElementWrapper)}: Start searching element from ROOT with no conditions");

                    this.Driver.FindElements(OpenQA.Selenium.By.XPath("//*"));
                }
            }

            else
            {
                foreach (var (key, value) in searchConditions)
                {
                    var elements = this.FindAllBySingleCondition(key, value);
                    if (!foundItems.Any())
                    {
                        foundItems.AddRange(elements);
                    }
                    else
                    {
                        foundItems = foundItems.Intersect(elements).ToList();
                        if (!foundItems.Any())
                        {
                            return null;
                        }
                    }
                }
            }

            return foundItems;
        }
        private IEnumerable<IWebElement> FindAllByCustomConditionInternal(string key, object value)
        {
            if (key.IsNullOrEmpty() || value == null)
            {
                return null;
            }
            string searchCondition = $@".//*[@{key}='{value}']";

            return this.windowsElement.FindElementsByXPath(searchCondition);
        }

        public IEnumerable<IUiElementWrapper> FindAllByCustomCondition(string key, object value)
        {
            this.Logger.Trace("{wrapper}: Starting search elements with custom conditions: {key}{value}", nameof(AppiumUiElementWrapper), key, (string)value);

            List<IWebElement> foundElements = null;
            var timer = Stopwatch.StartNew();
            int attempts = 0;

            while (foundElements == null && attempts < this.Settings.TryAttemptsFindOrClose)
            {
                try
                {
                    foundElements = this.FindAllByCustomConditionInternal(key, value).ToList();
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    this.Logger.Trace("{wrapper}: Error with search elements : " + e.Message, nameof(AppiumUiElementWrapper));
                    return null;
                }
            }

            if (foundElements == null || !foundElements.Any())
            {
                this.Logger.Trace("{wrapper}: Couldn't find elements by custom condition after {attempts} attempts ", nameof(AppiumUiElementWrapper), this.Settings.TryAttemptsFindOrClose);
            }

            timer.Stop();
            this.Logger.Trace("{wrapper}: Completed search in {time} ms", nameof(AppiumUiElementWrapper), timer.ElapsedMilliseconds);

            return foundElements.Select(e =>
            {
                return this.WrapperContext.WrapperFactory.CreateWrapper((WindowsElement)e, this);
            });
        }

        private IEnumerable<IWebElement> FindAllBySingleCondition(string key, object value)
        {
            if (key == null || value == null)
            {
                return null;
            }

            if (key == By.NameProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElementsByName((string)value);
                }

                return this.Driver.FindElementsByName((string)value);
            }

            if (key == By.ClassNameProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElementsByClassName((string)value);
                }

                return this.Driver.FindElementsByClassName((string)value);
            }

            if (key == By.LocalizedControlTypeProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElements(OpenQA.Selenium.By.XPath($@".//*[@LocalizedControlType='{value}']"));
                }

                return this.Driver.FindElements(OpenQA.Selenium.By.XPath($@".//*[@LocalizedControlType='{value}']"));
            }

            if (key == By.IsEnabledProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElements(OpenQA.Selenium.By.XPath($@".//*[@IsEnabled='{value}']"));
                }

                return this.Driver.FindElements(OpenQA.Selenium.By.XPath($@".//*[@IsEnabled='{value}']"));
            }

            if (key == By.AutomationIdProperty)
            {
                if (this.windowsElement != null)
                {
                    return this.windowsElement.FindElementsByAccessibilityId((string)value);
                }

                return this.Driver.FindElementsByAccessibilityId((string)value);
            }

            return null;
        }

        private string GetPropertyValue(WindowsElement wElement, string propertyName)
        {
            if (wElement != null)
            {
                string result = wElement.GetAttribute(propertyName);
                return result;
            }

            return null;
        }

        public string GetPropertyValue(string propertyName)
        {
            this.Logger.Trace("{wrapper}: Get property {property} value of the element: ", nameof(AppiumUiElementWrapper), propertyName);

            return this.GetPropertyValue(this.windowsElement, propertyName);
        }

        public void PressKeysCombination(Keys modifier, char symbol)
        {
            this.windowsElement.SendKeys(KeyMap.GetSeleniumKey(modifier) + symbol.ToString() + KeyMap.GetSeleniumKey(modifier));
        }

        public void PressKeysCombination(Keys[] keys)
        {
            string keysToSend = string.Empty;

            foreach (var key in keys)
            {
                keysToSend += KeyMap.GetSeleniumKey(key);
            }
            this.Logger.Trace("{wrapper}: Send combination of keys to the element: {name}", nameof(AppiumUiElementWrapper), keysToSend, this.Name);

            this.windowsElement.SendKeys(keysToSend);

        }

        public void SendKeys(string keys)
        {
            this.Logger.Trace("{wrapper}: Send keys {keys} to the element: {name}", nameof(AppiumUiElementWrapper), keys, this.Name);

            this.windowsElement.SendKeys(keys);
        }

        public void PressKey(Keys key)
        {
            this.Logger.Trace("{wrapper}: Press key {key} to the element: {name}", nameof(AppiumUiElementWrapper), key, this.Name);

            this.windowsElement.SendKeys(KeyMap.GetSeleniumKey(key));
        }

        public void Click()
        {
            this.Logger.Trace("{wrapper}: Click on the element: {name}", nameof(AppiumUiElementWrapper), this.Name);
            
            this.windowsElement?.Click();
        }

        public void MouseHover()
        {
            Actions a = new Actions(this.WrapperContext.Driver);
            a.MoveToElement(this.windowsElement);

            a.Perform();
        }

        public void MouseMove(int x, int y)
        {
            Actions a = new Actions(this.WrapperContext.Driver);
            a.MoveByOffset(x, y);

            a.Perform();
        }

        public Bitmap TakeScreenshot()
        {
            this.Logger.Trace("{wrapper}: Take screenshot of the element: {name}", nameof(AppiumUiElementWrapper), this.Name);

            Screenshot screen = this.windowsElement?.GetScreenshot();
            byte[] imageArray = screen.AsByteArray;
            return new Bitmap(new System.IO.MemoryStream(imageArray));
        }

        public bool TakeScreenshot(string fileName)
        {
            var image = this.TakeScreenshot();

            string resultPath = this.Settings.ScreenshotsFolder + fileName;

            this.Logger.Trace("{wrapper}: Saving screenshot of the element to file: {name}", nameof(AppiumUiElementWrapper), resultPath);

            image.Save(resultPath, ImageFormat.Png);

            image.Dispose();

            return File.Exists(resultPath);
        }
    }
}

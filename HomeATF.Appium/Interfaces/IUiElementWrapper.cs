using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium.Interfaces
{
    public interface IUiElementWrapper
    {
        
        string Name { get; }

        string RuntimeId { get; }

        bool IsVisible { get; }

        bool IsEnabled { get; }

        void PressKeysCombination(Keys modifier, char symbol);

        void PressKeysCombination(Keys[] keys);

        void SendKeys(string keys);

        void Click();

        void PressKey(Keys key);

        void MouseHover();

        void MouseMove(int x, int y);

        bool TakeScreenshot(string file);

        string GetPropertyValue(string propertyName);

        IDimensions Dimensions { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    public static class KeyMap
    {
        public static Dictionary<Keys, string> AppKeyMap = new Dictionary<Keys, string>
        {
            { Keys.Windows, OpenQA.Selenium.Keys.Meta},
            { Keys.Enter, OpenQA.Selenium.Keys.Enter},
            { Keys.Alt, OpenQA.Selenium.Keys.Alt},
            { Keys.F4, OpenQA.Selenium.Keys.F4},
            { Keys.ArrowUp, OpenQA.Selenium.Keys.ArrowUp},
            { Keys.ArrowDown, OpenQA.Selenium.Keys.ArrowDown},

        };

        public static string GetSeleniumKey(Keys key)
        {
            return AppKeyMap[key];
        }
    }
}

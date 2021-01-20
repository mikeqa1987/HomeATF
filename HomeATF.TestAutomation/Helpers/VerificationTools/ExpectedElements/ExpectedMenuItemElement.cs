using HomeATF.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.TestAutomation
{
    public class ExpectedMenuItemElement : ExpectedElement
    {
        public int? ItemsQuantity { get; set; }

        public string AccessKey { get; set; }

        public string[] Items { get; set; }

        public bool VerifyLabel(Element actualElement, out string errorDescription)
        {
            bool result = true;
            errorDescription = string.Empty;

            if (this.Label == null)
            {
                errorDescription += $"\nLabel for verification has not been provided.";
                return false;
            }

            string actualLabel = actualElement.Name;
            if ( actualLabel != this.Label)
            {
                errorDescription += $"\nActual menu label is \"{actualLabel}\" instead of expected \"{this.Label}\".";
                result = false;
            }

            return result;
        }

        public bool VerifyItemsQuantity(Element actualElement, out string errorDesc)
        {
            bool result = true;
            errorDesc = string.Empty;

            if (this.ItemsQuantity == null)
            {
                errorDesc += $"\nFor menu item {this.Label} expected menu count has not been provided.";
                return false;
            }

            actualElement.Click();
            int actualCount = actualElement.FindAllElements<Element>((By.LocalizedControlTypeProperty, "menu item")).Count();

            if (this.ItemsQuantity != --actualCount)
            {
                errorDesc += $"\nFor menu item {this.Label} actual menu count \"{actualCount}\" is not equal with expected \"{this.ItemsQuantity}\".";
                result = false;
            }

            actualElement.Click();

            return result;
        }

        public bool VerifyItems(Element actualElement, out string errorDescription)
        {
            bool result = true;
            errorDescription = string.Empty;

            if (this.Items == null)
            {
                errorDescription += $"\nFor menu item {this.Label} expected menu items list has not been provided.";
                return false;
            }

            actualElement.Click();
            var items = actualElement.FindAllElements<Element>((By.LocalizedControlTypeProperty, "menu item"));
            foreach (var item in this.Items)
            {
                Element found = items.FirstOrDefault(e => e.Name == item);
                if (found == null)
                {
                    errorDescription += $"\nFor menu \"{this.Label}\" expected menu item \"{item}\"has not been found.";
                    result = false;
                }
            }
            actualElement.Click();
            return result;
        }

        public bool VerifyAccessKey(Element actualElement, out string errorDescription)
        {
            bool result = true;
            errorDescription = string.Empty;

            if (this.AccessKey == null)
            {
                return true;
            }

            string currentAccessKey = actualElement.UiInstance.GetPropertyValue("AccessKey");
            if (currentAccessKey != this.AccessKey)
            {
                errorDescription += $"\nFor menu item {this.Label} actual accessKey \"{currentAccessKey}\" is not equal with expected \"{this.AccessKey}\".";
                result = false;
            }

            return result;
        }
    }
}

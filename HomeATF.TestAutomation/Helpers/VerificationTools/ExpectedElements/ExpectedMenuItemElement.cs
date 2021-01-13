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
        public int ItemsQuantity { get; set; }

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
                errorDescription += $"\nMenu label is \"{actualLabel}\" instead of \"{this.Label}\".";
                result = false;
            }

            return result;
        }
    }
}

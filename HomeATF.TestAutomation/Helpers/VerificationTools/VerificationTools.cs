using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeATF.Appium;

namespace HomeATF.TestAutomation
{
    public class VerificationTools
    {
        public static bool VerifyCommonElementLayout(Element actualElement, ExpectedElement expected, out string errorDesc)
        {
            if (actualElement == null)
            {
                errorDesc = $"\nElement with name \"{expected.Label}\" not found.";
                return false;
            }

            string error = string.Empty;
            errorDesc = error;

            bool result = true;

            if (!actualElement.IsEnabled)
            {
                errorDesc += $"\nElement \"{expected.Label}\" IsEnabled property is FALSE.";
                result = false;
            }

            if (!expected.VerifyBelongsTo(actualElement, out error))
            {
                result = false;
            }

            errorDesc += error;

            return result;
        }
        public static bool VerifyMenuItemLayout(Element actualElement, ExpectedMenuItemElement expected, out string errorDesc)
        {
            if (actualElement == null)
            {
                errorDesc = $"\nMenu item \"{expected.Label}\" not found.";
                return false;
            }

            bool result = true;
            string error = string.Empty;

            if (!VerifyCommonElementLayout(actualElement, expected, out string errorDescription))
            {
                result = false;
            }

            errorDesc = errorDescription;

            if (!expected.VerifyLabel(actualElement, out error))
            {
                result = false;
            }
            errorDesc += error;

            if (!expected.VerifyItemsQuantity(actualElement, out error))
            {
                result = false;
            }
            errorDesc += error;

            if (!expected.VerifyItems(actualElement, out error))
            {
                result = false;
            }
            errorDesc += error;

            if (!expected.VerifyAccessKey(actualElement, out error))
            {
                result = false;
            }
            errorDesc += error;
            return result;
        }
    }
}

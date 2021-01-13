using HomeATF.Appium;
using HomeATF.Elements.Notepad;
using System.Linq;

namespace HomeATF.TestAutomation
{
    public class ExpectedElement
    {
        public ExpectedElement()
        {
            this.Label = string.Empty;
        }
        public string Label { get; set; }

        public Element ParentElement { get; set; }

        public bool VerifyBelongsTo(Element actualElement, out string errorDescription)
        {
            bool result = true;
            errorDescription = string.Empty;
            if (this.ParentElement == null)
            {
                return true;
            }

            if (!this.ParentElement.FindAllElements<Element>().Any(e => e.RuntimeId == actualElement.RuntimeId))
            {
                result = false;
                errorDescription += $"Element {this.Label} is not a child of {this.ParentElement.Name} element";
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ServiceModel.Security;

namespace HomeATF.Appium
{
    public class Console
    {
        private readonly string delimeter = "=======================================================";
        public Console()
        { }

        public void WriteLine(string message)
        {
            Debug.WriteLine(message);            
        }

        public void PrintElement(Element element)
        {
            if (element == null)
                this.WriteLine($"{nameof(DebugTools)}: element is NULL");

            this.WriteLine(delimeter);
            this.WriteLine(element.Name);
            this.WriteLine(element.RuntimeId);
        }

        public void PrintElementsCollection(IEnumerable<Element> elementsCollection)
        {
            if (elementsCollection == null || !elementsCollection.Any())
            {
                this.WriteLine($"{nameof(DebugTools)}: elements collection is NULL or EMPTY");
            }
            this.WriteLine($"Collection has {elementsCollection?.Count()} elements inside:");
            foreach (var el in elementsCollection)
            {
                this.PrintElement(el);
            }
        }
    }
}

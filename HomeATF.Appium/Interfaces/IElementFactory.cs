using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium.Interfaces
{
    public interface IElementFactory
    {
        IEnumerable<(string key, object value)> DefaultConditionsToSearchElement(Type type);

        T CreateElement<T>(
            IUiElementWrapper wrapper,
            Navigator navigator, 
            IElementFactory eFactory, 
            ILogger logger
            ) where T : Element;
    }
}

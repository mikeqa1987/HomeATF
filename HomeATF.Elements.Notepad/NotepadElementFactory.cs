using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using HomeATF.Elements.Notepad.Elements;
using NLog;
using System;
using System.Collections.Generic;


namespace HomeATF.Elements.Notepad
{
    public class NotepadElementFactory : IElementFactory
    {
        public T CreateElement<T>(IUiElementWrapper wrapper, Navigator navigator, IElementFactory eFactory, ILogger logger)
            where T : Element
        {
            Type type = typeof(T);

            if (type == typeof(Element))
            {
                return new Element(wrapper, navigator, logger, eFactory) as T;
            }
            else if (type == typeof(MenuBar))
            {
                return new MenuBar(wrapper, navigator, eFactory, logger) as T;
            }
            else if (type == typeof(FileMenu))
            {
                return new FileMenu(wrapper, navigator, eFactory, logger) as T;
            }
            else if (type == typeof(EditMenu))
            {
                return new EditMenu(wrapper, navigator, eFactory, logger) as T;
            }
            else if (type == typeof(ViewMenu))
            {
                return new ViewMenu(wrapper, navigator, eFactory, logger) as T;
            }
            else if (type == typeof(TextEditor))
            {
                return new TextEditor(wrapper, navigator, eFactory, logger) as T;
            }
            else if (type == typeof(ButtonElement))
            {
                return new ButtonElement(wrapper, navigator, eFactory, logger) as T;
            }
            else
                return default;
        }

        public IEnumerable<(string key, object value)> DefaultConditionsToSearchElement(Type type)
        {
            if (type == typeof(Element))
            {
               
            }

            else if (type == typeof(MenuBar))
            {
                yield return (By.AutomationIdProperty, "MenuBar");
            }
            else if (type == typeof(FileMenu))
            {
                yield return (By.LocalizedControlTypeProperty, "menu item");
                yield return (By.NameProperty, "File");
            }
            else if (type == typeof(EditMenu))
            {
                yield return (By.LocalizedControlTypeProperty, "menu item");
                yield return (By.NameProperty, "Edit");
            }
            else if (type == typeof(ViewMenu))
            {
                yield return (By.LocalizedControlTypeProperty, "menu item");
                yield return (By.NameProperty, "View");
            }
            else if (type == typeof(TextEditor))
            {
                yield return (By.LocalizedControlTypeProperty, "document");
                yield return (By.NameProperty, "Text Editor");
            }
            else if (type == typeof(ButtonElement))
            {
                yield return (By.LocalizedControlTypeProperty, "Button");
            }
        }
    }
}

using Castle.Core.Internal;
using HomeATF.Appium.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    public class WindowsRootWindow : Element
    {
        private readonly ILogger logger;
        public WindowsRootWindow(IUiElementWrapper wrapper, Navigator navigator, ILogger logger, IElementFactory eFactory)
            : base(wrapper, navigator, logger, eFactory)
        {
            this.logger = logger;
        }

        public string Title => this.UiInstance.Name;

        public void CloseWindow()
        {
            if (!this.UiInstance.RuntimeId.IsNullOrEmpty())
            {
                this.logger.Trace("Closing window with name {name}", this.Title);
                this.UiInstance.PressKeysCombination(new Keys[] { Keys.Alt, Keys.F4 });
            }
        }

        public void MinimizeWindow()
        {
            this.logger.Trace("Minimizing window with name {name}", this.Title);
            this.UiInstance.PressKeysCombination(new Keys[] { Keys.Windows, Keys.ArrowDown });
            this.UiInstance.PressKeysCombination(new Keys[] { Keys.Windows, Keys.ArrowDown });

        }

        public void MaximizeWindow()
        {
            this.logger.Trace("Maximizing window with name {name}", this.Title);
            this.UiInstance.PressKeysCombination(new Keys[] { Keys.Windows, Keys.ArrowUp });
            this.UiInstance.PressKeysCombination(new Keys[] { Keys.Windows, Keys.ArrowUp });
        }
    }
}

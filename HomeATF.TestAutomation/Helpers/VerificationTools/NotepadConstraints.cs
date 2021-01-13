using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.TestAutomation
{
    public class NotepadConstraints : NUnit.Framework.Is
    {
        public static MenuItemConstraint MenuItemConstraint(ExpectedMenuItemElement expected)
        {
            return new MenuItemConstraint(expected);
        }

    }
}

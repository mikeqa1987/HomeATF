using HomeATF.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.TestAutomation
{
    public class BaseTestFixture
    {
        public BaseTestFixture(ITestContext context)
        {
            this.Context = context;
        }

        public ITestContext Context;
    }
}

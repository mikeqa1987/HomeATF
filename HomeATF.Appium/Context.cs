using HomeATF.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    public static class Context
    {
        public static ITestContext Create(ISettings settings, IElementFactory eFactory)
        {
            var logger = LoggerProvider.GetLogger(settings);
            var sessionHandler = AppiumSessionHandler.GetInstance(settings, logger);
            var wrapperFactory = new AppiumUiWrapperFactory();
            var navigationService = new Navigator(sessionHandler, wrapperFactory, settings, logger, eFactory);

            return new AppiumTestContext(sessionHandler, navigationService, logger, settings);
        }
    }
}

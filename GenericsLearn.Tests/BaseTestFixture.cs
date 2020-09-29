using System;
using NUnit.Framework;
using GenericsLearn.Tests.Settings;
using HomeATF.Elements.Notepad;
using HomeATF.Appium.Interfaces;
using HomeATF.Appium;

namespace HomeATF.Appium.Tests
{
    public abstract class BaseTestFixture
    {
        protected const string Parameterized = "Parameterized";
        protected ITestContext testContext;

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            var settings = new NUnitSettings(TestContext.Parameters);

            //TODO Make it switchable between different factories
            var notepadFactory = new NotepadElementFactory();
            this.testContext = Context.Create(settings, notepadFactory);
        }

        [SetUp]
        public virtual void SetUp()
        {
            this.testContext.StartSession();
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.testContext.EndSession();
        }
    }
}

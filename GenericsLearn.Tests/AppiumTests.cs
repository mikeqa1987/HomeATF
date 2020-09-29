using System;
using NUnit.Framework;
using HomeATF.TestAutomation.Tests;

namespace HomeATF.Appium.Tests
{
    [TestFixture]
    public class AppiumTests : BaseTestFixture
    {
        private BasicTests tests;

        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            tests = new BasicTests(this.testContext);           
        }

        [Test]
        public void SaveTest() => this.tests.SaveTest();

        [Test]
        public void OpenTest() => this.tests.OpenTest();

        [Test]
        public void ReplaceTextTest() => this.tests.ReplaceTextTest();

        
    }
}

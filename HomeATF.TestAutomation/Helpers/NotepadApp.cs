using System;
using System.Collections.Generic;
using System.Linq;
using HomeATF.Appium;
using HomeATF.Appium.Interfaces;
using HomeATF.Elements.Notepad;

namespace HomeATF.TestAutomation.Helpers
{
    public class NotepadApp
    {
        private readonly string appTitle;
        private ITestContext testContext;
        private WindowsRootWindow appWindow;

        public NotepadApp(ITestContext testContext, string appTitle)
        {
            this.testContext = testContext;
            this.appTitle = appTitle;
            this.appWindow = InitializeRoot();
        }

        private WindowsRootWindow InitializeRoot()
        {
            var root = this.GetRootWindow();
            return root;
        }
        public WindowsRootWindow GetRootWindow()
        {
            if (this.appWindow != null)
            {
                return this.appWindow;
            }

            return this.appWindow = this.testContext.GetRoot(appTitle);
        }

        public string Title  
            {
            get
            {
                var titleBar = this.appWindow.FindElement<Element>((By.LocalizedControlTypeProperty, "title bar"));
                return titleBar.UiInstance.GetPropertyValue("Value.Value");
            }
            }

        public TextEditor TextEditor => appWindow?.FindElement<TextEditor>();

        public void SaveFile(string fullPathName)
        {

            this.testContext.Logger.Trace("{wrapper}: Saving document to file: {name}", nameof(NotepadApp), fullPathName);

            var window = this.GetRootWindow();

            var fileMenu = window.FindElement<FileMenu>();
            fileMenu.ClickSave();
            var saveDialog = testContext.GetRoot("Save As");

            var edit = saveDialog.FindElement<Element>((By.LocalizedControlTypeProperty, "edit"), (By.NameProperty, "File name:"));

            edit.SendKeys(fullPathName);

            var saveButton = saveDialog.FindElement<ButtonElement>((By.NameProperty, "Save"));
            saveButton.Click();

            this.testContext.AwaitingService.WaitFor(TimeSpan.FromMilliseconds(1000));
            
        }

        public void OpenFile(string fullPathName)
        {

            this.testContext.Logger.Trace("{wrapper}: Opens file: {name}", nameof(NotepadApp), fullPathName);

            var window = this.GetRootWindow();

            var fileMenu = window.FindElement<FileMenu>();
            fileMenu.ClickOpen();
            
            var openDialog = testContext.GetRoot("Open");

            var edit = openDialog.FindElement<Element>((By.LocalizedControlTypeProperty, "edit"), (By.NameProperty, "File name:"));

            edit.SendKeys(fullPathName);

            this.testContext.AwaitingService.WaitFor(TimeSpan.FromMilliseconds(1000));

            var openButton = openDialog.FindElement<ButtonElement>((By.NameProperty, "Open"));

            this.testContext.AwaitingService.WaitFor(() => { return openButton.UiInstance.IsVisible; }, TimeSpan.FromSeconds(10));
            openButton.PressKey(Keys.Enter);            

        }

        public string GetStatusBarValue()
        {
            Element statusBar = null;

            try
            {
                statusBar = this.appWindow.FindElement<Element>((By.AutomationIdProperty, "1025"));
            }
            catch
            {
                var viewMenu = this.appWindow.FindElement<ViewMenu>();
                viewMenu.EnableStatusBar();
            }

            statusBar = this.appWindow.FindElement<Element>((By.AutomationIdProperty, "1025"));
            var textBar = statusBar.FindAllElements<Element>((By.LocalizedControlTypeProperty, "text")).Skip(1).FirstOrDefault();

            return textBar.Name;
        }

    }
}

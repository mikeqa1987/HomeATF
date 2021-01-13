using HomeATF.Appium.Interfaces;
using System;
using HomeATF.TestAutomation.Helpers;
using NUnit.Framework; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Services;
using HomeATF.Elements.Notepad.Elements;
using HomeATF.Appium;
using HomeATF.Elements.Notepad;
using System.Threading;

namespace HomeATF.TestAutomation.Tests
{
    public class BasicTests : BaseTestFixture
    {
        public BasicTests(ITestContext context) : base(context)
        { }

        public void SaveTest()
        {
            var notepadApp = new NotepadApp(this.Context, "Untitled - Notepad");

            //notepad.MaximizeWindow();

            var textEdit = notepadApp.TextEditor;
            textEdit.TypeText($"Test message to check typing. Today is {DateTime.Now}");
            var saveName = $"testSaving_{Guid.NewGuid().ToString()}.txt";
            var savePath = $@"C:\Work\"+ saveName;

            notepadApp.SaveFile(savePath);

            Assert.AreEqual($"{saveName} - Notepad", notepadApp.Title);
        }

        public void OpenTest()
        {
            var notepadApp = new NotepadApp(this.Context, "Untitled - Notepad");
            notepadApp.OpenFile(@"C:\Work\testFile.txt");

            Assert.AreEqual("testFile.txt - Notepad", notepadApp.Title);
            Assert.AreEqual("TpTrace", notepadApp.TextEditor.CurrentText);
        }

        public void ReplaceTextTest()
        { 
            var notepadApp = new NotepadApp(this.Context, "Untitled - Notepad");

            var textEditor = notepadApp.TextEditor;
            textEditor.TypeText("This text is absolutely valid.");
            textEditor.PressKey(Appium.Keys.Enter);
            textEditor.TypeText("This text is absolutely valid.");

            var editMenu = notepadApp.GetRootWindow().FindElement<EditMenu>();
            editMenu.Click();

            var replace = editMenu.FindElement<Element>((By.AutomationIdProperty, "23"));

            this.Context.AwaitingService.WaitFor(() => { return replace.UiInstance.IsVisible; }, TimeSpan.FromSeconds(5));

            replace.Click();

            var replaceWindow = this.Context.GetRoot("Replace");
            var fWhatEdit = replaceWindow.FindElement<Element>((By.ClassNameProperty, "Edit"), (By.NameProperty, "Find what:"));
            fWhatEdit.SendKeys("valid");

            var rWithEdit = replaceWindow.FindElement<Element>((By.ClassNameProperty, "Edit"), (By.NameProperty, "Replace with:"));
            rWithEdit.SendKeys("invalid");

            var repAllButton = replaceWindow.FindElement<ButtonElement>((By.NameProperty, "Replace All"));
            repAllButton.Click();

            this.Context.AwaitingService.WaitFor(TimeSpan.FromSeconds(5));
            replaceWindow.FindElement<ButtonElement>((By.NameProperty, "Cancel")).Click();

            string expectedText = "This text is absolutely invalid.\r\nThis text is absolutely invalid.";

            Assert.AreEqual(expectedText, textEditor.CurrentText);
        }

        public void MenuItemsTest()
        {
            var notepadApp = new NotepadApp(this.Context, "Untitled - Notepad");

            var menuBar = notepadApp.GetRootWindow().FindElement<MenuBar>();
            var fileMenu = notepadApp.GetRootWindow().FindElement<FileMenu>();

            Assert.That(fileMenu, NotepadConstraints.MenuItemConstraint(new ExpectedMenuItemElement()
            {
                Label = "File",
                ParentElement = menuBar,
            }));
        }

        public void StatusBarTest()
        {
            var notepadApp = new NotepadApp(this.Context, "Untitled - Notepad");

            var currentValue = notepadApp.GetStatusBarValue();
            Assert.AreEqual("   Ln 1, Col 1  ", currentValue);

            notepadApp.TextEditor.TypeText("A");
            currentValue = notepadApp.GetStatusBarValue();
            Assert.AreEqual("   Ln 1, Col 2  ", currentValue);

            notepadApp.TextEditor.TypeText("B");
            currentValue = notepadApp.GetStatusBarValue();
            Assert.AreEqual("   Ln 1, Col 3  ", currentValue);

            notepadApp.TextEditor.TypeText("C");
            currentValue = notepadApp.GetStatusBarValue();
            Assert.AreEqual("   Ln 1, Col 4  ", currentValue);

            notepadApp.TextEditor.NewLine();

            notepadApp.TextEditor.TypeText("D");
            currentValue = notepadApp.GetStatusBarValue();
            Assert.AreEqual("   Ln 2, Col 2  ", currentValue);

            notepadApp.TextEditor.TypeText("E");
            currentValue = notepadApp.GetStatusBarValue();
            Assert.AreEqual("   Ln 2, Col 3  ", currentValue);

            notepadApp.TextEditor.TypeText("F");
            currentValue = notepadApp.GetStatusBarValue();
            Assert.AreEqual("   Ln 2, Col 4  ", currentValue);

            for (int i = 0; i < 200; i++)
            {
                notepadApp.TextEditor.Click(i, i);
                Thread.Sleep(500);
            }
            
        }
    }
}

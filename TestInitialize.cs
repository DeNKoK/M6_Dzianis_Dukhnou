using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace M6_Dzianis_Dukhnou
{
    public class TestInitialize
    {
        public IWebDriver driver;
        public string baseUrl;
        public string userName;
        public string userPassword;

        [SetUp]
        public void Setup()
        {
            var service = FirefoxDriverService.CreateDefaultService();
            this.driver = new FirefoxDriver(service);

            this.baseUrl = "https://mail.yandex.by";
            this.driver.Manage().Window.Maximize();
            this.driver.Navigate().GoToUrl(this.baseUrl);

            this.userName = "DzianisM6";
            this.userPassword = "HometaskM6";
        }

        [TearDown]
        public void TearDown()
        {
            Logoff();

            this.driver.Close();
            this.driver.Quit();
        }

        public void WaitElement(By element, int timeoutSecs = 10)
        {
            new WebDriverWait(this.driver, TimeSpan.FromSeconds(timeoutSecs))
                .Until(ExpectedConditions.ElementIsVisible(element));
        }

        public void LoginWithCredentials(string accountId, string password)
        {
            //Click Login
            this.driver.FindElement(By.CssSelector(".button2_theme_mail-white"))
                .Click();

            //Input the userId and submit
            WaitElement(By.Id("passp-field-login"));
            this.driver.FindElement(By.Id("passp-field-login"))
                .SendKeys(accountId);
            this.driver.FindElement(By.XPath("//button[contains(@class, 'Button2')]"))
                .Click();

            //Input the password and submit
            WaitElement(By.Id("passp-field-passwd"));
            this.driver.FindElement(By.Id("passp-field-passwd"))
                .SendKeys(password);
            this.driver.FindElement(By.XPath("//button[@type='submit']"))
                .Click();

            WaitElement(By.XPath("//span[@class='user-account__name']"));
        }

        public void Logoff()
        {
            //Click on the User icon
            this.driver.FindElement
                (By.XPath("//div[contains(@class, 'user-pic user-pic')]"))
                .Click();

            //LogOff
            WaitElement(By.XPath("//span[text() = 'Выйти из сервисов Яндекса']"));
            this.driver.FindElement
                (By.XPath("//span[text() = 'Выйти из сервисов Яндекса']"))
                .Click();
        }

        public void CreateDraftEmail(string emailTo, string subject, string message)
        {
            //Create the letter
            this.driver.FindElement(By.XPath("//a[contains(@class, 'mail-ComposeButton')]"))
                .Click();

            //Input the email address
            WaitElement(By.XPath("//div[@class = 'composeYabbles']"));
            this.driver.FindElement(By.XPath("//div[@class = 'composeYabbles']"))
                .SendKeys(emailTo);

            //Input the subject
            this.driver.FindElement(By.XPath("//span[text() = 'Тема']"))
                .Click();
            this.driver.FindElement(By.XPath("//input[contains(@class, 'ComposeSubject-TextField')]"))
                .Click();
            this.driver.FindElement(By.XPath("//input[contains(@class, 'ComposeSubject-TextField')]"))
                .SendKeys(subject);

            //Input the message
            this.driver.FindElement(By.XPath("//div[@role = 'textbox']"))
                .SendKeys(message);

            //Find the draft letter
            this.driver.FindElement(By.XPath("//span[text() = 'Черновики']"))
                .Click();
            WaitElement(By.XPath($"//span[@Title = '{subject}']"));
        }

        public void SendDraftEmail()
        {
            //Open the draft email
            this.driver.FindElement(By.CssSelector(".mail-MessageSnippet-Item_sender"))
                .Click();

            //Click 'Send' button
            WaitElement(By.XPath("//span[text() = 'Отправить']"));
            this.driver.FindElement(By.CssSelector(".ComposeControlPanelButton-Button_action"))
                .Click();

            //Wait for the result and reopen the draft folder
            WaitElement(By.XPath("//span[text() = 'Письмо отправлено']"));
            this.driver.FindElement(By.XPath("//div[contains(@data-key, 'box=fill-height-placeholder-box')]"))
                .Click();
            this.driver.FindElement(By.XPath("//span[text() = 'Черновики']"))
                .Click();
            WaitElement(By.XPath($"//div[@title = 'Создать шаблон']"));
        }

        public void DeleteDraftEmail()
        {
            //Open the draft Folder
            this.driver.FindElement(By.XPath("//span[text() = 'Черновики']"))
                .Click();

            //Update the view
            this.driver.FindElement(By.XPath("//span[@data-click-action='mailbox.check']"))
                .Click();

            //Select All
            this.driver.FindElement(By.XPath("//span[@class = 'checkbox_view']"))
                .Click();

            //Delete
            this.driver.FindElement(By.XPath("//div[contains(@title, 'Delete')]"))
                .Click();
        }

        public void DeleteSentEmail()
        {
            //Open the sent Folder
            this.driver.Navigate().Refresh();
            WaitElement(By.XPath("//span[text() = 'Отправленные']"));
            this.driver.FindElement(By.XPath("//span[text() = 'Отправленные']"))
                .Click();

            //Update the view
            this.driver.FindElement(By.XPath("//span[@data-click-action='mailbox.check']"))
                .Click();

            //Select All
            this.driver.FindElement(By.CssSelector(".ns-view-toolbar-button-main-select-all > label:nth-child(1) > span"))
                .Click();

            //Delete
            this.driver.FindElement(By.XPath("//div[contains(@title, 'Delete')]"))
                .Click();
        }

        public bool IsElementDisplayed(By element)
        {
            try
            {
                return this.driver.FindElement(element).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
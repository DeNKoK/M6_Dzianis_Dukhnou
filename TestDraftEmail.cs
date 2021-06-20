using NUnit.Framework;
using OpenQA.Selenium;

namespace M6_Dzianis_Dukhnou
{
    [TestFixture]
    public class TestDraftEmail : TestInitialize
    {
        string subject;
        string emailTo;
        string message;

        [SetUp]
        public void TestDraftEmail_SetUp()
        {
            emailTo = "dzianis.dukhnou@thomsonreuters.com";
            subject = "TestEmail";
            message = "TestEmail";
        }

        [TearDown]
        public void TestDraftEmail_Teardown()
        {
            DeleteDraftEmail();
        }

        [Test]
        public void CreatingDraftEmail()
        {
            LoginWithCredentials(userName, userPassword);
            CreateDraftEmail(emailTo, subject, message);

            Assert.IsTrue(IsElementDisplayed(By.XPath($"//span[@Title = '{subject}']")),
                $"The draft email with {subject} is not in the list");
        }

        [Test]
        public void CreatingDraftEmail_VerifyContent()
        {
            LoginWithCredentials(userName, userPassword);
            CreateDraftEmail(emailTo, subject, message);

            //Openning created draft letter
            this.driver.FindElement(By.XPath($"//span[@Title = '{subject}']"))
                .Click();
            var actualEmail = this.driver.FindElement(By.XPath("//div[@class = 'ComposeYabble-Text']"))
                .Text;
            var actualSubject = this.driver.FindElement
                (By.CssSelector(".mail-MessageSnippet-Item_subject > span:nth-child(1)"))
                .Text;
            var actualMessage = this.driver.FindElement(By.CssSelector(".cke_wysiwyg_div"))
                .Text;

            Assert.AreEqual(emailTo, actualEmail);
            Assert.AreEqual(subject, actualSubject);
            Assert.AreEqual(message, actualMessage);
        }
    }
}
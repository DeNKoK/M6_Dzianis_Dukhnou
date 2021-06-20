using NUnit.Framework;
using OpenQA.Selenium;

namespace M6_Dzianis_Dukhnou
{
    [TestFixture]
    public class TestSendEmail : TestInitialize
    {
        string emailTo;
        string subject;
        string message;

        [SetUp]
        public void TestSendEmail_SetUp()
        {
            emailTo = "dzianis.dukhnou@thomsonreuters.com";
            subject = "TestEmail";
            message = "TestEmail";
        }

        [TearDown]
        public void TestSendEmail_TearDown()
        {
            DeleteSentEmail();
        }

        [Test]
        public void SendingDraftEmail_CheckDraftFolder()
        {
            LoginWithCredentials(userName, userPassword);
            CreateDraftEmail(emailTo, subject, message);
            SendDraftEmail();

            Assert.IsFalse(IsElementDisplayed(By.XPath($"//span[@Title = '{subject}']")),
                "The email still exists in the draft folder.");
        }

        [Test]
        public void SendingDraftEmail_CheckSentFolder()
        {
            LoginWithCredentials(userName, userPassword);
            CreateDraftEmail(emailTo, subject, message);
            SendDraftEmail();

            //Go to the sent folder and update
            this.driver.FindElement(By.XPath("//span[text() = 'Отправленные']"))
                .Click();
            this.driver.FindElement(By.XPath("//span[@data-click-action='mailbox.check']"))
                .Click();
            WaitElement(By.XPath($"//span[@Title = '{subject}']"));

            Assert.IsTrue(IsElementDisplayed(By.XPath($"//span[@Title = '{subject}']")),
                "The email still exists in the draft folder.");
        }
    }
}

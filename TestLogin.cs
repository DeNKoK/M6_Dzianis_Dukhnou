using NUnit.Framework;
using OpenQA.Selenium;

namespace M6_Dzianis_Dukhnou
{
    [TestFixture]
    public class TestLogin : TestInitialize
    {

        [Test]
        public void Login()
        {
            LoginWithCredentials(userName, userPassword);

            Assert.IsTrue(IsElementDisplayed(By.XPath($"//span[text() ='{userName}']")),
                "Login is not successful");
        }

    }
}
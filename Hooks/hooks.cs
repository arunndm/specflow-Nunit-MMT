using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace MMT.Hooks
{
    [Binding]
    public class hooks
    {
        public IWebDriver driver;

        public hooks(IWebDriver _driver)
        {
            driver = _driver;
        }

        [AfterScenario]
        public void afterScenario()
        { //tear off
            driver.Quit();
        }
    }
}

using BoDi;
using MMT.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace MMT.Hooks
{
    [Binding]
    public class driverSetup
    {
        private IObjectContainer _objectContainer;
        public IWebDriver _driver;
        private configuration _configuration => new configuration();

        public driverSetup(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void beforeScenario()
        { // initialising the driver with chromedriver
            _driver = new ChromeDriver(_configuration.chromedriverLocation);
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configuration.implicitWaitTime);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.pageLoadTime);
            _objectContainer.RegisterInstanceAs(_driver);
        }
    }
}

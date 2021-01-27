using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using MMT.Locators;
using MMT.Helpers;
using System.Diagnostics;

namespace MMT.StepDefinition
{
    [Binding]
    public class stepDefinitions
    {
        String sut_url = "https://www.makemytrip.com/";
        IWebDriver driver;

        private locator _locator => new locator(driver);
        private helper _helper => new helper(driver);

        public stepDefinitions(IWebDriver _driver)
        {
            driver = _driver;
        }

        [Given(@"makemytrip website is loaded")]
        public void GivenMakemytripWebsiteIsLoaded()
        {
            driver.Navigate().GoToUrl(sut_url);
            Thread.Sleep(3500);                    
            if(_locator.outsideModal.Count>0)
                _locator.outsideModal.FirstOrDefault().Click();
        }

        [Given(@"I click on the (ROUNDTRIP||MULTICITY)")]
        public void GivenIClickOnTheRoundTrip(string travelOption)
        {
            Thread.Sleep(3500);
            _helper.travelOptionSelector(travelOption);  //function to select the travel option based on the input in feature file
        }

        [When(@"I enter ""(.*)"" in (FROM||FIRST FROM||SECOND FROM||TO||FIRST TO||SECOND TO) of (MULTICITY||ROUNDTRIP)")]
        public void WhenIEnterInTheTOField(string city, string pointofEntry, string travelOption)
        {
            _helper.sourceDestinationSelector(city, pointofEntry, travelOption);  //function to enter the city in the corresponding ui locator based on the input in feature file
        }

        [When(@"I add ""(.*)"" days from current day to (FIRST DEPARTURE||SECOND DEPARTURE||THE RETURN||THE DEPARTURE) field")]
        public void WhenIEnterInDepartureField(string dayCount, string dateElement)
        {
            _helper.dateSelector(dayCount); // function to set the date in date selectors based on the input in feature file
        }

        [When(@"I add ""(.*)"" count in adults ""(.*)"" count in children ""(.*)"" count in infant")]
        public void WhenIAddCountInAdultsCountInChildrenCountInInfant(int adultCount, int childrenCount, int infantCount)
        {
            Thread.Sleep(2000);
            _helper.paxCountSelector(adultCount, childrenCount, infantCount);   //function to set the pax count in ui elements based on the the inputs from feature file
        }

        [When(@"I click search button")]
        public void WhenIClickSearchButton()
        {
            Thread.Sleep(2000);
            _locator.searchButton.Click();
        }

        [Then(@"the result to be displayed")]
        public void ThenTheResultShouldBeDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));
            wait.Until(x => x.FindElement(By.CssSelector("div.price_sorter>span>span")));
        }

        [Then(@"the roundtrip result to be displayed")]
        public void ThenTheRoundtripResultToBeDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(x => x.FindElement(By.Id("sorter_btn_onward")));
        }

        [When(@"I sort the flight with ascending order of price")]
        public void WhenISortTheFlightWithAscendingOrderOfPrice()
        {
            Thread.Sleep(15000);
            if (_locator.priceSortCheck.Count == 0)
                _locator.priceSortButton.Click(); //clicking on price sort button to sort the flights in ascending order
        }

        [Then(@"the price to be sorted in ascending order")]
        public void ThenThePriceToBeSortedInAscendingOrder()
        {
            List<String> prices = new List<string>();
            foreach (IWebElement element in _locator.pricesElementList)
            {
                prices.Add(element.Text);  // Adding the price of each flights to a list of strings for the ease of assertion
            }

            Assert.That(prices, Is.Ordered); // Asserting if the prices of each flight is in ascending order
        }

        [Then(@"I select the flight with lowest surcharge")]
        public void ThenISelectTheFlightWithLowestSurcharge()
        {
            _helper.selectFlightWithLowestSurcharge(); // function to find and select/click on the flight with lowest surcharge
        }

        [Then(@"I print the itenary of the selected flight")]
        public void ThenIPrintTheItenaryOfTheSelectedFlight()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());  // transfer control to newly opened tab
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(x => x.FindElements(By.CssSelector("div.airways-info-sect")));

            for (int i = 0; i < _locator.deptCityList.Count; i++)  // printing the itenary details in output panel
            {
                Debug.WriteLine(_locator.airwaysNameList[i].Text+" from "+ _locator.deptCityList[i].Text+" at time "+ _locator.deptTimeList[i].Text+" reaches "+ _locator.arrivalCityList[i].Text+" at time "+ _locator.arrivalTimeList[i].Text);
            }
        }

        [Then(@"I select the flight combination with lowest fare")]
        public void ThenISelectTheFlightCombinationWithLowestFare()
        {
            _helper.selectFlightCombinationWithLowestFare();  // function to find the flight combination with lowest fare
        }

        [Then(@"I validate each cities in provided Excel with that available in website")]
        public void ThenIValidateEachCitiesInProvidedExcelWithThatAvailableInWebsite()
        {
            _helper.excelValidator();  // function to validate each cities in the provided excel with that avaiable in website
        }

    }
}

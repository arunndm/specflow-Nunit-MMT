using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MMT.Locators
{
    public class locator
    {
        private IWebDriver driver;

        public locator(IWebDriver _driver)
        {
            driver = _driver;
        }

        public IWebElement addButton => driver.FindElement(By.Id("addbutton"));

        public IReadOnlyCollection<IWebElement> outsideModal => driver.FindElements(By.CssSelector("div.loginModal.displayBlock.modalLogin.dynHeight.personal"));

        public IWebElement multicitySelector => driver.FindElement(By.CssSelector("ul.fswTabs.latoBlack.greyText > li:nth-child(3)"));

        public IWebElement roundTripSelector => driver.FindElement(By.CssSelector("ul.fswTabs.latoBlack.greyText > li:nth-child(2)"));

        public IWebElement multicityFrom1 => driver.FindElement(By.Id("fromAnotherCity0"));

        public IWebElement firstAutoSuggestItem => driver.FindElement(By.Id("react-autowhatever-1-section-0-item-0"));

        public IWebElement multicityTo1Input => driver.FindElement(By.XPath("//*[@id=\"toAnotherCity0\"]/../../div/div/div/div/input"));

        public IWebElement multicityFrom2 => driver.FindElement(By.Id("fromAnotherCity1"));

        public IWebElement multicityTo2Input => driver.FindElement(By.XPath("//*[@id=\"toAnotherCity1\"]/../../div/div/div/div/input"));

        public IWebElement cityElementLo => driver.FindElement(By.Id("fromCity"));

        public IWebElement cityElement => driver.FindElement(By.XPath("//*[@id=\"fromCity\"]/../../div/div/div/div/input"));

        public IWebElement toCityElement => driver.FindElement(By.XPath("//*[@id=\"toCity\"]/../../div/div/div/div/input"));

        public IWebElement paxCountElement => driver.FindElement(By.XPath("//*[@id=\"travellers\"]/../span"));

        public List<IWebElement> guestClassList => driver.FindElements(By.CssSelector("ul.guestCounter")).ToList();

        public IWebElement applyButton => driver.FindElement(By.CssSelector("button.btnApply"));

        public IWebElement searchButton => driver.FindElement(By.CssSelector("a.widgetSearchBtn"));

        public IWebElement priceSortButton => driver.FindElement(By.CssSelector("div.price_sorter>span>span"));

        public IReadOnlyCollection<IWebElement> priceSortCheck => driver.FindElements(By.CssSelector("div.price_sorter>span>span.down"));

        public List<IWebElement> pricesElementList => driver.FindElements(By.CssSelector("span.actual-price")).ToList();

        public List<IWebElement> resultFlightList => driver.FindElements(By.CssSelector("div.fli-list.multi-city")).ToList();

        public List<IWebElement> deptTimeList => driver.FindElements(By.CssSelector("div.dept-time")).ToList();

        public List<IWebElement> deptCityList => driver.FindElements(By.CssSelector("p.dept-city")).ToList();

        public List<IWebElement> arrivalTimeList => driver.FindElements(By.CssSelector("p.reaching-time")).ToList();

        public List<IWebElement> arrivalCityList => driver.FindElements(By.CssSelector("p.arrival-city")).ToList();

        public List<IWebElement> airwaysNameList => driver.FindElements(By.CssSelector("div.airways-info-sect")).ToList();

        public List<IWebElement> onwardsFlightList => driver.FindElements(By.CssSelector("#ow-domrt-jrny>div>div.fli-list")).ToList();

        public List<IWebElement> returnFlightList => driver.FindElements(By.CssSelector("#rt-domrt-jrny>div>div.fli-list")).ToList();

        public List<IWebElement> disabledSubmitButton => driver.FindElements(By.CssSelector("button.fli_primary_btn.disabled")).ToList();

        public IWebElement submitButton => driver.FindElement(By.CssSelector("button.fli_primary_btn"));

        public IWebElement fromCity => driver.FindElement(By.Id("fromCity"));

        public List<IWebElement> autoSuggestFromRecentList => driver.FindElements(By.CssSelector("#react-autowhatever-1 > div:nth-child(1) > ul>li")).ToList();

        public List<IWebElement> autoSuggestFromPopularCities => driver.FindElements(By.CssSelector("#react-autowhatever-1 > div:nth-child(2) > ul>li")).ToList();

        public List<IWebElement> autoSuggestSearchResultList => driver.FindElements(By.CssSelector("#react-autowhatever-1 > div.react-autosuggest__section-container.react-autosuggest__section-container--first > ul > li")).ToList();

        public IWebElement toCity => driver.FindElement(By.Id("toCity"));

        public List<IWebElement> autoSuggestToRecentList => driver.FindElements(By.CssSelector("#react-autowhatever-1 > div:nth-child(1) > ul>li")).ToList();

        public List<IWebElement> autoSuggestToPopularCities => driver.FindElements(By.CssSelector("#react-autowhatever-1 > div:nth-child(2) > ul>li")).ToList();
    }
}

using MMT.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using MMT.Configurations;

namespace MMT.Helpers
{
    public class helper
    {
        private IWebDriver driver;
        private locator _locator => new locator(driver);
        private configuration _configuration => new configuration();

        public helper(IWebDriver _driver)
        {
            driver = _driver;
        }

        /// <summary>
        /// function to select the travel option based on the input in feature file
        /// </summary>
        /// <param name="travelOption"></param>
        public void travelOptionSelector(string travelOption)
        {
            if (travelOption == "ROUNDTRIP")
                _locator.roundTripSelector.Click();
            else if (travelOption == "MULTICITY")
                _locator.multicitySelector.Click();
        }

        /// <summary>
        /// function to enter the city in the corresponding ui locator based on the input in feature file
        /// </summary>
        /// <param name="city"></param>
        /// <param name="pointofEntry"></param>
        /// <param name="travelOption"></param>
        public void sourceDestinationSelector(string city, string pointofEntry, string travelOption)
        {
            IWebElement selectedCity = null;
            IWebElement selectedField = null;
            Thread.Sleep(3500);
            do
            {
                if (travelOption == "MULTICITY")
                {
                    if (pointofEntry == "FIRST FROM")
                    {
                        selectedField = _locator.multicityFrom1;
                    }
                    else if (pointofEntry == "SECOND FROM")
                    {
                        selectedField = _locator.multicityFrom2;
                    }
                    else if (pointofEntry == "FIRST TO")
                    {
                        selectedField = _locator.multicityTo1Input;
                    }
                    else if (pointofEntry == "SECOND TO")
                    {
                        selectedField = _locator.multicityTo2Input;
                    }
                }
                else if (travelOption == "ROUNDTRIP")
                {
                    if (pointofEntry == "FROM")
                    {
                        selectedField = _locator.cityElementLo;
                    }
                    else if (pointofEntry == "TO")
                    {
                        selectedField = _locator.toCityElement;
                    }
                }

                if (selectedField != null)
                {
                    selectedField.SendKeys(city);
                }

                Thread.Sleep(3500);
                selectedCity = _locator.autoSuggestSearchResultList.Find(x => x.Text.Contains(city));

                if (selectedCity != null)
                {
                    selectedCity.Click();
                }
                else
                {
                    selectedField.Clear();
                }
            } while (selectedCity == null);
        }

        /// <summary>
        /// function to set the date in date selectors based on the input in feature file
        /// </summary>
        /// <param name="dayCount"></param>
        public void dateSelector(string dayCount)
        {
            var thisDate1 = DateTime.Today.AddDays(Convert.ToInt32(dayCount));
            String FromDate = thisDate1.ToString("ddd MMM dd yyyy");

            IWebElement fromDateElement = driver.FindElement(By.XPath("//div[@aria-label=\"" + FromDate + "\"]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(false);", fromDateElement);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(x => x.FindElement(By.XPath("//div[@aria-label=\"" + FromDate + "\"]")).Displayed);

            Thread.Sleep(1000);
            fromDateElement.Click();
        }

        /// <summary>
        /// function to set the pax count in ui elements based on the the inputs from feature file
        /// </summary>
        /// <param name="adultCount"></param>
        /// <param name="childrenCount"></param>
        /// <param name="infantCount"></param>
        public void paxCountSelector(int adultCount, int childrenCount, int infantCount)
        {
            _locator.paxCountElement.Click();

            Thread.Sleep(2000);

            List<IWebElement> adultList = _locator.guestClassList[0].FindElements(By.TagName("li")).ToList();
            adultList[(adultCount - 1)].Click();

            List<IWebElement> childrenList = _locator.guestClassList[1].FindElements(By.TagName("li")).ToList();
            childrenList[childrenCount].Click();

            List<IWebElement> infantList = _locator.guestClassList[2].FindElements(By.TagName("li")).ToList();
            infantList[infantCount].Click();

            _locator.applyButton.Click();
        }

        /// <summary>
        /// function to find and select/click on the flight with lowest surcharge
        /// </summary>
        public void selectFlightWithLowestSurcharge()
        {
            int selectedflight = 1;
            decimal lowestSurcharge = 0;
            decimal currSurcharge = 0;

            for (int i = 0; i < _locator.resultFlightList.Count; i++) // looping the flight details list
            {
                //obtain and click on the Flight Details button in each of the list item
                IWebElement flightDetails = _locator.resultFlightList[i].FindElement(By.CssSelector("div.fli-list.multi-city>div>div:nth-child(2)>div>div"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(false);", flightDetails);
                flightDetails.Click();

                Thread.Sleep(2000);

                //obtain and click on the Fare Summary tab in each of the list item
                IWebElement fareSummary = _locator.resultFlightList[i].FindElement(By.LinkText("FARE SUMMARY"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(false);", fareSummary);
                fareSummary.Click();

                Thread.Sleep(2000);

                //obtain, calculate the surcharge of each of the list item and save in variable currSurcharge
                IWebElement surchargeElement = _locator.resultFlightList[i].FindElement(By.CssSelector("div.fli-list-fare_details__left>p:nth-child(4)>span:nth-child(2)"));
                String strSurcharge = surchargeElement.Text;
                strSurcharge = strSurcharge.Substring(1);
                strSurcharge = strSurcharge.Replace(",", "");
                strSurcharge = strSurcharge.Trim();
                currSurcharge = Convert.ToDecimal(strSurcharge);

                //applying the logic to find the lowest surcharge by assuming the first srcharge as lowest
                //and comparing the next one to the current one. if the next surcharge is lower, 
                //it is considered as the lowestSurcharge and so on
                if (lowestSurcharge == 0)
                    lowestSurcharge = currSurcharge; // assuming the first surcharge as lowest
                if (currSurcharge < lowestSurcharge)
                {
                    selectedflight = i;
                    lowestSurcharge = currSurcharge;
                }
            }
            // flight with lowest surcharge is obtained and clicked
            IWebElement lowSurchargeRoute = driver.FindElements(By.CssSelector("button.fli_primary_btn"))[selectedflight];
            Actions actions = new Actions(driver);
            actions.MoveToElement(lowSurchargeRoute);
            actions.Perform();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(x => x.FindElements(By.CssSelector("button.fli_primary_btn"))[selectedflight]);

            lowSurchargeRoute.Click();
        }

        /// <summary>
        /// function to find the flight combination with lowest fare
        /// </summary>
        public void selectFlightCombinationWithLowestFare()
        {
            bool flightSelected = false;

            // make sure there are onwards and return flights 
            if (_locator.onwardsFlightList.Count > 0 && _locator.returnFlightList.Count > 0) 
            {
                // make sure the submit button is disabled. Disbaled button is enabled only 
                //when a valid flight combination is selected
                if (_locator.disabledSubmitButton.Count > 0) 
                {
                    // applying logic to find the lowest fare
                    // both onwards and return flights are sorted in order by default
                    // click on each of the return flight for every onward flight 
                    // to check if any makes a valid combination
                    // once the first valid combination is selected the loop breaks and submit button is clicked
                    for (int i = 0; i < _locator.returnFlightList.Count; i++)
                    {
                        if (i < _locator.returnFlightList.Count - 1)
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(false);", _locator.returnFlightList[(i + 1)]);
                        _locator.returnFlightList[i].FindElement(By.CssSelector("span.splitVw-outer")).Click();

                        for (int j = 0; j < _locator.onwardsFlightList.Count; j++)
                        {
                            if (j < _locator.onwardsFlightList.Count - 1)
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(false);", _locator.onwardsFlightList[(j + 1)]);
                            Thread.Sleep(1000);
                            _locator.onwardsFlightList[j].FindElement(By.CssSelector("span.splitVw-outer")).Click();
                            Thread.Sleep(1000);
                            if (driver.FindElements(By.CssSelector("button.fli_primary_btn.disabled")).Count == 0)
                            {
                                flightSelected = true;
                                break;
                            }
                        }
                        if (flightSelected)
                            break;
                    }
                }
                _locator.submitButton.Click();
            }
        }

        /// <summary>
        /// function to validate each cities in the provided excel with that avaiable in website
        /// </summary>
        public void excelValidator()
        {
            // get the excel file ready for processing
            string filePath = _configuration.inputExcelFilePath;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath);
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //get the used range
            Excel.Range xlRange = xlWorkSheet.UsedRange;
            int totalRows = xlRange.Rows.Count;
            int totalColumns = xlRange.Columns.Count;

            //use exception handlers to make sure the file is closed in case of any exception
            try
            {
                // iterating through each "places" in excel
                for (int rowCount = 2; rowCount <= totalRows; rowCount++)
                {
                    String currentPlace = Convert.ToString((xlRange.Cells[rowCount, 1] as Excel.Range).Text);
                    // flags to mark if the place is found in the website
                    bool placeFoundinFromList = false, placeFoundinToList = false;

                    // logic to check webite for the list of places associated to "from"
                    // get all the elements in the "FROM" dropdown/combobox/autosuggest tool as list
                    // check which of places in excel is listed in the list
                    // add the corresponding response in the excel
                    _locator.fromCity.Click();
                    Thread.Sleep(2000);
                    if (_locator.autoSuggestFromRecentList.Count > 0) // check if the place is present in "Recent List" in website
                    {
                        placeFoundinFromList = _locator.autoSuggestFromRecentList.Any(x => x.FindElement(By.CssSelector("div>div>p")).Text.Contains(currentPlace));
                    }
                    if (!placeFoundinFromList && _locator.autoSuggestFromPopularCities.Count > 0) // check if the place is present in "Popular List" in website
                    {
                        placeFoundinFromList = _locator.autoSuggestFromPopularCities.Any(x => x.FindElement(By.CssSelector("div>div>p")).Text.Contains(currentPlace));
                    }

                    if (placeFoundinFromList)
                    {
                        xlRange.Cells[rowCount, 2] = "Pass";
                    }
                    else
                    {
                        xlRange.Cells[rowCount, 2] = "Fail";
                    }

                    // applying the same login to places associated with "TO" in the 
                    //website with regrard to that of excel sheet
                    _locator.toCity.Click();

                    if (_locator.autoSuggestToRecentList.Count > 0) // check if the place is present in "Recent List" in website
                    {
                        placeFoundinToList = _locator.autoSuggestToRecentList.Any(x => x.FindElement(By.CssSelector("div>div>p")).Text.Contains(currentPlace));
                    }

                    if (!placeFoundinToList && _locator.autoSuggestToPopularCities.Count > 0) // check if the place is present in "Popular List" in website
                    {
                        placeFoundinToList = _locator.autoSuggestToPopularCities.Any(x => x.FindElement(By.CssSelector("div>div>p")).Text.Contains(currentPlace));
                    }
                    if (placeFoundinToList)
                    {
                        xlRange.Cells[rowCount, 3] = "Pass";
                    }
                    else
                    {
                        xlRange.Cells[rowCount, 3] = "Fail";
                    }
                }
                xlWorkBook.Save();
                xlWorkBook.Close();
            }
            catch (Exception e)
            {
                xlWorkBook.Close();
            }
        }
    }
}

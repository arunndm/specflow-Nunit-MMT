
namespace MMT.Configurations
{
    public class configuration
    {
        /// <summary>
        /// Define the implicit wait time in SECONDS
        /// </summary>
        public int implicitWaitTime = 25;

        /// <summary>
        /// Define the page load time in SECONDS
        /// </summary>
        public int pageLoadTime = 45;

        /// <summary>
        /// Define the location of input excel file
        /// </summary>
        public string inputExcelFilePath = @"D:\test\Places.xlsx";

        /// <summary>
        /// Define the location of chromedriver
        /// </summary>
        public string chromedriverLocation = @"D:\test";
    }
}

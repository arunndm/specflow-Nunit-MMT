Feature: Multicity Flight Search Testcases
	To Perform Flight Search with Multicity and validate different testcases

@flightsearch @multicity
Scenario: Validate Display Order for Multicity Flight Search
	Given makemytrip website is loaded
	And I click on the MULTICITY
	When I enter "Coimbatore" in FIRST FROM of MULTICITY
	And I enter "Bengaluru" in FIRST TO of MULTICITY
	And I add "1" days from current day to FIRST DEPARTURE field
	And I enter "Chennai" in SECOND FROM of MULTICITY
	And I enter "Trivandrum" in SECOND TO of MULTICITY
	And I add "2" days from current day to SECOND DEPARTURE field
	And I click search button
	Then the result to be displayed
	When I sort the flight with ascending order of price
	Then the price to be sorted in ascending order

@surcharge @multicity
Scenario: Validate Lowest Surcharge for Multicity Flight Search
	Given makemytrip website is loaded
	And I click on the MULTICITY
	When I enter "Coimbatore" in FIRST FROM of MULTICITY
	And I enter "Bengaluru" in FIRST TO of MULTICITY
	And I add "1" days from current day to FIRST DEPARTURE field
	And I enter "Chennai" in SECOND FROM of MULTICITY
	And I enter "Trivandrum" in SECOND TO of MULTICITY
	And I add "2" days from current day to SECOND DEPARTURE field
	And I click search button
	Then the result to be displayed
	And I select the flight with lowest surcharge
	And I print the itenary of the selected flight
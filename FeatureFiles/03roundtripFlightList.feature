Feature: Roundtrip Flight Search Testcases
	To Perform Flight Search with Roundtrip and validate different testcases

@roundtrip @lowestfare
Scenario: Validate Lowest Fare For Round Trip Flight Search
	Given makemytrip website is loaded
	And I click on the ROUNDTRIP
	When I enter "Coimbatore" in FROM of ROUNDTRIP
	And I enter "Chennai" in TO of ROUNDTRIP
	And I add "4" days from current day to THE DEPARTURE field
	And I add "4" days from current day to THE RETURN field
	And I add "2" count in adults "1" count in children "1" count in infant
	And I click search button
	Then the roundtrip result to be displayed
	And I select the flight combination with lowest fare
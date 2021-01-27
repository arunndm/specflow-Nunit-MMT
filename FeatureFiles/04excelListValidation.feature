Feature: Validation
	To Perform Assertion and Update the corresponding values in Excel
@mytag
Scenario: Validate the Autosuggest with Excel
	Given makemytrip website is loaded
	Then I validate each cities in provided Excel with that available in website
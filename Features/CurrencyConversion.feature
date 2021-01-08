Feature: Test Currency Conversion API
	In order to verify currency API works fine
	We want to input *toCurrency*, *fromCurrency* and *amount* to be converted 

@VerifyThatGivenTwoCurrenciesAreConvertedAsPerGivenAmount
Scenario: To Verify that Given two currencies and decimal amount converts successfully 
	Given API Initialization for fixer Currency Conversion API
	And I have following data
	|	from	|	to	|	amount	|
	|	USD		|	PKR	|	16.98	|
	|	EUR		|	INR	|	100.09	|
	When Currency Conversion API is Invoked
	Then the response code should be 200


@VerifyThatGivenTwoCurrenciesAreConvertedOnGivenAmount
Scenario: To Verify that Given two currencies converts successfully 
	Given API Initialization for fixer Currency Conversion API
	And I have following data for POST Request
	|	from	|	to	|	amount	|
	|	NOK		|	PKR	|	10.5	|
	|	DKK		|	SEK	|	44.5	|
	When POST API is invoked
	Then the response code should be 200


@TestPOSTReqestScenarioOutline
Scenario Outline: Verify Currency Conversion POST API works fine Scenario Outline
	Given API Initialization for fixer Currency Conversion API
	And I have this data for POST Request <from>, <to> and <amount>
	When POST API is invoked
	Then the response code should be 200

	Examples: 
		|	from	|	to	|	amount	|
		|	NOK		|	PKR	|	10.5	|
		|	DKK		|	SEK	|	44.5	|
		|	PKR		|	INR	|	90.8	|


Feature: Test Currency Conversion API
	In order to verify currency API works fine
	We want to input *toCurrency*, *fromCurrency* and *amount* to be converted 

@VerifyThatGivenTwoCurrenciesAreConvertedAsPerGivenCurrencyAmount
Scenario: To Verify that Given two currencies and amount converts successfully 
	Given I have Initialized API Service call for fixer Currency Conversion API
	And I want to convert 100.5 SEK to NOK
	When Currency Conversion API is Invoked for given data
	Then Verify that the response after conversion is valid


@VerifyThatGivenTwoCurrenciesAreConvertedAsPerGivenAmountGETRequest
Scenario Outline: To Verify that Given two currencies and amount converts successfully with data from fixer
	Given I have Initialized API Service call for fixer Currency Conversion API
	And I want to convert <amount> <fromCurrency> to <toCurrency>
	When Currency Conversion API is Invoked for given data
	Then Verify that the response after conversion is valid
	Examples:
		| amount | fromCurrency | toCurrency |
		| 1587   | USD          | PKR        |
		| 169000 | NOK			| EUR        |
		| 89000  | NOK			| SEK		 |

@VerifyThatGivenTwoCurrenciesAreConvertedAsPerGivenAmountPOSTRequest
Scenario Outline: To Verify Given two currencies and amount converts successfully 
	Given I have Initialized API Service call for fixer Currency Conversion API
	And I want to convert <amount> <fromCurrency> <toCurrency>
	When When I invoke Service to convert given amount between two currencies
	Then Verify that the responseCode is <responseStatus>
	Examples: 
		| fromCurrency	| toCurrency	| amount	| responseStatus |
		| NOK			| PKR			| 10.5		| OK             |
		| DKK			| SEK			| 44.5		| OK             |
		| CCCZ			| SEK			| 20.0		| BadRequest     |
		| NOK			| BBCC			| 22.7		| BadRequest     |
		| NOK			| INR			| 0			| BadRequest     |


@VerifyThatGivenTwoCurrenciesAreConvertedOnGivenAmount
Scenario: To Verify that Given two currencies converts successfully 
	Given I have Initialized API Service call for fixer Currency Conversion API
	And I wnat to current following currencies and amount
		|	from	|	to	|	amount	|
		|	NOK		|	PKR	|	10.5	|
	When When I invoke Service to convert given amount between two currencies
	Then Verify that the responseCode is OK

# Some Negative Scenarios
@VerifyThatGivenTwoInvalidCurrenciesThrowErrorInGETRequest
Scenario Outline: To Verify that Given two Invalid currenciesThrows error 
	Given I have Initialized API Service call for fixer Currency Conversion API
	And I want to convert <amount> <fromCurrency> to <toCurrency>
	When Currency Conversion API is Invoked for given data
	Then Verify that the responseCode is <responseStatus>
	Examples:
		| amount | fromCurrency | toCurrency | responseStatus	|
		| 1587   | Dummy        | AAA        | BadRequest		|


#without API Key
@VerifyThatCallingCurrencyConversionAPIWithoutAPIKeyThrowsUNAUTHORIZEDErrorCode
Scenario Outline: To Verify that any request with invalid API key in header will throw 401 UnAuthorized Error respose from server
	Given API Initialization without api key for fixer Currency Conversion API
	And I want to convert <amount> <fromCurrency> to <toCurrency>
	When Currency Conversion API is Invoked for given data
	Then Verify that the responseCode is <responseStatus>

	Examples:
		| amount | fromCurrency | toCurrency | responseStatus |
		| 1587   | NOK			| AED        | Unauthorized   |

#using HttpClient
@VerifyThatGivenTwoCurrenciesAreConvertedAsPerGivenAmountAndCurrenciesAmountCore
Scenario: To Verify that Given two currencies and amount are exchanged successfully 
	Given I have Initialized API Service call for fixer Currency Conversion APIs
	And I want to convert 100.5 SEK PKR
	When I call Currency Conversion API on above given data
	Then Verify that the response after conversion contains success as true
	And Verify that response after conversion contains valid converted result

@VerifyThatGivenTwoCurrenciesAreConvertedIntoGivenAmountPOSTRequest
Scenario Outline: To Verify Given two currencies amount converts successfully 
	Given I have Initialized API Service call for fixer Currency Conversion APIs
	And I want to convert <amount> <fromCurrency> <toCurrency>
	When I Convert above given currencies data
	Then Verify that the response after conversion contains success as true
	Examples: 
		| fromCurrency	| toCurrency	| amount	| responseStatus |
		| NOK			| PKR			| 10.5		| OK             |
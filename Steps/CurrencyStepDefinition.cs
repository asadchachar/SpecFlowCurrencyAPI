using NUnit.Framework;
using RestSharp;
using SpecFlowCurrencyAPI.API;
using SpecFlowCurrencyAPI.Config;
using SpecFlowCurrencyAPI.Model;
using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowCurrencyAPI.Steps
{
    [Binding]
    public sealed class CurrencyStepDefinition
    {
        private RestClient RestClient;
        private IRestResponse Response;
        private CurrencyRequest CurrencyRequest;

        private HttpClient HttpClient;
        private HttpResponseMessage ResponseMessage;

        private APIAdapter APIAdapter = new APIAdapter();
        /*
         * API Initialization
         */
        [Given(@"I have Initialized API Service call for fixer Currency Conversion API")]
        public void GivenIHaveFixerAPI()
        {
            RestClient = new RestClient(APIConfig.BaseUrl);
            RestClient.AddDefaultHeader("api-key", APIConfig.ApiKey);
        }
        [Given(@"API Initialization without api key for fixer Currency Conversion API")]
        public void GivenAPIInitializationWithoutApiKeyForFixerCurrencyConversionAPI()
        {
            RestClient = new RestClient(APIConfig.BaseUrl);
            //Note that this time we are skipping api key for authorization 
        }

        [Given(@"I have Initialized API Service call for fixer Currency Conversion APIs")]
        public void GivenIHaveInitializedAPIServiceCallForFixerCurrencyConversionAPIViaCoreHTTPClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(APIConfig.BaseUrl);
            HttpClient.DefaultRequestHeaders.Add("api-key", APIConfig.ApiKey);
        }


        /*
         * Request paramter Initializations 
         */
        [Given(@"I wnat to current following currencies and amount")]
        public void GivenIHaveFollowingDataForPOST(Table table)
        {
            IEnumerable data = table.CreateSet<CurrencyRequest>();

            foreach(CurrencyRequest c in data)
            {
                CurrencyRequest = new CurrencyRequest { from = c.from, to = c.to, amount = c.amount };
                break;
            }

        }

        [Given(@"I want to convert (.*) (.*) to (.*)")]
        public void GivenIWantToConvertTo(Decimal amount, string fromCurrency, string toCurrency)
        {
            RestClient.AddDefaultQueryParameter("from", fromCurrency);
            RestClient.AddDefaultQueryParameter("to", toCurrency);
            RestClient.AddDefaultQueryParameter("amount", amount.ToString());

        }

        [Given(@"I want to convert (.*) (.*) (.*)")]
        public void GivenIWantToConvert(double Amount, string FromCurrency, string ToCurrency)
        {
            CurrencyRequest = new CurrencyRequest { amount = Amount, from = FromCurrency, to = ToCurrency };
        }

        /*
         * Invoke API Service
         */
        [When(@"Currency Conversion API is Invoked for given data")]
        public void WhenCurrencyConversionAPIIsInvokedForGivenData()
        {
            Response = APIAdapter.callCurrencyConversionGETAPI(RestClient);
        }

        [When(@"When I invoke Service to convert given amount between two currencies")]
        public void WhenPOSTAPIIsInvokedWithCurrencyData()
        {
            Response = APIAdapter.callCurrencyConversionPOSTAPI(RestClient, CurrencyRequest);
        }

        [When(@"I call Currency Conversion API on above given data")]
        public async System.Threading.Tasks.Task WhenICallCurrencyConversionAPIOnAboveGivenDataAsync()
        {
            ResponseMessage = await APIAdapter.callCurrencyConversionGETAPI(HttpClient, CurrencyRequest);
        }

        /*
         * Verification
        */
        [Then(@"Verify that the response after conversion is valid")]
        public void ThenVerifyThatTheResponseIsValid()
        {
            Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(Response.ContentType, Is.EqualTo("application/json"));
            Assert.That(Response.Content.Contains("success"));
        }

        [Then(@"Verify that the responseCode is (.*)")]
        public void ThenVerifyThatTheResponseCodeIs(HttpStatusCode responseStatus)
        {
            Assert.That(Response.StatusCode, Is.EqualTo(responseStatus));
        }

        [Then(@"Verify that the response after conversion contains success as (.*)")]
        public async System.Threading.Tasks.Task ThenVerifyThatTheResponseAfterConversionContainsSuccessAsTrueAsync(Boolean SuccessFlag)
        {
            CurrencyResponse res = await ResponseMessage.Content.ReadAsAsync<CurrencyResponse>();
            Assert.That(res.success, Is.EqualTo(SuccessFlag));
        }

        [Then(@"Verify that response after conversion contains valid converted result")]
        public async void ThenVerifyThatResponseAfterConversionContainsValidConvertedResult()
        {
            CurrencyResponse res = await ResponseMessage.Content.ReadAsAsync<CurrencyResponse>();
            Assert.That(res.rate, Is.Not.Null);
            Assert.That(res.convertResult, Is.Not.Null);
        }


    }
}

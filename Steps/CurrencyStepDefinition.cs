using NUnit.Framework;
using RestSharp;
using SpecFlowCurrencyAPI.Config;
using SpecFlowCurrencyAPI.Model;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace NUnitTestProject1.Steps
{
    [Binding]
    public sealed class CurrencyStepDefinition
    {
        private RestClient RestClient;
        private List<IRestResponse> Responses;
        private List<CurrencyRequest> Requests;
        private IEnumerable TableData;

        [Given(@"API Initialization for fixer Currency Conversion API")]
        public void GivenIHaveFixerAPI()
        {
            Responses = new List<IRestResponse>();
            Requests = new List<CurrencyRequest>();

            RestClient = new RestClient(APIConfig.BaseUrl);
            RestClient.AddDefaultHeader("api-key", APIConfig.ApiKey);
        }

        [Given(@"I have following data")]
        public void GivenIHaveFollowingData(Table table)
        {
            TableData = table.CreateSet<CurrencyRequest>();
        }

        [When(@"Currency Conversion API is Invoked")]
        public void WhenAPIIsInvoked()
        {
            RestRequest restRequest = new RestRequest("/convert", Method.GET);

            foreach(CurrencyRequest req in TableData ) {
                RestClient.AddDefaultQueryParameter("from", req.from);
                RestClient.AddDefaultQueryParameter("to", req.to);
                RestClient.AddDefaultQueryParameter("amount", req.amount.ToString());
                Responses.Add(RestClient.Execute(restRequest));
            }

        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int p0)
        {
            foreach(IRestResponse response in Responses)
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.ContentType, Is.EqualTo("application/json"));
                Assert.That(response.Content.Contains("success"));
                Assert.That(response.Content.Contains("true"));
                Assert.That(response.Content.Contains("convertResult"));
                Assert.That(response.Content.Contains("rate"));
            }
        }

        /*
         * POST Request for currency Conversion
         */
        [Given(@"I have following data for POST Request")]
        public void GivenIHaveFollowingDataForPOST(Table table)
        {
            TableData = table.CreateSet<CurrencyRequest>();
            foreach(CurrencyRequest req in TableData)
            {
                Requests.Add(new CurrencyRequest { from = req.from, to = req.to, amount = req.amount });
            }

        }

        [When(@"POST API is invoked")]
        public void WhenPOSTAPIIsInvoked()
        {
            foreach (CurrencyRequest req in Requests)
            {
                RestRequest request = new RestRequest("/convert/currency", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(req);

                Responses.Add(RestClient.Execute(request));
            }

        }
        [Given(@"I have this data for POST Request (.*), (.*) and (.*)")]
        public void GivenIHaveThusDataForPOSTRequestAnd(string from, string to, double amount)
        {
            Requests.Add(new CurrencyRequest { from = from, to = to, amount = amount });
        }

    }
}

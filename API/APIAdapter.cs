using RestSharp;
using SpecFlowCurrencyAPI.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SpecFlowCurrencyAPI.API
{
    class APIAdapter
    {
        public async System.Threading.Tasks.Task<HttpResponseMessage> callCurrencyConversionGETAPI(HttpClient HttpClient, CurrencyRequest CurrencyRequest)
        {
            return await HttpClient.GetAsync("/fixer/convert?" +
                "from=" + CurrencyRequest.from
                + "&to=" + CurrencyRequest.to
                + "&amount=" + CurrencyRequest.amount);
            
        }
        public IRestResponse callCurrencyConversionGETAPI(RestClient rc)
        {
            RestRequest restRequest = new RestRequest("/convert", Method.GET);
            return rc.Execute(restRequest);
        }
  
        public IRestResponse callCurrencyConversionPOSTAPI(RestClient RestClient, CurrencyRequest request)
        {
            RestRequest restRequest = new RestRequest("/convert/currency", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(request);
            return RestClient.Execute(restRequest);
        }
    }
}

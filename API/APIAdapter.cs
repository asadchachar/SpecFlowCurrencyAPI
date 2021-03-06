﻿using Newtonsoft.Json;
using RestSharp;
using SpecFlowCurrencyAPI.Config;
using SpecFlowCurrencyAPI.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SpecFlowCurrencyAPI.API
{
    class APIAdapter
    {
        private string relativePath_GET = APIConfig.relativePath_GET;
        private string relativePath_POST = APIConfig.relativePath_POST;

        // for dot net core API Test
        // private string relativePath_GET = "/currency";
        // private string relativePath_POST = "/currency";

        public async System.Threading.Tasks.Task<HttpResponseMessage> callCurrencyConversionGETAPI(HttpClient HttpClient, CurrencyRequest CurrencyRequest)
        {
            return await HttpClient.GetAsync(relativePath_GET + "?" +
                "from=" + CurrencyRequest.from
                + "&to=" + CurrencyRequest.to
                + "&amount=" + CurrencyRequest.amount);
            
        }
        public async System.Threading.Tasks.Task<HttpResponseMessage> callCurrencyConversionsPOSTAPI(HttpClient HttpClient, CurrencyRequest CurrencyRequest)
        {
            var CurrencyRequestJSON = JsonConvert.SerializeObject(CurrencyRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(CurrencyRequestJSON);
            var RequestByteArray = new ByteArrayContent(buffer);
            RequestByteArray.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await HttpClient.PostAsync(relativePath_POST, RequestByteArray);
        }
        public IRestResponse callCurrencyConversionGETAPI(RestClient rc)
        {
            RestRequest restRequest = new RestRequest(relativePath_GET, Method.GET);
            return rc.Execute(restRequest);
        }
  
        public IRestResponse callCurrencyConversionPOSTAPI(RestClient RestClient, CurrencyRequest request)
        {
            RestRequest restRequest = new RestRequest(relativePath_POST, Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(request);
            return RestClient.Execute(restRequest);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpecFlowCurrencyAPI.Model
{
    class CurrencyResponse
    {
        public Boolean success { set; get; }
        public string from { set; get; }
        public string to { set; get; }
        public Double amount { set; get; }
        public Double rate { set; get; }
        public Double convertResult { set; get; }

    }
}

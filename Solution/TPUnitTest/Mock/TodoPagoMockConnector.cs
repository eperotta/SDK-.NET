using System;
using System.Collections.Generic;
using TodoPagoConnector;

namespace TPUnitTest.Mock
{
    public class TodoPagoMockConnector : TodoPago
    {
        private string requestResponse;

        public TodoPagoMockConnector(string endpoint, Dictionary<string, string> headders) : base(endpoint, headders)
        {
            this.requestResponse = String.Empty;
        }

        public void SetRequestResponse(string response)
        {
            this.requestResponse = response;
        }

        protected override string ExecuteRequest(Dictionary<string, string> param, string url, string method, bool withApiKey)
        {
            return requestResponse;
        }
    }
}

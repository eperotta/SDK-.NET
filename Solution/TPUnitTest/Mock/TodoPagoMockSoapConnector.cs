using System.Collections.Generic;
using TodoPagoConnector;

namespace TPUnitTest.Mock
{
    internal class TodoPagoMockSoapConnector : SoapConnector
    {
        private Dictionary<string, object> r;

        public TodoPagoMockSoapConnector(string soapEndpoint, Dictionary<string, string> headers) : base(soapEndpoint, headers)
        {
        }

        public void SetRequestResponse(Dictionary<string, object> response)
        {
            this.r = response;
        }

        protected override Dictionary<string, object> ExecuteSendAuthorizeRequest(Dictionary<string, string> request, string payloadTAG)
        {
            return r;
        }

        protected override Dictionary<string, object> ExecuteGetAuthorizeAnswer(Dictionary<string, string> request)
        {
            return r;
        }
    }
}

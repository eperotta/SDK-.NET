using System.Collections.Generic;
using TodoPagoConnector;

namespace TPUnitTest.Mock
{
    public class TPConnectorMock : TPConnector
    {
        public TPConnectorMock(int endpoint, Dictionary<string, string> headers, SoapConnector soapClient) : base (endpoint, headers)
        {
            this.soapClient = soapClient;
        }

        public TPConnectorMock(int endpoint, Dictionary<string, string> headers, TodoPago todoPagoClient) : base(endpoint, headers)
        {
            this.todoPagoClient = todoPagoClient;
        }
    }
}

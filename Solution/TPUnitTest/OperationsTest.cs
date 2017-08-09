using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagoConnector;
using TPUnitTest.Mock;
using TPUnitTest.Mock.Data;
using TodoPagoConnector.Utils;
using TodoPagoConnector.Exceptions;

namespace TPUnitTest
{
    [TestClass]
    public class OperationsTest
    {
        private string getStatusOperationId;
        private string getStatusMerchant;

        [TestInitialize]
        public void initialize()
        {
            getStatusOperationId = "185";
            getStatusMerchant = "41702";
        }

        [TestMethod]
        public void GetStatusOKTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(OperationsDataProvider.GetStatusOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            List<Dictionary<string, object>> response = connector.GetStatus(getStatusMerchant, getStatusOperationId);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreNotEqual(null, response[0]);

            Assert.AreEqual(true, response[0].ContainsKey("Operations"));

            Assert.AreEqual(true, ((Dictionary<string, object>) response[0]["Operations"]).ContainsKey("RESULTCODE"));

            Assert.AreEqual("-1", (string)((Dictionary<string, object>)response[0]["Operations"])["RESULTCODE"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ResponseException), "<OperationsColections xmlns=\"http://api.todopago.com.ar\" ></OperationsColections>")]
        public void GetStatusFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(OperationsDataProvider.GetStatusFailResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            List<Dictionary<string, object>> response = connector.GetStatus(getStatusMerchant, getStatusOperationId);
        }

        [TestMethod]
        public void GetByRangeDateTimeOkTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(OperationsDataProvider.GetByRangeDateTimeOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, string> gbrdt = new Dictionary<string, string>();

            gbrdt.Add(ElementNames.MERCHANT, "2658");
            gbrdt.Add(ElementNames.STARTDATE, "2017-05-07");
            gbrdt.Add(ElementNames.ENDDATE, "2017-05-09");
            gbrdt.Add(ElementNames.PAGENUMBER, "1");

            Dictionary<string, object> response = connector.getByRangeDateTime(gbrdt);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("OperationsColections"));

            Assert.AreEqual(true, ((Dictionary<string, object>)response["OperationsColections"]).Count > 0);
        }
    }
}

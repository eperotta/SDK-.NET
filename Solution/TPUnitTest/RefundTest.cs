using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagoConnector;
using TPUnitTest.Mock;
using TPUnitTest.Mock.Data;
using TodoPagoConnector.Utils;

namespace TPUnitTest
{
    [TestClass]
    public class RefundTest
    {
        [TestMethod]
        public void RefundRequestOkTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(RefundDataProvider.ReturnRequestOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, string> gbrdt = new Dictionary<string, string>();

            gbrdt.Add(ElementNames.MERCHANT, "2669");
            gbrdt.Add(ElementNames.SECURITY, "108fc2b7c8a640f2bdd3ed505817ffde");
            gbrdt.Add(ElementNames.REQUESTKEY, "0d801e1c-e6b1-672c-b717-5ddbe5ab97d6");
            gbrdt.Add(ElementNames.AMOUNT, "1");

            Dictionary<string, object> response = connector.ReturnRequest(gbrdt);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("ReturnResponse"));

            Dictionary<string, object> returnResponse = (Dictionary<string, object>)response["ReturnResponse"];

            Assert.AreEqual(true, returnResponse.Count > 0);

            Assert.AreEqual(true, returnResponse.ContainsKey("StatusCode"));
            Assert.AreEqual(true, returnResponse.ContainsKey("StatusMessage"));

            Assert.AreEqual(2011, (long)returnResponse["StatusCode"]);
        }

        [TestMethod]
        public void RefundRequestFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(RefundDataProvider.ReturnRequestFailResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, string> gbrdt = new Dictionary<string, string>();

            gbrdt.Add(ElementNames.MERCHANT, "2669");
            gbrdt.Add(ElementNames.SECURITY, "108fc2b7c8a640f2bdd3ed505817ffde");
            gbrdt.Add(ElementNames.REQUESTKEY, "0d801e1c-e6b1-672c-b717-5ddbe5ab97d6");
            gbrdt.Add(ElementNames.AMOUNT, "1");

            Dictionary<string, object> response = connector.ReturnRequest(gbrdt);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("ReturnResponse"));

            Dictionary<string, object> returnResponse = (Dictionary<string, object>)response["ReturnResponse"];

            Assert.AreEqual(true, returnResponse.Count > 0);

            Assert.AreEqual(true, returnResponse.ContainsKey("StatusCode"));
            Assert.AreEqual(true, returnResponse.ContainsKey("StatusMessage"));

            Assert.AreNotEqual(2011, (long)returnResponse["StatusCode"]);
        }

        [TestMethod]
        public void RefundRequest702Test()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(RefundDataProvider.ReturnRequest702Response());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, string> gbrdt = new Dictionary<string, string>();

            gbrdt.Add(ElementNames.MERCHANT, "2669");
            gbrdt.Add(ElementNames.SECURITY, "108fc2b7c8a640f2bdd3ed505817ffde");
            gbrdt.Add(ElementNames.REQUESTKEY, "0d801e1c-e6b1-672c-b717-5ddbe5ab97d6");
            gbrdt.Add(ElementNames.AMOUNT, "1");

            Dictionary<string, object> response = connector.ReturnRequest(gbrdt);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("ReturnResponse"));

            Dictionary<string, object> returnResponse = (Dictionary<string, object>)response["ReturnResponse"];

            Assert.AreEqual(true, returnResponse.Count > 0);

            Assert.AreEqual(true, returnResponse.ContainsKey("StatusCode"));
            Assert.AreEqual(true, returnResponse.ContainsKey("StatusMessage"));

            Assert.AreNotEqual(2011, (long)returnResponse["StatusCode"]);
            Assert.AreEqual(702, (long)returnResponse["StatusCode"]);
        }

        [TestMethod]
        public void VoidRequestOkTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(RefundDataProvider.VoidRequestOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, string> gbrdt = new Dictionary<string, string>();

            gbrdt.Add(ElementNames.MERCHANT, "2669");
            gbrdt.Add(ElementNames.SECURITY, "108fc2b7c8a640f2bdd3ed505817ffde");
            gbrdt.Add(ElementNames.REQUESTKEY, "0d801e1c-e6b1-672c-b717-5ddbe5ab97d6");

            Dictionary<string, object> response = connector.VoidRequest(gbrdt);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("VoidResponse"));

            Dictionary<string, object> voidResponse = (Dictionary<string, object>)response["VoidResponse"];

            Assert.AreEqual(true, voidResponse.Count > 0);

            Assert.AreEqual(true, voidResponse.ContainsKey("StatusCode"));
            Assert.AreEqual(true, voidResponse.ContainsKey("StatusMessage"));

            Assert.AreEqual(2011, (long)voidResponse["StatusCode"]);
        }

        [TestMethod]
        public void VoidRequestFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(RefundDataProvider.VoidRequestFailResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, string> gbrdt = new Dictionary<string, string>();

            gbrdt.Add(ElementNames.MERCHANT, "2669");
            gbrdt.Add(ElementNames.SECURITY, "108fc2b7c8a640f2bdd3ed505817ffde");
            gbrdt.Add(ElementNames.REQUESTKEY, "0d801e1c-e6b1-672c-b717-5ddbe5ab97d6");

            Dictionary<string, object> response = connector.VoidRequest(gbrdt);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("VoidResponse"));

            Dictionary<string, object> voidResponse = (Dictionary<string, object>)response["VoidResponse"];

            Assert.AreEqual(true, voidResponse.Count > 0);

            Assert.AreEqual(true, voidResponse.ContainsKey("StatusCode"));
            Assert.AreEqual(true, voidResponse.ContainsKey("StatusMessage"));

            Assert.AreNotEqual(2011, (long)voidResponse["StatusCode"]);
        }

        [TestMethod]
        public void VoidRequest702Test()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(RefundDataProvider.VoidRequest702Response());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, string> gbrdt = new Dictionary<string, string>();

            gbrdt.Add(ElementNames.MERCHANT, "2669");
            gbrdt.Add(ElementNames.SECURITY, "108fc2b7c8a640f2bdd3ed505817ffde");
            gbrdt.Add(ElementNames.REQUESTKEY, "0d801e1c-e6b1-672c-b717-5ddbe5ab97d6");

            Dictionary<string, object> response = connector.VoidRequest(gbrdt);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("VoidResponse"));

            Dictionary<string, object> voidResponse = (Dictionary<string, object>)response["VoidResponse"];

            Assert.AreEqual(true, voidResponse.Count > 0);

            Assert.AreEqual(true, voidResponse.ContainsKey("StatusCode"));
            Assert.AreEqual(true, voidResponse.ContainsKey("StatusMessage"));

            Assert.AreNotEqual(2011, (long)voidResponse["StatusCode"]);
            Assert.AreEqual(702, (long)voidResponse["StatusCode"]);
        }
    }
}

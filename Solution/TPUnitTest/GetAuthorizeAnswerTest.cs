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
    public class GetAuthorizeAnswerTest
    {
        private Dictionary<string, string> getAuthorizeAnswerParams;

        [TestInitialize]
        public void initialize()
        {
            getAuthorizeAnswerParams = new Dictionary<string, string>();
            getAuthorizeAnswerParams.Add(ElementNames.SECURITY, "8A891C0676A25FBF052D1C2FFBC82DEE");
            getAuthorizeAnswerParams.Add(ElementNames.MERCHANT, "41702");
            getAuthorizeAnswerParams.Add(ElementNames.REQUESTKEY, "83765ffb-39c8-2cce-b0bf-a9b50f405ee3");
            getAuthorizeAnswerParams.Add(ElementNames.ANSWERKEY, "9c2ddf78-1088-b3ac-ae5a-ddd45976f77d");
        }

        [TestMethod]
        public void GetAuthorizeAnswerOKTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockSoapConnector soapConnector = new TodoPagoMockSoapConnector("https://developers.todopago.com.ar/services/t/1.1/", headers);
            soapConnector.SetRequestResponse(GetAuthorizeAnswerDataProvider.GetAuthorizeAnswerOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, soapConnector);

            Dictionary<string, object> response = connector.GetAuthorizeAnswer(getAuthorizeAnswerParams);
            
            Assert.AreEqual(true, response.ContainsKey("StatusCode"));
            Assert.AreEqual(-1, (int)response["StatusCode"]);

            Assert.AreEqual(true, response.ContainsKey("StatusMessage"));
            Assert.AreEqual(true, response.ContainsKey("AuthorizationKey"));
            Assert.AreEqual(true, response.ContainsKey("EncodingMethod"));
            Assert.AreEqual(true, response.ContainsKey("Payload"));
        }

        [TestMethod]
        public void GetAuthorizeAnswerFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockSoapConnector soapConnector = new TodoPagoMockSoapConnector("https://developers.todopago.com.ar/services/t/1.1/", headers);
            soapConnector.SetRequestResponse(GetAuthorizeAnswerDataProvider.GetAuthorizeAnswerFailResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, soapConnector);

            Dictionary<string, object> response = connector.GetAuthorizeAnswer(getAuthorizeAnswerParams);

            Assert.AreEqual(true, response.ContainsKey("StatusCode"));
            Assert.AreEqual(404, (int)response["StatusCode"]);
            Assert.AreNotEqual(-1, (int)response["StatusCode"]);

            Assert.AreEqual(true, response.ContainsKey("StatusMessage"));
            Assert.AreEqual(true, response.ContainsKey("AuthorizationKey"));
            Assert.AreEqual(true, response.ContainsKey("EncodingMethod"));
            Assert.AreEqual(true, response.ContainsKey("Payload"));
        }

        [TestMethod]
        public void GetAuthorizeAnswer702Test()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockSoapConnector soapConnector = new TodoPagoMockSoapConnector("https://developers.todopago.com.ar/services/t/1.1/", headers);
            soapConnector.SetRequestResponse(GetAuthorizeAnswerDataProvider.GetAuthorizeAnswer702Response());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, soapConnector);

            Dictionary<string, object> response = connector.GetAuthorizeAnswer(getAuthorizeAnswerParams);

            Assert.AreEqual(true, response.ContainsKey("StatusCode"));
            Assert.AreEqual(702, (int)response["StatusCode"]);
            Assert.AreNotEqual(-1, (int)response["StatusCode"]);

            Assert.AreEqual(true, response.ContainsKey("StatusMessage"));
            Assert.AreEqual(true, response.ContainsKey("AuthorizationKey"));
            Assert.AreEqual(true, response.ContainsKey("EncodingMethod"));
            Assert.AreEqual(true, response.ContainsKey("Payload"));
        }
    }
}

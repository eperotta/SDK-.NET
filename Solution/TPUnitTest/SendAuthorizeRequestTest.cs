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
    public class SendAuthorizeRequestTest
    {
        private Dictionary<string, string> sendAuthorizeRequestParams;
        private Dictionary<string, string> sendAuthorizeRequestPayload;

        [TestInitialize]
        public void initialize()
        {
            sendAuthorizeRequestParams = new Dictionary<string, string>();
            sendAuthorizeRequestPayload = new Dictionary<string, string>();

            sendAuthorizeRequestParams.Add(ElementNames.SECURITY, "d064744d44cf4985851e460e893e1b15");
            sendAuthorizeRequestParams.Add(ElementNames.SESSION, "ABCDEF-1234-12221-FDE1-00000200");
            sendAuthorizeRequestParams.Add(ElementNames.MERCHANT, "2658");
            sendAuthorizeRequestParams.Add(ElementNames.URL_OK, "http://someurl.com/ok");
            sendAuthorizeRequestParams.Add(ElementNames.URL_ERROR, "http://someurl.com/fail");
            sendAuthorizeRequestParams.Add(ElementNames.ENCODING_METHOD, "XML");

            sendAuthorizeRequestPayload.Add(ElementNames.MERCHANT, "2658");
            sendAuthorizeRequestPayload.Add(ElementNames.OPERATIONID, "50");
            sendAuthorizeRequestPayload.Add(ElementNames.CURRENCYCODE, "032");
            sendAuthorizeRequestPayload.Add(ElementNames.AMOUNT, "1.00");
            sendAuthorizeRequestPayload.Add(ElementNames.EMAILCLIENTE, "email_cliente@dominio.com");
            sendAuthorizeRequestPayload.Add(ElementNames.MAXINSTALLMENTS, "12"); //NO MANDATORIO, MAXIMA CANTIDAD DE CUOTAS, VALOR MAXIMO 12
            sendAuthorizeRequestPayload.Add(ElementNames.MININSTALLMENTS, "3"); //NO MANDATORIO, MINIMA CANTIDAD DE CUOTAS, VALOR MINIMO 1
            sendAuthorizeRequestPayload.Add(ElementNames.TIMEOUT, "300000"); //NO MANDATORIO, TIEMPO DE ESPERA DE 300000 (5 minutos) a 21600000 (6hs)

            sendAuthorizeRequestPayload.Add("CSBTCITY", "Villa General Belgrano"); //MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTCOUNTRY", "AR");//MANDATORIO. Código ISO.
            sendAuthorizeRequestPayload.Add("CSBTEMAIL", "todopago@hotmail.com"); //MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTFIRSTNAME", "Juan");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTLASTNAME", "Perez");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTPHONENUMBER", "541161988");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTPOSTALCODE", "1010");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTSTATE", "B");//MANDATORIO
            sendAuthorizeRequestPayload.Add("CSBTSTREET1", "Cerrito 740");//MANDATORIO.
                                                                          //sendAuthorizeRequestPayload.Add("CSBTSTREET2", "");//NO MANDATORIO

            sendAuthorizeRequestPayload.Add("CSBTCUSTOMERID", "453458"); //MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTIPADDRESS", "192.0.0.4"); //MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSPTCURRENCY", "ARS");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSPTGRANDTOTALAMOUNT", "1.00");//MANDATORIO.

            sendAuthorizeRequestPayload.Add("CSMDD6", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD7", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD8", ""); //NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD9", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD10", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD11", "");//NO MANDATORIO.

            //retail
            sendAuthorizeRequestPayload.Add("CSSTCITY", "Villa General Belgrano"); //MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTCOUNTRY", "AR");//MANDATORIO. Código ISO.
            sendAuthorizeRequestPayload.Add("CSSTEMAIL", "todopago@hotmail.com"); //MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTFIRSTNAME", "Juan");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTLASTNAME", "Perez");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTPHONENUMBER", "541160913988");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTPOSTALCODE", "1010");//MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTSTATE", "B");//MANDATORIO
            sendAuthorizeRequestPayload.Add("CSSTSTREET1", "Cerrito 740");//MANDATORIO.
                                                                          //sendAuthorizeRequestPayload.Add("CSSTSTREET2", "");//NO MANDATORIO.

            sendAuthorizeRequestPayload.Add("CSITPRODUCTCODE", "electronic_good#electronic_good#electronic_good#electronic_good");//CONDICIONAL
            sendAuthorizeRequestPayload.Add("CSITPRODUCTDESCRIPTION", "Prueba desde net#Prueba desde net#Prueba desde net#tttt");//CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITPRODUCTNAME", "netsdk#netsdk#netsdk#netsdk");//CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITPRODUCTSKU", "nsdk123#nsdk123#nsdk123#nsdk123");//CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITTOTALAMOUNT", "1.00#1.00#1.00#1.00");//CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITQUANTITY", "1#1#1#1");//CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITUNITPRICE", "1.00#1.00#1.00#1.00");

            sendAuthorizeRequestPayload.Add("CSMDD12", "");//NO MADATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD13", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD14", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD15", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD16", "");//NO MANDATORIO.
        }

        [TestMethod]
        public void SendAuthorizeRequestOKTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockSoapConnector soapConnector = new TodoPagoMockSoapConnector("https://developers.todopago.com.ar/services/t/1.1/", headers);
            soapConnector.SetRequestResponse(SendAuthorizeRequestDataProvider.SendAuthorizeRequestOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, soapConnector);

            Dictionary<string, object> response = connector.SendAuthorizeRequest(sendAuthorizeRequestParams, sendAuthorizeRequestPayload);

            Assert.AreEqual(true, response.ContainsKey("StatusCode"));
            Assert.AreEqual(-1, (int)response["StatusCode"]);

            Assert.AreEqual(true, response.ContainsKey("StatusMessage"));
            Assert.AreEqual(true, response.ContainsKey("URL_Request"));
            Assert.AreEqual(true, response.ContainsKey("RequestKey"));
            Assert.AreEqual(true, response.ContainsKey("PublicRequestKey"));
        }

        [TestMethod]
        public void SendAuthorizeRequestFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockSoapConnector soapConnector = new TodoPagoMockSoapConnector("https://developers.todopago.com.ar/services/t/1.1/", headers);
            soapConnector.SetRequestResponse(SendAuthorizeRequestDataProvider.SendAuthorizeRequestFailResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, soapConnector);

            Dictionary<string, object> response = connector.SendAuthorizeRequest(sendAuthorizeRequestParams, sendAuthorizeRequestPayload);

            Assert.AreEqual(true, response.ContainsKey("StatusCode"));
            Assert.AreNotEqual(-1, (int)response["StatusCode"]);
            Assert.AreEqual(98001, (int)response["StatusCode"]);

            Assert.AreEqual(true, response.ContainsKey("StatusMessage"));
            Assert.AreEqual(true, response.ContainsKey("URL_Request"));
            Assert.AreEqual(true, response.ContainsKey("RequestKey"));
            Assert.AreEqual(true, response.ContainsKey("PublicRequestKey"));
        }

        [TestMethod]
        public void SendAuthorizeRequest702Test()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockSoapConnector soapConnector = new TodoPagoMockSoapConnector("https://developers.todopago.com.ar/services/t/1.1/", headers);
            soapConnector.SetRequestResponse(SendAuthorizeRequestDataProvider.SendAuthorizeRequest702Response());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, soapConnector);

            Dictionary<string, object> response = connector.SendAuthorizeRequest(sendAuthorizeRequestParams, sendAuthorizeRequestPayload);

            Assert.AreEqual(true, response.ContainsKey("StatusCode"));
            Assert.AreNotEqual(-1, (int)response["StatusCode"]);
            Assert.AreEqual(702, (int)response["StatusCode"]);

            Assert.AreEqual(true, response.ContainsKey("StatusMessage"));
            Assert.AreEqual(true, response.ContainsKey("URL_Request"));
            Assert.AreEqual(true, response.ContainsKey("RequestKey"));
            Assert.AreEqual(true, response.ContainsKey("PublicRequestKey"));
        }
    }
}

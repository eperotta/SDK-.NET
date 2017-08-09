using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagoConnector;

namespace TPUnitTest
{
    [TestClass]
    public class GetEndpointFormTest
    {
        [TestMethod]
        public void GetEndpointFormDevTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TPConnector connector = new TPConnector(TPConnector.developerEndpoint, headers);

            string endpoint = connector.GetEndpointForm();

            Assert.AreEqual(true, !String.IsNullOrEmpty(endpoint));
            Assert.AreEqual("https://developers.todopago.com.ar/resources/TPHybridForm-v0.1.js", endpoint);
        }

        [TestMethod]
        public void GetEndpointFormProdTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            TPConnector connector = new TPConnector(TPConnector.productionEndpoint, headers);

            string endpoint = connector.GetEndpointForm();

            Assert.AreEqual(true, !String.IsNullOrEmpty(endpoint));
            Assert.AreEqual("https://forms.todopago.com.ar/resources/TPHybridForm-v0.1.js", endpoint);
        }
    }
}

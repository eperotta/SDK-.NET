using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagoConnector;
using TodoPagoConnector.Model;
using TPUnitTest.Mock;
using TodoPagoConnector.Exceptions;
using TPUnitTest.Mock.Data;

namespace TPUnitTest
{
    [TestClass]
    public class CredentialsTest
    {
        [TestMethod]
        public void GetCredentialsOKTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            User user = new User("ejemplo@mail.com", "mypassword");
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(CredentialsDataProvider.GetCredentialsOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            user = connector.getCredentials(user);
            
            Assert.AreEqual(true, !String.IsNullOrEmpty(user.getApiKey()));
            Assert.AreEqual(true, !String.IsNullOrEmpty(user.getMerchant()));
        }
        
        [TestMethod]
        [ExpectedException(typeof(EmptyFieldPassException), "Password is empty")]
        public void GetCredentialsEmptyPasswordTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            User user = new User();
            user.setUser("ejemplo@mail.com");
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(CredentialsDataProvider.GetCredentialsOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            user = connector.getCredentials(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldUserException), "User/Mail is empty")]
        public void GetCredentialsEmptyUserTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            User user = new User();
            user.setPassword("mypassword");
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(CredentialsDataProvider.GetCredentialsOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            user = connector.getCredentials(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldPassException), "User is null")]
        public void GetCredentialsUserNullTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(CredentialsDataProvider.GetCredentialsOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            connector.getCredentials(null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResponseException), "1050 - Este usuario no se encuentra registrado. Revisá la información indicada o registrate.")]
        public void GetCredentialsFailureUserTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            User user = new User("ejemplo@mail.com", "mypassword");
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(CredentialsDataProvider.GetCredentialsWrongUserResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            user = connector.getCredentials(user);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResponseException), "1055 - La contraseña ingresada es incorrecta. Revisala.")]
        public void GetCredentialsFailurePasswordTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            User user = new User("ejemplo@mail.com", "mypassword");
            
            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(CredentialsDataProvider.GetCredentialsWrongPasswordResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            user = connector.getCredentials(user);
        }

        [TestMethod]
        public void UserInstance()
        {
            User user = new User();

            user.setUser("ejemplo@mail.com");
            user.setPassword("mypassword");

            Assert.AreEqual("ejemplo@mail.com", user.getUser());
            Assert.AreEqual("mypassword", user.getPassword());
        }

        [TestMethod]
        public void UserInstanceTwo()
        {
            User user = new User("ejemplo@mail.com", "mypassword");

            Assert.AreEqual("ejemplo@mail.com", user.getUser());
            Assert.AreEqual("mypassword", user.getPassword());
        }
    }
}

using System;
using System.Collections.Generic;
using System.ServiceModel;
using TodoPagoConnector.Service_Extensions;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Utils;
using TodoPagoConnector.Operations;

/*
 * 
 * EN CASO DE REGENERAR LA CONEXION AL SERVICIO SOAP
 * MODIFICAR:
 * public AuthorizeEndpoint(string uri):base(uri+@"/services/Authorize")
 * POR:
 * public AuthorizeEndpoint(string uri):base(uri+@"/Authorize.AuthorizeHttpsSoap12Endpoint")
 * DENTRO DE "Service Bindings" -> AuthorizeBinding.cs
 * */

namespace TodoPagoConnector{

    public class TPConnector {

        string versionTodoPago = "1.4.0";
        private Dictionary<string, string> Headers;
        private const string tenant = "t/1.1";
        private const string soapAppend = "services/";
        private const string restAppend = "/api/";

        public const int developerEndpoint = 0;
        public const int productionEndpoint = 1;

        private string ep = String.Empty;
        private string t = String.Empty;
        private const string endPointDev = "https://developers.todopago.com.ar/";
        private const string endPointPrd = "https://apis.todopago.com.ar/";

        private string restEndpoint;
        private TodoPago todoPagoClient;
        private BSA bsaClient;

        private string soapEndpoint;
        private SoapConector soapClient;

        public TPConnector(int endpoint)
           : this(endpoint, null){
        }

        public TPConnector(int endpoint, Dictionary<string, string> headers){

            switch (endpoint) {
                case developerEndpoint:
                    ep = endPointDev;
                    t = tenant;
				    break;
                case productionEndpoint:
                    ep = endPointPrd;
                    t = tenant;
                    break;
            }

            this.soapEndpoint = ep + soapAppend + t;
            this.restEndpoint = ep + t + restAppend;

            if (headers != null) {
                this.Headers = headers;
            }

            createClients();
        }

        public TPConnector(string endpoint)
            : this(endpoint, null) {
        }

        public TPConnector(string endpoint, Dictionary<string, string> headers)
        {
            this.soapEndpoint = endpoint + soapAppend + tenant;
            this.restEndpoint = endpoint + tenant + restAppend;

            if (headers!= null){
              this.Headers = headers;
            }

            createClients();
        }

        private void createClients(){
            this.todoPagoClient = new TodoPago(this.restEndpoint, this.Headers);
            this.bsaClient = new BSA(this.restEndpoint, this.Headers);
            this.soapClient = new SoapConector(this.soapEndpoint, this.Headers);
        }

        public void setAuthorize(String authorization){
            var headers = new Dictionary<String, String>();

            if (authorization != null && !authorization.Equals("")) {
                headers.Add("Authorization", authorization);
                this.Headers = headers;
                createClients();
            }else{
                throw new ResponseException("ApiKey is null");
            }         
        }


        public List<Dictionary<string, object>> GetStatus(string merchant, string operation){
            return todoPagoClient.getByOperationID(merchant, operation);
        }

        public Dictionary<string, object> GetAllPaymentMethods(string merchant){
            return todoPagoClient.GetAllPaymentMethods(merchant);
        }

        public Dictionary<string, object> VoidRequest(Dictionary<string, string> param){
            return todoPagoClient.VoidRequest(param);
        }

        public Dictionary<string, object> ReturnRequest(Dictionary<string, string> param){
            return todoPagoClient.ReturnRequest(param);
        }

        public Dictionary<string, object> getByRangeDateTime(Dictionary<string, string> param){
            return todoPagoClient.GetByRangeDateTime(param);
        }

        public User getCredentials(User user)
        {
            User result = user;
            CredentialsValidate cv = new CredentialsValidate();

            if (cv.ValidateCredentials(user))
                result = todoPagoClient.getCredentials(user);

            return result;		
        }

        public Dictionary<string, object> GetAuthorizeAnswer(Dictionary<string, string> request)
        {
            return this.soapClient.GetAuthorizeAnswer(request);
        }

        public Dictionary<string, object> SendAuthorizeRequest(Dictionary<string, string> request, Dictionary<string, string> payloads)
        {
            return this.soapClient.SendAuthorizeRequest(request, payloads);
        }

        private void ChannelFactory_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ServiceModel;
using TodoPagoConnector.Service_Extensions;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Utils;

/*
 * 
 * EN CASO DE REGENERAR LA CONEXION AL SERVICIO SOAP
 * MODIFICAR:
 * public AuthorizeEndpoint(string uri):base(uri+@"/services/Authorize")
 * POR:
 * public AuthorizeEndpoint(string uri):base(uri+@"/Authorize.AuthorizeHttpsSoap12Endpoint")
 * DENTRO DE "Service Bindings" -> AuthorizeBinding.cs
 * */

namespace TodoPagoConnector
{
    public class TPConnector 
    {

        string versionTodoPago = "1.2.0";
       
        private AuthorizeBinding AuthorizeBinding;
        private AuthorizeEndpoint AuthorizeEndpoint;
        private OperationsBinding OperationsBinding;
        private OperationsEndpoint OperationsEndpoint;
        private Dictionary<string, string> Headers;
        private const string tenant = "t/1.1";
        private const string soapAppend = "services/";
        private const string restAppend = "/api/";
        private RestConnector restClient;
        private string restEndpoint;


        public TPConnector(string endpoint)
           : this(endpoint, null)
        {
        }

        public TPConnector(string endpoint, Dictionary<string, string> headers)
        {
            string sopaEndpoint = endpoint + soapAppend + tenant; 
            this.AuthorizeBinding = new AuthorizeBinding();
            this.AuthorizeEndpoint = new AuthorizeEndpoint(sopaEndpoint);

            this.OperationsBinding = new OperationsBinding();
            this.OperationsEndpoint = new OperationsEndpoint(sopaEndpoint);

            if (headers!= null)
            {
              this.Headers = headers;
            }
 
            this.restEndpoint = endpoint + tenant + restAppend;
            this.restClient = new RestConnector(restEndpoint, headers);
        }


        public List<Dictionary<string, object>> GetStatus(string merchant, string operation)
        {
            return restClient.getByOperationID(merchant, operation);
        }

        public Dictionary<string, object> GetAllPaymentMethods(string merchant)
        {
            return restClient.GetAllPaymentMethods(merchant);
        }

        public Dictionary<string, object> VoidRequest(Dictionary<string, string> param)
        {
            return restClient.VoidRequest(param);
        }

        public Dictionary<string, object> ReturnRequest(Dictionary<string, string> param)
        {
            return restClient.ReturnRequest(param);
        }

        public Dictionary<string, object> getByRangeDateTime(Dictionary<string, string> param)
        { 
            return restClient.GetByRangeDateTime(param);
        }

        public User getCredentials(User user) 
        {
            User result = user;
            if (user != null)
            {
                if (user.getUser() == null || user.getUser().Equals(""))
                {
                    throw new EmptyFieldUserException("User/Mail is empty");
                }
                if (user.getPassword() == null || user.getPassword().Equals(""))
                {
                    throw new EmptyFieldPassException("Pass is empty");
                }
                result = restClient.getCredentials(user);
            }
            else
            {
                throw new EmptyFieldPassException("User is null");
            }
            return result;		
        }

        public void setAuthorize(String authorization)
        {
            var headers = new Dictionary<String, String>();

            if (authorization != null && !authorization.Equals(""))
            {
                headers.Add("Authorization", authorization);
                this.Headers = headers;
                this.restClient = new RestConnector(restEndpoint, headers);
            }
            else
            {
                throw new ResponseException("ApiKey is null");
            }         
        }

        public Dictionary<string, object> GetAuthorizeAnswer(Dictionary<string, string> request)
        {
            var result =  new Dictionary<string, object>();

            try
            {
                using (var client = new AuthorizeService.AuthorizePortTypeClient(this.AuthorizeBinding, this.AuthorizeEndpoint))
                {
                    HeaderHttpExtension.AddCustomHeaderUserInformation(new OperationContextScope(client.InnerChannel), this.Headers);

                    string statusMessage, authorizationKey, encodingMethod;
                    object payload;

                    var statusCode = client.GetAuthorizeAnswer(
                        request[ElementNames.SECURITY],
                        request[ElementNames.SESSION],
                        request[ElementNames.MERCHANT],
                        request[ElementNames.REQUESTKEY],
                        request[ElementNames.ANSWERKEY],
                        out statusMessage,
                        out authorizationKey,
                        out encodingMethod,
                        out payload);

                    result.Add("StatusCode", statusCode);
                    result.Add("StatusMessage", statusMessage);
                    result.Add("AuthorizationKey", authorizationKey);
                    result.Add("EncodingMethod", encodingMethod);
                    result.Add("Payload", payload);
                }
            }
            catch (Exception ex)
            {
                ///TODO: ACA VA EL MANEJO DE EXCEPCIONES
                result.Add("ErrorMessage", ex.Message);
                throw ex;
            }

            return result;
        }

        public Dictionary<string, object> SendAuthorizeRequest(Dictionary<string, string> request, Dictionary<string, string> payloads)
        {

            //Add version to payload dictionary
            // string val = "";
            // if (payloads.TryGetValue("SDK", out val))
            //{
            // payloads["SDK"] = ".NET";
            //}
            // else
            //{
            //payloads.Add("SDK", ".NET");
            //}

            //if (payloads.TryGetValue("SDKVERSION", out val))
            //{
            // payloads["SDKVERSION"] = versionTodoPago;
            //}
            //else
            //{
            //payloads.Add("SDKVERSION", versionTodoPago);
            //}

            // if (payloads.TryGetValue("LENGUAGEVERSION", out val))
            //{
            //payloads["LENGUAGEVERSION"] = Environment.Version.ToString();
            //}
            //else
            //{
           // payloads.Add("LENGUAGEVERSION", Environment.Version.ToString());
            //}


            var result = new Dictionary<string, object>();

            string payloadTAG = String.Empty;
            payloadTAG = "<Request>";
            foreach (var payload in payloads.Keys)
            {
                payloadTAG += "<" + payload.ToUpper() + ">" + payloads[payload] + "</" + payload.ToUpper() + ">";
            }
            payloadTAG += "</Request>";

            try
            {

                
                using (var client = new AuthorizeService.AuthorizePortTypeClient(this.AuthorizeBinding, this.AuthorizeEndpoint))
                {
                    HeaderHttpExtension.AddCustomHeaderUserInformation(new OperationContextScope(client.InnerChannel), this.Headers);
                    

                    string statusMessage, URL_Request, RequestKey, PublicRequestKey;

                        var statusCode = client.SendAuthorizeRequest(
                            request[ElementNames.SECURITY],
                            request[ElementNames.SESSION],
                            request[ElementNames.MERCHANT],
                            request[ElementNames.URL_OK],
                            request[ElementNames.URL_ERROR],
                            request[ElementNames.ENCODING_METHOD],
                            payloadTAG,
                            out statusMessage,
                            out URL_Request,
                            out RequestKey,
                            out PublicRequestKey);

                        result.Add("StatusCode", statusCode);
                        result.Add("StatusMessage", statusMessage);
                        result.Add("URL_Request", URL_Request);
                        result.Add("RequestKey", RequestKey);
                        result.Add("PublicRequestKey", PublicRequestKey);
                    }
                }
            catch (Exception ex)
            {
                ///TODO: ACA VA EL MANEJO DE EXCEPCIONES
                result.Add("ErrorMessage", ex.Message);
                throw ex;
            }

            return result;
        }

        private void ChannelFactory_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}

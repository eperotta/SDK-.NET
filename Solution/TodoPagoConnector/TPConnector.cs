using System;
using System.Collections.Generic;
using System.ServiceModel;
using TodoPagoConnector.Service_Extensions;

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
        string versionTodoPago = "1.0.1";
        
        private AuthorizeBinding AuthorizeBinding;
        private AuthorizeEndpoint AuthorizeEndpoint;
       
        private Dictionary<string, string> Headers;

        private const string SECURITY = "Security";
        private const string SESSION = "Session";
        private const string MERCHANT = "Merchant";
        private const string REQUESTKEY = "RequestKey";
        private const string ANSWERKEY = "AnswerKey";
        private const string URL_OK = "URL OK";
        private const string URL_ERROR = "URL Error";
        private const string ENCODING_METHOD = "Encoding Method";

        private const string tenant = "t/1.1";
        private const string soapAppend = "services/";
        private const string restAppend = "/api/";

        private RestConnector restClient;


        public TPConnector(string endpoint, Dictionary<string, string> headers)
        {
            string sopaEndpoint = endpoint + soapAppend + tenant; 
            this.AuthorizeBinding = new AuthorizeBinding();
            this.AuthorizeEndpoint = new AuthorizeEndpoint(sopaEndpoint);

            this.Headers = headers;

            string restEndpoint = endpoint + tenant + restAppend;
            restClient = new RestConnector(restEndpoint, headers);

        }

        //TODO rename to GetStatus
        public List<Dictionary<string, object>> GetStatus(string merchant, string operation)
        {

            return restClient.getByOperationID(merchant, operation);

        }


        public Dictionary<string, object> GetAllPaymentMethods(string merchant)
        {

            return restClient.GetAllPaymentMethods(merchant);

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
                        request[SECURITY],
                        request[SESSION],
                        request[MERCHANT],
                        request[REQUESTKEY],
                        request[ANSWERKEY],
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
                            request[SECURITY],
                            request[SESSION],
                            request[MERCHANT],
                            request[URL_OK],
                            request[URL_ERROR],
                            request[ENCODING_METHOD],
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
    }
}

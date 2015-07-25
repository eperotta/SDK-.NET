using System;
using System.Collections.Generic;
using System.ServiceModel;
using TodoPagoConnector.Service_Extensions;

namespace TodoPagoConnector
{
    public class TPConnector
    {
        private AuthorizeBinding AuthorizeBinding;
        private AuthorizeEndpoint AuthorizeEndpoint;
        private OperationsBinding OperationsBinding;
        private OperationsEndpoint OperationsEndpoint;

        private Dictionary<string, string> Headers;

        private const string SECURITY = "Security";
        private const string SESSION = "Session";
        private const string MERCHANT = "Merchant";
        private const string REQUESTKEY = "RequestKey";
        private const string ANSWERKEY = "AnswerKey";
        private const string URL_OK = "URL OK";
        private const string URL_ERROR = "URL Error";
        private const string ENCODING_METHOD = "Encoding Method";

        public TPConnector(string endpoint, Dictionary<string, string> headers)
        {
            this.AuthorizeBinding = new AuthorizeBinding();
            this.AuthorizeEndpoint = new AuthorizeEndpoint(endpoint);

            this.OperationsBinding = new OperationsBinding();
            this.OperationsEndpoint = new OperationsEndpoint(endpoint);

            this.Headers = headers;
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


        

        public List<Dictionary<string, object>> GetStatus(string merchant, string operationID)
        {
            var result = new List<Dictionary<string, object>>();

            try
            {
                using (var client = new OperationsService.OperationsPortTypeClient(this.OperationsBinding, this.OperationsEndpoint))
                {
                    HeaderHttpExtension.AddCustomHeaderUserInformation(new OperationContextScope(client.InnerChannel), this.Headers);

                    var operations = client.GetByOperationId(merchant, operationID);

                    foreach (var operation in operations)
                    {
                        var dic = new Dictionary<string, object>();

                        foreach (var prop in operation.GetType().GetProperties())
                        {
                            dic.Add(prop.Name, prop.GetValue(operation,null));
                        }

                        result.Add(dic);
                    }
                }
            }
            catch (Exception ex)
            {
                var resultDict = new Dictionary<string, object>();
                ///TODO: ACA VA EL MANEJO DE EXCEPCIONES
                resultDict.Add("ErrorMessage", ex.Message);
                result.Add(resultDict);
                throw ex;
            }

            return result;
        }

    }
}

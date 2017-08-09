using System;
using System.Collections.Generic;
using System.ServiceModel;
using TodoPagoConnector.Service_Extensions;
using TodoPagoConnector.Utils;

namespace TodoPagoConnector
{
    public class SoapConnector
    {
        private AuthorizeBinding AuthorizeBinding;
        private AuthorizeEndpoint AuthorizeEndpoint;
        private Dictionary<string, string> Headers;

        private string soapEndpoint;

        public SoapConnector(string soapEndpoint, Dictionary<string, string> headers)
        {
            this.AuthorizeBinding = new AuthorizeBinding();
            this.soapEndpoint = soapEndpoint;
            this.AuthorizeEndpoint = new AuthorizeEndpoint(this.soapEndpoint);
            this.Headers = headers;
        }

        public Dictionary<string, object> GetAuthorizeAnswer(Dictionary<string, string> request)
        {
            var result = new Dictionary<string, object>();

            try
            {
                result = ExecuteGetAuthorizeAnswer(request);
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

            //FraudControlValidate fc = new FraudControlValidate();
            //payloads = fc.validate(payloads);

            //if (!payloads.ContainsKey(ElementNames.ERROR))
            //{

            foreach (var payload in payloads.Keys)
            {
                payloadTAG += "<" + payload.ToUpper() + ">" + payloads[payload] + "</" + payload.ToUpper() + ">";
            }

            payloadTAG += "</Request>";
            //Console.WriteLine(payloadTAG);
            try
            {
                result = ExecuteSendAuthorizeRequest(request, payloadTAG);
            }
            catch (Exception ex)
            {
                ///TODO: ACA VA EL MANEJO DE EXCEPCIONES
                result.Add("ErrorMessage", ex.Message);
                throw ex;
            }

            //}
            //else
            //{
            //    result = SetResult(payloads);
            //}

            return result;
        }

        protected virtual Dictionary<string, object> ExecuteSendAuthorizeRequest(Dictionary<string, string> request, string payloadTAG)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

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

                result.Add(ElementNames.STATUS_CODE, statusCode);
                result.Add(ElementNames.STATUS_MESSAGE, statusMessage);
                result.Add(ElementNames.URL_REQUEST, URL_Request);
                result.Add(ElementNames.REQUEST_KEY, RequestKey);
                result.Add(ElementNames.PUBLIC_REQUEST_KEY, PublicRequestKey);
            }

            return result;
        }

        protected virtual Dictionary<string, object> ExecuteGetAuthorizeAnswer(Dictionary<string, string> request)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

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

                result.Add(ElementNames.STATUS_CODE, statusCode);
                result.Add(ElementNames.STATUS_MESSAGE, statusMessage);
                result.Add(ElementNames.AUTHORIZATIONKEY, authorizationKey);
                result.Add(ElementNames.ENCODINGMETHOD, encodingMethod);
                result.Add(ElementNames.PAYLOAD, payload);
            }

            return result;
        }

        private Dictionary<string, object> SetResult(Dictionary<string, string> dic)
        {
            var result = new Dictionary<string, object>();
            var aux = new Dictionary<string, object>();

            result.Add(ElementNames.STATUS_CODE, "9999");
            result.Add(ElementNames.STATUS_MESSAGE, "Campos invalidos " + dic[ElementNames.ERROR]);
            result.Add(ElementNames.URL_REQUEST, String.Empty);
            result.Add(ElementNames.REQUEST_KEY, String.Empty);
            result.Add(ElementNames.PUBLIC_REQUEST_KEY, String.Empty);

            foreach (string k in dic.Keys)
            {
                aux.Add(k, (string)dic[k]);
            }

            result.Add("ERROR", aux);

            return result;
        }
    }
}

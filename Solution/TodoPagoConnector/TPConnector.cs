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
        string versionTodoPago = "1.0.3";
        
        private AuthorizeBinding AuthorizeBinding;
        private AuthorizeEndpoint AuthorizeEndpoint;

        private OperationsBinding OperationsBinding;
        private OperationsEndpoint OperationsEndpoint;
       
        private Dictionary<string, string> Headers;

        public const string SECURITY = "Security";
        public const string SESSION = "Session";
        public const string MERCHANT = "Merchant";
        public const string REQUESTKEY = "RequestKey";
        public const string ANSWERKEY = "AnswerKey";
        public const string URL_OK = "URL OK";
        public const string URL_ERROR = "URL Error";
        public const string ENCODING_METHOD = "Encoding Method";

        public const string STARTDATE = "StartDate";
        public const string ENDDATE = "EndDate";

        private const string tenant = "t/1.1";
        private const string soapAppend = "services/";
        private const string restAppend = "/api/";

        private RestConnector restClient;


        public TPConnector(string endpoint, Dictionary<string, string> headers)
        {
            string sopaEndpoint = endpoint + soapAppend + tenant; 
            this.AuthorizeBinding = new AuthorizeBinding();
            this.AuthorizeEndpoint = new AuthorizeEndpoint(sopaEndpoint);

            this.OperationsBinding = new OperationsBinding();
            this.OperationsEndpoint = new OperationsEndpoint(sopaEndpoint);

            this.Headers = headers;

            string restEndpoint = endpoint + tenant + restAppend;
            restClient = new RestConnector(restEndpoint, headers);

        }

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

            //Add version to payload dictionary
            string val = "";
            if (payloads.TryGetValue("SDK", out val))
            {
                payloads["SDK"] = ".NET";
            }
            else
            {
                payloads.Add("SDK", ".NET");
            }

            if (payloads.TryGetValue("SDKVERSION", out val))
            {
                payloads["SDKVERSION"] = versionTodoPago;
            }
            else
            {
                payloads.Add("SDKVERSION", versionTodoPago);
            }

            if (payloads.TryGetValue("LENGUAGEVERSION", out val))
            {
                payloads["LENGUAGEVERSION"] = Environment.Version.ToString();
            }
            else
            {
                payloads.Add("LENGUAGEVERSION", Environment.Version.ToString());
            }


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


        public Dictionary<string, object> getByRangeDateTime(Dictionary<string, object> request)
        {
            var result = new Dictionary<string, object>();

            try
            {
                using (var client = new OperationsService.OperationsPortTypeClient(this.OperationsBinding, this.OperationsEndpoint))
                {
                    HeaderHttpExtension.AddCustomHeaderUserInformation(new OperationContextScope(client.InnerChannel), this.Headers);

                    //DateTime start = new DateTime(yy, mm, day, hour, minute, second);

                    TodoPagoConnector.OperationsService.Operations[] ret = client.GetByRangeDateTime(
                        (string)request[MERCHANT],
                        (DateTime)request[STARTDATE],
                        (DateTime)request[ENDDATE]);

                    //swap from custom [] to Dictionary
                    for (int i = 0; i < ret.Length; i++)
                    {
                        Console.WriteLine(ret[i]);
                        Dictionary<string, string> tmp = new Dictionary<string, string>();

                        tmp.Add("AMOUNT", "" + ret[i].AMOUNT);
                        tmp.Add("AMOUNTBUYER", "" + ret[i].AMOUNTBUYER);
                        tmp.Add("AUTHORIZATIONCODE", "" + ret[i].AUTHORIZATIONCODE);
                        tmp.Add("BANKID", "" + ret[i].BANKID);
                        tmp.Add("BARCODE", "" + ret[i].BARCODE);
                        tmp.Add("CARDHOLDERNAME", "" + ret[i].CARDHOLDERNAME);
                        tmp.Add("CARDNUMBER", "" + ret[i].CARDNUMBER);
                        tmp.Add("COUPONEXPDATE", "" + ret[i].COUPONEXPDATE);
                        tmp.Add("COUPONSECEXPDATE", "" + ret[i].COUPONSECEXPDATE);
                        tmp.Add("COUPONSUBSCRIBER", "" + ret[i].COUPONSUBSCRIBER);
                        tmp.Add("CURRENCYCODE", "" + ret[i].CURRENCYCODE);
                        tmp.Add("CUSTOMEREMAIL", "" + ret[i].CUSTOMEREMAIL);
                        tmp.Add("DATETIME", "" + ret[i].DATETIME);
                        tmp.Add("IDENTIFICATION", "" + ret[i].IDENTIFICATION);
                        tmp.Add("IDENTIFICATIONTYPE", "" + ret[i].IDENTIFICATIONTYPE);
                        tmp.Add("INSTALLMENTPAYMENTS", "" + ret[i].INSTALLMENTPAYMENTS);
                        tmp.Add("OPERATIONID", "" + ret[i].OPERATIONID);
                        tmp.Add("PAYMENTMETHODCODE", "" + ret[i].PAYMENTMETHODCODE);
                        tmp.Add("PAYMENTMETHODNAME", "" + ret[i].PAYMENTMETHODNAME);
                        tmp.Add("PAYMENTMETHODTYPE", "" + ret[i].PAYMENTMETHODTYPE);
                        tmp.Add("PROMOTIONID", "" + ret[i].PROMOTIONID);
                        tmp.Add("REFUNDED", "" + ret[i].REFUNDED);
                        tmp.Add("RESULTCODE", "" + ret[i].RESULTCODE);
                        tmp.Add("RESULTMESSAGE", "" + ret[i].RESULTMESSAGE);
                        tmp.Add("TICKETNUMBER", "" + ret[i].TICKETNUMBER);
                        tmp.Add("TYPE", "" + ret[i].TYPE);

                        result.Add("" + ret[i].OPERATIONID, tmp);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ///TODO: ACA VA EL MANEJO DE EXCEPCIONES
                result.Add("ErrorMessage", ex.Message);
                //throw ex;
            }

            return result;
        }

        private void ChannelFactory_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}

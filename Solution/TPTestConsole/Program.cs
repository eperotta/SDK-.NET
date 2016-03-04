using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TodoPagoConnector;

namespace TPTestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Console.WriteLine("init TodoPagoConnectorSample");

            TodoPagoConnectorSample tpcs = new TodoPagoConnectorSample();

            Console.WriteLine("------------------------------------------------------------------------");

            Console.WriteLine("initSendAuthorizeRequestParams");
            tpcs.initSendAuthorizeRequestParams();
            Console.WriteLine("Call SendAuthorizeRequest");
            tpcs.sendAuthorizeRequest();

            Console.WriteLine("------------------------------------------------------------------------");

            Console.WriteLine("initGetAuthorizeAnswer");
            tpcs.initGetAuthorizeAnswer();
            Console.WriteLine("sendGetAuthorizeAnswer");
            tpcs.sendGetAuthorizeAnswer();

            Console.WriteLine("------------------------------------------------------------------------");

            Console.WriteLine("initGetStatus");
            Console.WriteLine("sendGetStatus");
            tpcs.initGetStatus();
            tpcs.sendGetStatus();
            Console.WriteLine("------------------------------------------------------------------------");

            tpcs.getAllPaymentMethods();
            Console.WriteLine("------------------------------------------------------------------------");

            Console.WriteLine("initGetByRangeDateTime");
            tpcs.getByRangeDateTime();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("VoidRequest");
            tpcs.voidRequest();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("ReturnRequest");
            tpcs.returnRequest();

            Console.Read();
        }

        private class TodoPagoConnectorSample
        {
            //Names
            private const string SECURITY = "Security";

            private const string SESSION = "Session";
            private const string MERCHANT = "Merchant";
            private const string REQUESTKEY = "RequestKey";
            private const string ANSWERKEY = "AnswerKey";
            private const string URL_OK = "URL OK";
            private const string URL_ERROR = "URL Error";
            private const string ENCODING_METHOD = "Encoding Method";
            private const string OPERATIONID = "OPERATIONID";
            private const string CURRENCYCODE = "CURRENCYCODE";
            private const string AMOUNT = "AMOUNT";
            private const string EMAILCLIENTE = "EMAILCLIENTE";

            //Connector
            private TPConnector connector;

            //Parameters
            //SendAuthorizeRequest
            private Dictionary<string, string> sendAuthorizeRequestParams = new Dictionary<string, string>();

            private Dictionary<string, string> sendAuthorizeRequestPayload = new Dictionary<string, string>();

            //GetAuthorizeAnswer
            private Dictionary<string, string> getAuthorizeAnswerParams = new Dictionary<string, string>();

            //GetStatus
            private string getStatusMerchant;

            private string getStatusOperationId;

            //Authentification and Endpoint
            private string authorization = "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3";

            //string endpoint = "https://apis.todopago.com.ar/";//produccion
            private string endpoint = "https://developers.todopago.com.ar/";//desarrollo

            //Constructor
            public TodoPagoConnectorSample()
            {
                connector = initConnector();
            }

            private TPConnector initConnector()
            {
                var headers = new Dictionary<String, String>();

                headers.Add("Authorization", authorization); 
                
                //Override SSL security - must be removed on PRD
                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);

                return new TPConnector(endpoint, headers);
            }

            public void initSendAuthorizeRequestParams()
            {
                sendAuthorizeRequestParams.Add(SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                sendAuthorizeRequestParams.Add(SESSION, "ABCDEF-1234-12221-FDE1-00000200");
                sendAuthorizeRequestParams.Add(MERCHANT, "2153");
                sendAuthorizeRequestParams.Add(URL_OK, "http://someurl.com/ok");
                sendAuthorizeRequestParams.Add(URL_ERROR, "http://someurl.com/fail");
                sendAuthorizeRequestParams.Add(ENCODING_METHOD, "XML");

                var payload = new Dictionary<string, string>();
                sendAuthorizeRequestPayload.Add(MERCHANT, "2153");
                sendAuthorizeRequestPayload.Add(OPERATIONID, "2121");
                sendAuthorizeRequestPayload.Add(CURRENCYCODE, "032");
                sendAuthorizeRequestPayload.Add(AMOUNT, "55");
                sendAuthorizeRequestPayload.Add(EMAILCLIENTE, "email_cliente@dominio.com");

                sendAuthorizeRequestPayload.Add("CSBTCITY", "Villa General Belgrano"); //MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSBTCOUNTRY", "AR");//MANDATORIO. Código ISO.
                sendAuthorizeRequestPayload.Add("CSBTEMAIL", "todopago@hotmail.com"); //MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSBTFIRSTNAME", "Juan");//MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSBTLASTNAME", "Perez");//MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSBTPHONENUMBER", "541160913988");//MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSBTPOSTALCODE", "1010");//MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSBTSTATE", "B");//MANDATORIO
                sendAuthorizeRequestPayload.Add("CSBTSTREET1", "Cerrito 740");//MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSBTSTREET2", "");//NO MANDATORIO

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
                sendAuthorizeRequestPayload.Add("CSSTSTREET2", "");//NO MANDATORIO.

                sendAuthorizeRequestPayload.Add("CSITPRODUCTCODE", "electronic_good");//CONDICIONAL
                sendAuthorizeRequestPayload.Add("CSITPRODUCTDESCRIPTION", "Prueba desde net");//CONDICIONAL.
                sendAuthorizeRequestPayload.Add("CSITPRODUCTNAME", "netsdk");//CONDICIONAL.
                sendAuthorizeRequestPayload.Add("CSITPRODUCTSKU", "nsdk123");//CONDICIONAL.
                sendAuthorizeRequestPayload.Add("CSITTOTALAMOUNT", "1.00");//CONDICIONAL.
                sendAuthorizeRequestPayload.Add("CSITQUANTITY", "1");//CONDICIONAL.
                sendAuthorizeRequestPayload.Add("CSITUNITPRICE", "1.00");

                sendAuthorizeRequestPayload.Add("CSMDD12", "");//NO MADATORIO.
                sendAuthorizeRequestPayload.Add("CSMDD13", "");//NO MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSMDD14", "");//NO MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSMDD15", "");//NO MANDATORIO.
                sendAuthorizeRequestPayload.Add("CSMDD16", "");//NO MANDATORIO.
            }

            public void sendAuthorizeRequest()
            {
                string output = "";
                try
                {
                    var res = connector.SendAuthorizeRequest(sendAuthorizeRequestParams, sendAuthorizeRequestPayload);

                    //string response = res["StatusCode"].ToString() + "-" + res["StatusMessage"].ToString();
                    //string detail = "URL_Request = " + res["URL_Request"] + "\r\nRequestKey = " + res["RequestKey"] + "\r\nPublicRequestKey = " + res["PublicRequestKey"];

                    output += "\r\n- " + res["StatusCode"].ToString();
                    output += "\r\n- " + res["StatusMessage"].ToString();

                    output += "\r\n- URL_Request = " + res["URL_Request"];
                    output += "\r\n- RequestKey = " + res["RequestKey"];
                    output += "\r\n- PublicRequestKey = " + res["PublicRequestKey"];

                    //Console.WriteLine(response);
                    //Console.WriteLine(detail);
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        WebResponse resp = ex.Response;
                        using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                        {
                            output += "\r\n" + sr.ReadToEnd() + " - " + ex.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    output += "\r\n" + ex.Message + "\r\n" + ex.InnerException.Message + "\r\n" + ex.HelpLink;
                }

                Console.WriteLine(output);
            }

            public void initGetAuthorizeAnswer()
            {
                getAuthorizeAnswerParams.Add(SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                getAuthorizeAnswerParams.Add(SESSION, null);
                getAuthorizeAnswerParams.Add(MERCHANT, "2153");
                getAuthorizeAnswerParams.Add(REQUESTKEY, "0db2e848-b0ab-6baf-f9e1-b70a82ed5844");
                getAuthorizeAnswerParams.Add(ANSWERKEY, "31b8ae04-318c-89dd-5354-494dd5fefb2f");
            }

            public void sendGetAuthorizeAnswer()
            {
                string output = "";
                try
                {
                    var res = connector.GetAuthorizeAnswer(getAuthorizeAnswerParams);

                    foreach (var key in res.Keys)
                    {
                        Console.WriteLine("- " + key + ": " + res[key]);

                        if (key.Equals("Payload"))
                        {
                            System.Xml.XmlNode[] aux = (System.Xml.XmlNode[])res["Payload"];
                            if (aux != null)
                            {
                                for (int i = 0; i < aux.Count(); i++)
                                {
                                    System.Xml.XmlNodeList inner = aux[i].ChildNodes;
                                    for (int j = 0; j < inner.Count; j++)
                                    {
                                        Console.WriteLine("     " + inner.Item(j).Name + " : " + inner.Item(j).InnerText);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        WebResponse resp = ex.Response;
                        using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                        {
                            output += "\r\n" + sr.ReadToEnd() + "\r\n" + ex.Message;
                            //Response.Write(sr.ReadToEnd());
                        }
                    }
                }
                catch (Exception ex)
                {
                    output += "\r\n" + ex.Message + "\r\n" + ex.InnerException.Message + "\r\n" + ex.HelpLink;
                }

                Console.WriteLine(output);
            }

            public void initGetStatus()
            {
                getStatusOperationId = "01";
                getStatusMerchant = "2153";
            }

            public void sendGetStatus()
            {
                List<Dictionary<string, object>> res = connector.GetStatus(getStatusMerchant, getStatusOperationId);

                for (int i = 0; i < res.Count; i++)
                {
                    Dictionary<string, object> dic = res[i];
                    foreach (Dictionary<string, string> aux in dic.Values)
                    {
                        foreach (string k in aux.Keys)
                        {
                            Console.WriteLine("- " + k + ": " + aux[k]);
                        }
                    }
                }
            }

            public void getAllPaymentMethods()
            {
                Dictionary<string, object> res = connector.GetAllPaymentMethods("2153");
                printDictionary(res, "");
            }

            public void voidRequest() 
            {
                Dictionary<string, string> gbrdt = new Dictionary<string, string>();

                gbrdt.Add(TPConnector.MERCHANT, "2153");
                gbrdt.Add(TPConnector.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                gbrdt.Add(TPConnector.REQUESTKEY, "bb25d589-52bc-8e21-fc5d-47d677b0995c");

                string res = connector.VoidRequest(gbrdt);
                Console.WriteLine("\nResponse received was :\n{0} ", res);
            }
            
            public void returnRequest()
            {
                Dictionary<string, string> gbrdt = new Dictionary<string, string>();
  
                gbrdt.Add(TPConnector.MERCHANT, "2153");
                gbrdt.Add(TPConnector.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                gbrdt.Add(TPConnector.REQUESTKEY, "0db2e848-b0ab-6baf-f9e1-b70a82ed5844");
                gbrdt.Add(TPConnector.AMOUNT, "10");

                string res = connector.ReturnRequest(gbrdt);
                Console.WriteLine("\nResponse received was :\n{0} ", res);
            }

            public void getByRangeDateTime()
            {
                Dictionary<string, string> gbrdt = new Dictionary<string, string>();
               
                gbrdt.Add(TPConnector.MERCHANT, "2153");
                gbrdt.Add(TPConnector.STARTDATE, "2015-01-01");
                gbrdt.Add(TPConnector.ENDDATE, "2015-12-10");
                gbrdt.Add(TPConnector.PAGENUMBER, "1");

                Dictionary<string, Object> res = connector.getByRangeDateTime(gbrdt);
                printDictionary(res, "");
            }

            //Utils

            /// <summary>
            /// Permite emular la validación del Certificado SSL devolviendo true siempre
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="certificate"></param>
            /// <param name="chain"></param>
            /// <param name="sslPolicyErrors"></param>
            /// <returns>bool true</returns>
            private bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            private void printDictionary(Dictionary<string, object> p, string tab)
            {
                foreach (string k in p.Keys)
                {
                    if (p[k].GetType().ToString().Contains("System.Collections.Generic.Dictionary"))//.ToString().Contains("string"))
                    {
                        Console.WriteLine(tab + "- " + k);
                        Dictionary<string, object> n = (Dictionary<string, object>)p[k];
                        printDictionary(n, tab + "  ");
                    }
                    else
                    {
                        Console.WriteLine(tab + "- " + k + ": " + p[k]);
                    }
                }
            }
        }
    }
}
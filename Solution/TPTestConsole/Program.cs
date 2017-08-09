using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TodoPagoConnector;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Utils;
using TodoPagoConnector.Operations;

namespace TPTestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("init TodoPagoConnectorSample");

            TodoPagoConnectorSample tpcs = new TodoPagoConnectorSample();

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("getCredentials");
            tpcs.getCredentials();
            
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("GetEndpointForm");
            string endpointForm = tpcs.GetEndpointForm();
            Console.WriteLine("Response: {0}", endpointForm);

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
            tpcs.initGetStatus();
            Console.WriteLine("sendGetStatus");
            tpcs.sendGetStatus();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("PaymentMethods");
            tpcs.getAllPaymentMethods();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("VoidRequest");
            tpcs.voidRequest();

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("ReturnRequest");
            tpcs.returnRequest();

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("initGetByRangeDateTime");
            tpcs.getByRangeDateTime();

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("DiscoverPaymentMethods");
            tpcs.discoverPaymentMethods();

            Console.Read();
        }

        private class TodoPagoConnectorSample
        {

            //Connector
            private TPConnector connector;

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

            //Constructor
            public TodoPagoConnectorSample()
            {
                connector = initConnector();
                //connector = initConnectorForCredetials();
            }

            private TPConnector initConnector()
            {
                var headers = new Dictionary<String, String>();
                headers.Add("Authorization", authorization); 
                
                //Override SSL security - must be removed on PRD
                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);

                return new TPConnector(TPConnector.developerEndpoint, headers);
            }

            private TPConnector initConnectorForCredetials()
            {
                return new TPConnector(TPConnector.developerEndpoint);
            }

            public void initSendAuthorizeRequestParams()
            {
                sendAuthorizeRequestParams.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                sendAuthorizeRequestParams.Add(ElementNames.SESSION, "ABCDEF-1234-12221-FDE1-00000200");
                sendAuthorizeRequestParams.Add(ElementNames.MERCHANT, "2153");
                sendAuthorizeRequestParams.Add(ElementNames.URL_OK, "http://someurl.com/ok");
                sendAuthorizeRequestParams.Add(ElementNames.URL_ERROR, "http://someurl.com/fail");
                sendAuthorizeRequestParams.Add(ElementNames.ENCODING_METHOD, "XML");

                sendAuthorizeRequestPayload.Add(ElementNames.MERCHANT, "2153");
                sendAuthorizeRequestPayload.Add(ElementNames.OPERATIONID, "2121");
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

            public void sendAuthorizeRequest()
            {
                string output = "";
                try
                {
                    var res = connector.SendAuthorizeRequest(sendAuthorizeRequestParams, sendAuthorizeRequestPayload);

                    printDictionary(res, "");

                    //string response = res["StatusCode"].ToString() + "-" + res["StatusMessage"].ToString();
                    //string detail = "URL_Request = " + res["URL_Request"] + "\r\nRequestKey = " + res["RequestKey"] + "\r\nPublicRequestKey = " + res["PublicRequestKey"];

                    // output += "\r\n- " + res["StatusCode"].ToString();
                    // output += "\r\n- " + res["StatusMessage"].ToString();

                    // output += "\r\n- URL_Request = " + res["URL_Request"];
                    //output += "\r\n- RequestKey = " + res["RequestKey"];
                    //output += "\r\n- PublicRequestKey = " + res["PublicRequestKey"];

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
                    output += "\r\n" + ex.Message;
                }

                Console.WriteLine(output);
            }

            public void initGetAuthorizeAnswer()
            {
                getAuthorizeAnswerParams.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                getAuthorizeAnswerParams.Add(ElementNames.SESSION, null);
                getAuthorizeAnswerParams.Add(ElementNames.MERCHANT, "2153");
                getAuthorizeAnswerParams.Add(ElementNames.REQUESTKEY, "710268a7-7688-c8bf-68c9-430107e6b9da");
                getAuthorizeAnswerParams.Add(ElementNames.ANSWERKEY, "693ca9cc-c940-06a4-8d96-1ab0d66f3ee6");
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
                getStatusOperationId = "8000";
                getStatusMerchant = "2658";
            }

            public void sendGetStatus()
            {
                List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();

                try
                {
                    res = connector.GetStatus(getStatusMerchant, getStatusOperationId);
                }
                catch (ResponseException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (res != null && res.Count > 0)
                {
                    Dictionary<string, object> d = res[0];
                    if (d.Values != null && d.Values.Count > 0)
                    {
                        for (int i = 0; i < res.Count; i++)
                        {
                            Dictionary<string, object> dic = res[i];
                            foreach (Dictionary<string, object> aux in dic.Values)
                            {
                                foreach (string k in aux.Keys)
                                {
                                    if (aux[k].GetType().IsInstanceOfType(aux))
                                    {
                                        Dictionary<string, object> a = (Dictionary<string, object>)aux[k];
                                        Console.WriteLine("- " + k + ": ");
                                        foreach (Dictionary<string, object> aux2 in a.Values)
                                        {
                                            Console.WriteLine("- REFUND: ");
                                            foreach (string b in aux2.Keys)
                                            {
                                                Console.WriteLine("- " + b + ": " + aux2[b]);

                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("- " + k + ": " + aux[k]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sin datos");
                    }
                }
                else
                {
                    Console.WriteLine("Sin datos");
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

                gbrdt.Add(ElementNames.MERCHANT, "2153");
                gbrdt.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                gbrdt.Add(ElementNames.REQUESTKEY, "bb25d589-52bc-8e21-fc5d-47d677b0995c");

                Dictionary<string, object> res = connector.VoidRequest(gbrdt);
                printDictionary(res, "");           
            }
            
            public void returnRequest()
            {
                Dictionary<string, string> gbrdt = new Dictionary<string, string>();

                gbrdt.Add(ElementNames.MERCHANT, "2153");
                gbrdt.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3");
                gbrdt.Add(ElementNames.REQUESTKEY, "0db2e848-b0ab-6baf-f9e1-b70a82ed5844");
                gbrdt.Add(ElementNames.AMOUNT, "10");

                Dictionary<string, object> res = connector.ReturnRequest(gbrdt);
                printDictionary(res, "");
            }

            public void getByRangeDateTime()
            {
                Dictionary<string, string> gbrdt = new Dictionary<string, string>();

                gbrdt.Add(ElementNames.MERCHANT, "2153");
                gbrdt.Add(ElementNames.STARTDATE, "2015-01-01");
                gbrdt.Add(ElementNames.ENDDATE, "2015-12-10");
                gbrdt.Add(ElementNames.PAGENUMBER, "1");

                Dictionary<string, Object> res = connector.getByRangeDateTime(gbrdt);
                printDictionary(res, "");
            }


            public void getCredentials()
            {
                User user = new User();

                try {
                     user = connector.getCredentials(getUser());
                     connector.setAuthorize(user.getApiKey());
                }
                     catch (EmptyFieldException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                     catch (ResponseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(user.toString()); 
            }

            private User getUser()
            {
                String mail = "test@Test.com.ar";
                String pass = "pass1234";
                User user = new User(mail, pass);
                return user;
            }

            public void discoverPaymentMethods()
            {
                Dictionary<string, object> res = connector.DiscoverPaymentMethods();
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

            public string GetEndpointForm()
            {
                return connector.GetEndpointForm();
            }

            private void printDictionary(Dictionary<string, object> p, string tab)
            {
                foreach (string k in p.Keys)
                {
                    if (p[k]!= null && p[k].GetType().ToString().Contains("System.Collections.Generic.Dictionary"))//.ToString().Contains("string"))
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

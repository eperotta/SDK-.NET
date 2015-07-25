using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagoConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagoConnector;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.IO;

namespace TPTestConsole
{
    class Program
    {

        
        



        static void Main(string[] args)
        {
            Console.WriteLine("init TodoPagoConnectorSample");

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
            tpcs.initGetStatus();
            Console.WriteLine("sendGetStatus");
            tpcs.sendGetStatus();

            Console.Read();
        }





        




        

        class TodoPagoConnectorSample{



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
            private Dictionary<string, string> sendAuthorizeRequestParams  = new Dictionary<string, string>();
            private Dictionary<string, string> sendAuthorizeRequestPayload = new Dictionary<string, string>();
            //GetAuthorizeAnswer
            private Dictionary<string, string> getAuthorizeAnswerParams = new Dictionary<string, string>();
            //GetStatus
            private string getStatusMerchant;
            private string getStatusOperationId;
            


            //Authentification and Endpoint
            string authorization = "PRISMA 912EC803B2CE49E4A541068D495AB570";
            string endpoint = "https://50.19.97.101:8243";
        


            //Constructor
            public TodoPagoConnectorSample()
            {
                connector = initConnector();
            }



            private TPConnector initConnector()
            {
                var headers = new Dictionary<String, String>();

                headers.Add("Authorization", authorization);

                headers.Add("USERAGENT", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
                headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
                headers.Add("UserAgent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");


                //Override SSL security - must be removed on PRD
                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);

                return new TPConnector(endpoint, headers);
            }





            public void initSendAuthorizeRequestParams()
            {
                sendAuthorizeRequestParams.Add(SECURITY, "912EC803B2CE49E4A541068D495AB570");
                sendAuthorizeRequestParams.Add(SESSION, "ABCDEF-1234-12221-FDE1-00000200");
                sendAuthorizeRequestParams.Add(MERCHANT, "538");
                sendAuthorizeRequestParams.Add(URL_OK, "http://google.com");
                sendAuthorizeRequestParams.Add(URL_ERROR, "http://yahoo.com");
                sendAuthorizeRequestParams.Add(ENCODING_METHOD, "XML");

                var payload = new Dictionary<string, string>();
                sendAuthorizeRequestPayload.Add(MERCHANT, "538");
                sendAuthorizeRequestPayload.Add(OPERATIONID, "01");
                sendAuthorizeRequestPayload.Add(CURRENCYCODE, "032");
                sendAuthorizeRequestPayload.Add(AMOUNT, "55");
                sendAuthorizeRequestPayload.Add(EMAILCLIENTE, "email_cliente@dominio.com");
            }

            public void sendAuthorizeRequest()
            {

                string output= "";
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
                getAuthorizeAnswerParams.Add(SECURITY, "1234567890ABCDEF1234567890ABCDEF");
                getAuthorizeAnswerParams.Add(SESSION, null);
                getAuthorizeAnswerParams.Add(MERCHANT, "305");
                getAuthorizeAnswerParams.Add(REQUESTKEY, "8496472a-8c87-e35b-dcf2-94d5e31eb12f");
                getAuthorizeAnswerParams.Add(ANSWERKEY, "8496472a-8c87-e35b-dcf2-94d5e31eb12f");

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
                getStatusMerchant = "538";
            }

            public void sendGetStatus()
            {
                string output = "";
                try
                {
                    var res = connector.GetStatus(getStatusMerchant, getStatusOperationId);
                    /*
                    string response = res.Count().ToString();
                    string detail = "";
                    foreach (var operation in res)
                    {
                        foreach (var propName in operation.Keys)
                        {
                            detail += propName + ": " + operation[propName] + "\r\n";
                        }

                    }
                    Console.WriteLine(response);
                    Console.WriteLine(detail);
                    */

                    output += "\r\n- " + res.Count().ToString();
                    string detail = "";
                    foreach (var operation in res)
                    {
                        foreach (var propName in operation.Keys)
                        {
                            output += "\r\n     " + propName + ": " + operation[propName];
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
        }

    }
}

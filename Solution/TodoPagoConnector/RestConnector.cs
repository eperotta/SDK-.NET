using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.IO;
using System;
using Newtonsoft.Json;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Utils;
using TodoPagoConnector.Operations;

namespace TodoPagoConnector
{
    class RestConnector
    {
        private string endpoint = "";
        private Dictionary<string, string> headders;
        private WebClient client;
        private const string REQUESTTYPE = "RequestType";

        public RestConnector(string endpoint, Dictionary<string, string> headders)
        {
            this.endpoint = endpoint;
            client = new WebClient();

            IWebProxy theProxy = client.Proxy;
            if (theProxy != null)
            {
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }

            if (headders!= null) { 
                 this.headders = headders;
                 foreach (var key in this.headders.Keys){
                     client.Headers.Add(key, this.headders[key]);
                 } 
            }
            
        }

        public List<Dictionary<string, object>> getByOperationID(string merchant, string operation)
        {
            string res = client.DownloadString(endpoint + "Operations/GetByOperationId" + "/MERCHANT/" + merchant + "/OPERATIONID/" + operation);

            List<Dictionary<string, object>> returnList = new List<Dictionary<string, object>>();
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.LoadXml(res);

                XmlNodeList nl = xd.GetElementsByTagName("OperationsColections");
                for (int i = 0; i < nl.Count; i++)
                {
                    Dictionary<string, object> masterDic = new Dictionary<string, object>();
                    XmlDocument xd2 = new XmlDocument();
                    xd2.LoadXml(nl[i].InnerXml);
                    XmlNodeList nl2 = xd2.GetElementsByTagName("Operations");
                    XmlNodeList nl3 = xd2.GetElementsByTagName("REFUND");

                    Dictionary<string, object> detailsDic = new Dictionary<string, object>();
                    for (int j = 0; j < nl2.Count; j++)
                    {
                        detailsDic = new Dictionary<string, object>();
                        XmlNode aux = nl2[i].FirstChild;
                        while (aux != null)
                        {
                            if (!"REFUNDS".Equals(aux.Name))
                            {
                                 string a = aux.InnerText;
                                 string b = aux.Name;
                                 //Console.WriteLine("- " + b + " : " + a);
                                detailsDic.Add(b, a);
                            }
                            aux = aux.NextSibling;
                        }
                        masterDic.Add("Operations", detailsDic);
                    }

                    Dictionary<string, object> detailsDic2 = new Dictionary<string, object>();
                    for (int k = 0; k < nl3.Count; k++)
                    {
                        Dictionary<string, object> detailsDic3 = new Dictionary<string, object>();
                        XmlNode aux2 = nl3[i].FirstChild;

                        while (aux2 != null)
                        {
                            string a2 = aux2.InnerText;
                            string b2 = aux2.Name;

                            detailsDic3.Add(b2, a2);
                            aux2 = aux2.NextSibling;

                        }
                        detailsDic2.Add("REFUND" + k, detailsDic3);
                    }
                    detailsDic.Add("REFUNDS", detailsDic2);
                    returnList.Add(masterDic);
                }

            }
            catch (System.Xml.XmlException ex)
            {
                throw new ResponseException(res);
            }

            return returnList;
        }


        public Dictionary<string, object> GetAllPaymentMethods(string merchant)
        {
            string res = client.DownloadString(endpoint + "PaymentMethods/Get" + "/MERCHANT/" + merchant);
            //System.Console.WriteLine(res);
             XmlDocument xd = new XmlDocument();
            try{         
                xd.LoadXml(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return toDictionary(xd);
        }

        public Dictionary<string, object> VoidRequest(Dictionary<string, string> param)
        {    
            string URL = endpoint + "Authorize";
            string result;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            IWebProxy theProxy = httpWebRequest.Proxy;

            if (theProxy != null)
            {
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }

            foreach (var key in this.headders.Keys)
            {
                httpWebRequest.Headers.Add(key, this.headders[key]);
            }

            param.Add(REQUESTTYPE, "VoidRequest");
          
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                 string json = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);
                 streamWriter.Write(json);
               //System.Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                 result = streamReader.ReadToEnd();
                //System.Console.WriteLine(result);
            }
            return OperationsParser.parseJsonToVoidRequest(result);
        }

        public Dictionary<string, object> ReturnRequest(Dictionary<string, string> param)
        {
            string URL = endpoint + "Authorize";
            string result;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            IWebProxy theProxy = httpWebRequest.Proxy;

            if (theProxy != null)
            {
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }

            foreach (var key in this.headders.Keys)
            {
                httpWebRequest.Headers.Add(key, this.headders[key]);
            }

            param.Add(REQUESTTYPE, "ReturnRequest");

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);
                streamWriter.Write(json);
                //System.Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                //System.Console.WriteLine(result);
            }

            return OperationsParser.parseJsonToReturnRequest(result);
        }

        public Dictionary<string, object> GetByRangeDateTime(Dictionary<string, string> param)
        {
            string url = endpoint + "Operations/GetByRangeDateTime";

            if (param.ContainsKey(ElementNames.MERCHANT))
            {
                string merchant = param[ElementNames.MERCHANT];
                url = url + "/MERCHANT/" + merchant;
            }
            if (param.ContainsKey(ElementNames.STARTDATE))
            {
                string startDate = param[ElementNames.STARTDATE];
                url = url + "/STARTDATE/" + startDate;
            }
            if (param.ContainsKey(ElementNames.ENDDATE))
            {
                string endDate = param[ElementNames.ENDDATE];
                url = url + "/ENDDATE/" + endDate;
            }
            if (param.ContainsKey(ElementNames.PAGENUMBER))
            {
                string pageNumber = param[ElementNames.PAGENUMBER];
                url = url + "/PAGENUMBER/" + pageNumber;
            }

            XmlDocument xd = new XmlDocument();

            try{
                string res = client.DownloadString(url);
              //System.Console.WriteLine(res);
                xd.LoadXml(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return toDictionary(xd);
        }


        public User getCredentials(User user)
        {
            string URL = endpoint + "Credentials";
            URL = URL.Replace("t/1.1/", "");

            string result;
            User userResponse = new User();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            IWebProxy theProxy = httpWebRequest.Proxy;
            if (theProxy != null)
            {
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(user.toDictionary(), Newtonsoft.Json.Formatting.Indented);
                streamWriter.Write(json);
                //System.Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                //System.Console.WriteLine(result);
            }

            userResponse = OperationsParser.parseJsonToUser(result);
            return userResponse;
        }


        public Dictionary<string, object> toDictionary(XmlDocument xd)
        {
            Dictionary<string, object> rv = new Dictionary<string, object>();
            Dictionary<string, object> ret = new Dictionary<string, object>();
            foreach (XmlNode child in xd.ChildNodes)
            {
                rv = toDictionary(child);

                if (!rv.ContainsKey(" "))
                {
                    foreach (string k in rv.Keys)
                    {
                        //Failsafe, exception thrown on adding a key that already exists
                        if (!ret.ContainsKey(k))
                        {
                            ret.Add(k, rv[k]);
                        }
                    }
                }
                else
                {
                    ret.Add(child.Name, rv[" "]);
                }
            }
            return ret;
        }


        public Dictionary<string, object> toDictionary(XmlNode xd)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            string keyVal = " ";
            foreach (XmlNode child in xd.ChildNodes)
            {
                if (child.HasChildNodes && child.FirstChild.HasChildNodes)
                {
                    Dictionary<string, object> rv = toDictionary(child);
                    if (!rv.ContainsKey(" "))
                    {
                        foreach (string k in rv.Keys)
                        {
                            //Failsafe, exception thrown on adding a key that already exists
                            if (!ret.ContainsKey(k))
                            {
                                ret.Add(k, rv[k]);
                            }
                        }
                    }
                    else
                    {
                        ret.Add(child.Name, rv[" "]);
                    }
                }
                else
                {
                    if (child.Name.Contains("Id"))
                    {
                        keyVal = child.InnerText;
                    }
                    ret.Add(child.Name, child.InnerText);
                }
            }
            Dictionary<string, object> aux = new Dictionary<string, object>();
            aux.Add(keyVal, ret);
            return aux;
        }

    }

}

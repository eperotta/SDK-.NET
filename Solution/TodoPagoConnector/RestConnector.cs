using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.IO;


namespace TodoPagoConnector
{
    class RestConnector
    {
        private string endpoint = "";
        private Dictionary<string, string> headders;
        private WebClient client;

        public const string SECURITY = "Security";
        public const string MERCHANT = "Merchant";
        public const string REQUESTKEY = "RequestKey";
        public const string AUTHORIZATIONKEY = "AuthorizationKey";
        public const string AMOUNT = "Amount";
        public const string REQUESTCHANNEL = "requestChannel";
        public const string CURRENCYCODE = "currencyCode";

        public const string STARTDATE = "STARTDATE";
        public const string ENDDATE = "ENDDATE";
        public const string PAGENUMBER = "PAGENUMBER";

        public RestConnector(string endpoint, Dictionary<string, string> headders)
        {
            this.endpoint = endpoint;
            this.headders = headders;

            client = new WebClient();
            
            //Add all headers on the Dictionary
            foreach (var key in this.headders.Keys){
                client.Headers.Add(key, this.headders[key]);
            }  
        }

        public List<Dictionary<string, object>> getByOperationID(string merchant, string operation)
        {
            string res = client.DownloadString(endpoint + "Operations/GetByOperationId" + "/MERCHANT/" + merchant + "/OPERATIONID/" + operation);
            
            List<Dictionary<string, object>> returnList = new List<Dictionary<string, object>>();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(res);
            XmlNodeList nl = xd.GetElementsByTagName("OperationsColections");
            for (int i = 0; i < nl.Count; i++)
            {
                Dictionary<string, object> masterDic = new Dictionary<string, object>();
                XmlDocument xd2 = new XmlDocument();
                xd2.LoadXml(nl[i].InnerXml);
                XmlNodeList nl2 = xd2.GetElementsByTagName("Operations");
                for (int j = 0; j < nl2.Count; j++)
                {
                    Dictionary<string, string> detailsDic = new Dictionary<string, string>();
                    XmlNode aux = nl2[i].FirstChild;
                    while (aux != null)
                    {
                        string a = aux.InnerText;
                        string b = aux.Name;
                        //Console.WriteLine("- " + b + " : " + a);
                        detailsDic.Add(b, a);
                        aux = aux.NextSibling;
                    }
                    masterDic.Add("Operations", detailsDic);
                }
                returnList.Add(masterDic);
            }
            return returnList;
        }


        public Dictionary<string, object> GetAllPaymentMethods(string merchant)
        {
            string res = client.DownloadString(endpoint + "PaymentMethods/Get" + "/MERCHANT/" + merchant);
            //Console.WriteLine(res);
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(res);
            return toDictionary(xd);
        }

        public string VoidRequest(Dictionary<string, string> param)
        {    
            string URL = endpoint + "Authorize";
            string result;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            foreach (var key in this.headders.Keys)
            {
                httpWebRequest.Headers.Add(key, this.headders[key]);
            }
       
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                string json = "{\"RequestType\":\"VoidRequest\"";

                     if (param.ContainsKey(SECURITY))
                     {
                         string security = param[SECURITY];
                         json = json + ",\"Security\":\"" + security + "\"";
                     }
                     if (param.ContainsKey(REQUESTKEY))
                     {
                        string requestKey = param[REQUESTKEY];
                        json = json + ",\"RequestKey\":\"" + requestKey + "\"";
                     }
                     if (param.ContainsKey(AUTHORIZATIONKEY))
                     {
                        string authorizationKey = param[AUTHORIZATIONKEY];
                        json = json + ",\"AuthorizationKey\":\"" + authorizationKey + "\"";
                     }
                     if (param.ContainsKey(MERCHANT))
                     {
                        string merchant = param[MERCHANT];
                        json = json + ",\"Merchant\":\"" + merchant + "\"";
                     }               
                    if (param.ContainsKey(REQUESTCHANNEL))
                    {
                        string requestChannel = param[REQUESTCHANNEL];
                        json = json + ",\"RequestChannel\":\"" + requestChannel + "\"";
                    }

                json = json + "}";
                streamWriter.Write(json);
                //Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                 result = streamReader.ReadToEnd();
                 //Console.WriteLine(result);
            }
            return result;
        }

        public string ReturnRequest(Dictionary<string, string> param)
        {
            string URL = endpoint + "Authorize";
            string result;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            foreach (var key in this.headders.Keys)
            {
                httpWebRequest.Headers.Add(key, this.headders[key]);
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                string json = "{\"RequestType\":\"ReturnRequest\"";

                if (param.ContainsKey(SECURITY))
                {
                    string security = param[SECURITY];
                    json = json + ",\"Security\":\"" + security + "\"";
                }
                if (param.ContainsKey(AUTHORIZATIONKEY))
                {
                    string authorizationKey = param[AUTHORIZATIONKEY];
                    json = json + ",\"AuthorizationKey\":\"" + authorizationKey + "\"";
                }
                if (param.ContainsKey(MERCHANT))
                {
                    string merchant = param[MERCHANT];
                    json = json + ",\"Merchant\":\"" + merchant + "\"";
                }
                if (param.ContainsKey(AMOUNT))
                {
                    float amount = float.Parse(param[AMOUNT]);
                    json = json + ",\"Amount\":" + amount + "";
                }
                if (param.ContainsKey(REQUESTKEY))
                {
                    string requestKey = param[REQUESTKEY];
                    json = json + ",\"RequestKey\":\"" + requestKey + "\"";
                }
                if (param.ContainsKey(CURRENCYCODE))
                {
                    string currencyCode = param[CURRENCYCODE];
                    json = json + ",\"CurrencyCode\":\"" + currencyCode + "\"";
                }

                json = json + "}";
                streamWriter.Write(json);
                //Console.WriteLine(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                //Console.WriteLine(result);
            }
            return result;
        }

        public Dictionary<string, object> GetByRangeDateTime(Dictionary<string, string> param)
        {
            string url = endpoint + "Operations/GetByRangeDateTime";

            if (param.ContainsKey(MERCHANT))
            {
                string merchant = param[MERCHANT];
                url = url + "/MERCHANT/" + merchant;
            }
            if (param.ContainsKey(STARTDATE))
            {
                string startDate = param[STARTDATE];
                url = url + "/STARTDATE/" + startDate;
            }
            if (param.ContainsKey(ENDDATE))
            {
                string endDate = param[ENDDATE];
                url = url + "/ENDDATE/" + endDate;
            }
            if (param.ContainsKey(PAGENUMBER))
            {
                string pageNumber = param[PAGENUMBER];
                url = url + "/PAGENUMBER/" + pageNumber;
            }

            string res = client.DownloadString(url);
            //Console.WriteLine(res);
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(res);
            return toDictionary(xd);
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

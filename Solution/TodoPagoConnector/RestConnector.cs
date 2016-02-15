using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Xml;



namespace TodoPagoConnector
{
    class RestConnector
    {
        private string endpoint = "";
        private Dictionary<string, string> headders;
        private WebClient client;

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

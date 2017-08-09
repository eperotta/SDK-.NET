using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace TodoPagoConnector.Services
{
    internal class WebService
    {
        public string Connect(string uri, string soapOperation, string xmlRequestText, Dictionary<string, string> headers)
        {
            //Dictionary con Headers
            string xmlResponseText = "";

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(uri);

            httpRequest.Headers.Add("SOAPAction", soapOperation);
            if (headers != null)
            {
                foreach (var headerKey in headers.Keys)
                {
                    httpRequest.Headers.Add(headerKey, headers[headerKey]);
                }
            }

            httpRequest.ContentType = "text/xml;charset=utf-8";
            httpRequest.Method = "POST";

            XmlDocument xmlRequestDocument = new XmlDocument();
            xmlRequestDocument.LoadXml(xmlRequestText);

            using (Stream stream = httpRequest.GetRequestStream())
            {
                xmlRequestDocument.Save(stream);
            }

            using (WebResponse response = httpRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    xmlResponseText = reader.ReadToEnd();
                }
            }

            return xmlResponseText;
        }
    }
}

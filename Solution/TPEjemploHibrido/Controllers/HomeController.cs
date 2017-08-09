using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using TodoPagoConnector;
using TodoPagoConnector.Utils;
using TPEjemploHibrido.Models;

namespace TPEjemploHibrido.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(InfoModel info)
        {
            TPConnector connector = initConnector(info);

            if (info.StatusCode == "-1")
                executeGetAuthorizeAnswer(connector, info);

            return View(info);
        }

        [HttpPost]
        public ActionResult FormularioHibrido(InfoModel info)
        {
            TPConnector connector = initConnector(info);

            executeSendAuthorizeRequest(connector, info);

            return View(info);
        }

        private Dictionary<String, Object> executeSendAuthorizeRequest(TPConnector connector, InfoModel info)
        {
            Dictionary<string, string> sendAuthorizeRequestParams = new Dictionary<string, string>();
            Dictionary<string, string> sendAuthorizeRequestPayload = new Dictionary<string, string>();
            Dictionary<String, Object> res = new Dictionary<String, Object>();

            initSendAuthorizeRequestParams(sendAuthorizeRequestParams, sendAuthorizeRequestPayload, info);

            try
            {
                res = connector.SendAuthorizeRequest(sendAuthorizeRequestParams, sendAuthorizeRequestPayload);

                if (res.ContainsKey("PublicRequestKey") && res["PublicRequestKey"] != null)
                {
                    info.PublicRequestKey = res["PublicRequestKey"].ToString();
                }

                if (res.ContainsKey("RequestKey") && res["RequestKey"] != null)
                {
                    info.RequestKey = res["RequestKey"].ToString();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    WebResponse resp = ex.Response;
                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                    {
                        res.Add("exception","\r\n" + sr.ReadToEnd() + " - " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                res.Add("exception", "\r\n" + ex.Message);
            }

            return res;
        }

        private void initSendAuthorizeRequestParams(Dictionary<string, string> sendAuthorizeRequestParams, Dictionary<string, string> sendAuthorizeRequestPayload, InfoModel info)
        {
            sendAuthorizeRequestParams.Add(ElementNames.SECURITY, info.Security);
            sendAuthorizeRequestParams.Add(ElementNames.SESSION, "ABCDEF-1234-12221-FDE1-00000200");
            sendAuthorizeRequestParams.Add(ElementNames.MERCHANT, info.MerchantId);
            sendAuthorizeRequestParams.Add(ElementNames.URL_OK, "http://someurl.com/ok");
            sendAuthorizeRequestParams.Add(ElementNames.URL_ERROR, "http://someurl.com/fail");
            sendAuthorizeRequestParams.Add(ElementNames.ENCODING_METHOD, "XML");

            sendAuthorizeRequestPayload.Add(ElementNames.MERCHANT, info.MerchantId);
            sendAuthorizeRequestPayload.Add(ElementNames.OPERATIONID, "2121");
            sendAuthorizeRequestPayload.Add(ElementNames.CURRENCYCODE, "032");
            sendAuthorizeRequestPayload.Add(ElementNames.AMOUNT, info.Monto);
            sendAuthorizeRequestPayload.Add(ElementNames.EMAILCLIENTE, "email_cliente@dominio.com");
            sendAuthorizeRequestPayload.Add(ElementNames.MAXINSTALLMENTS, "6"); //NO MANDATORIO, MAXIMA CANTIDAD DE CUOTAS, VALOR MAXIMO 12
            sendAuthorizeRequestPayload.Add(ElementNames.MININSTALLMENTS, "1"); //NO MANDATORIO, MINIMA CANTIDAD DE CUOTAS, VALOR MINIMO 1
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

        private Dictionary<String, Object> executeGetAuthorizeAnswer(TPConnector connector, InfoModel info)
        {
            Dictionary<string, string> getAuthorizeAnswerParams = new Dictionary<string, string>();

            info.ResultadoGAA = String.Empty;

            initGetAuthorizeAnswer(getAuthorizeAnswerParams, info);

            var res = new Dictionary<String, Object>();

            try
            {
                res = connector.GetAuthorizeAnswer(getAuthorizeAnswerParams);

                if (res.ContainsKey("StatusCode") && res["StatusCode"] != null)
                {
                    info.StatusCode = res["StatusCode"].ToString();
                }

                if (res.ContainsKey("StatusMessage") && res["StatusMessage"] != null)
                {
                    info.StatusMessage = res["StatusMessage"].ToString();
                }

                foreach (var key in res.Keys)
                {
                    info.ResultadoGAA += "- " + key + ": " + res[key];
                    if (key.Equals("Payload"))
                    {
                        XmlNode[] aux = (XmlNode[])res["Payload"];
                        if (aux != null)
                        {
                            for (int i = 0; i < aux.Length; i++)
                            {
                                XmlNodeList inner = aux[i].ChildNodes;
                                for (int j = 0; j < inner.Count; j++)
                                {
                                    info.ResultadoGAA += "     " + inner.Item(j).Name + " : " + inner.Item(j).InnerText;
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
                        info.ResultadoGAA = sr.ReadToEnd() + " - " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                info.ResultadoGAA = ex.Message + " - " + ex.InnerException.Message + " - " + ex.HelpLink;
            }

            return res;
        }

        private void initGetAuthorizeAnswer(Dictionary<string, string> getAuthorizeAnswerParams, InfoModel info)
        {
            getAuthorizeAnswerParams.Add(ElementNames.SECURITY, info.Security);
            getAuthorizeAnswerParams.Add(ElementNames.SESSION, null);
            getAuthorizeAnswerParams.Add(ElementNames.MERCHANT, info.MerchantId);
            getAuthorizeAnswerParams.Add(ElementNames.REQUESTKEY, info.RequestKey);
            getAuthorizeAnswerParams.Add(ElementNames.ANSWERKEY, info.AnswerKey);
        }

        private TPConnector initConnector(InfoModel info)
        {
            var headers = new Dictionary<String, String>();
            headers.Add("Authorization", info.ApiKey);

            return new TPConnector(TPConnector.developerEndpoint, headers);
        }
    }
}
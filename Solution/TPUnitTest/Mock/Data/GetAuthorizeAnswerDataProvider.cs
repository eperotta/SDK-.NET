using System.Collections.Generic;
using System.Xml;
using TodoPagoConnector.Utils;

namespace TPUnitTest.Mock.Data
{
    internal static class GetAuthorizeAnswerDataProvider
    {
        public static Dictionary<string, object> GetAuthorizeAnswerOkResponse()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            response.Add(ElementNames.STATUS_CODE, -1);
            response.Add(ElementNames.STATUS_MESSAGE, "APROBADA");
            response.Add(ElementNames.AUTHORIZATIONKEY, "a61de00b-c118-2688-77b0-16dbe5799913");
            response.Add(ElementNames.ENCODINGMETHOD, "XML");

            string res = "<Payload><Answer xmlns=\"http://api.todopago.com.ar\"><DATETIME>2017-05-02T11:37:48Z</DATETIME><CURRENCYNAME>Peso Argentino</CURRENCYNAME><PAYMENTMETHODNAME>VISA</PAYMENTMETHODNAME><TICKETNUMBER>12</TICKETNUMBER><AUTHORIZATIONCODE>654402</AUTHORIZATIONCODE><CARDNUMBERVISIBLE>4507XXXXXXXX0010</CARDNUMBERVISIBLE><BARCODE></BARCODE><OPERATIONID>551</OPERATIONID><COUPONEXPDATE></COUPONEXPDATE><COUPONSECEXPDATE></COUPONSECEXPDATE><COUPONSUBSCRIBER></COUPONSUBSCRIBER><BARCODETYPE></BARCODETYPE><ASSOCIATEDDOCUMENTATION></ASSOCIATEDDOCUMENTATION><INSTALLMENTPAYMENTS>7</INSTALLMENTPAYMENTS></Answer><Request xmlns=\"http://api.todopago.com.ar\"><MERCHANT>2658</MERCHANT><OPERATIONID>551</OPERATIONID><AMOUNT>12.00</AMOUNT><CURRENCYCODE>32</CURRENCYCODE><AMOUNTBUYER>12.00</AMOUNTBUYER><BANKID>4</BANKID><PROMOTIONID>2712</PROMOTIONID></Request></Payload>";

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(res);
            
            XmlNodeList nl = xd.GetElementsByTagName("Payload");
            XmlNode[] array = (new List<XmlNode>(Shim<XmlNode>(nl[0]))).ToArray();
            
            response.Add(ElementNames.PAYLOAD, array);

            return response;
        }

        public static Dictionary<string, object> GetAuthorizeAnswerFailResponse()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            response.Add(ElementNames.STATUS_CODE, 404);
            response.Add(ElementNames.STATUS_MESSAGE, "ERROR: Transaccion Enexistente");
            response.Add(ElementNames.AUTHORIZATIONKEY, null);
            response.Add(ElementNames.ENCODINGMETHOD, null);
            response.Add(ElementNames.PAYLOAD, null);

            return response;
        }

        public static Dictionary<string, object> GetAuthorizeAnswer702Response()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            response.Add(ElementNames.STATUS_CODE, 702);
            response.Add(ElementNames.STATUS_MESSAGE, "Cuenta de Vendedor Invalida");
            response.Add(ElementNames.AUTHORIZATIONKEY, null);
            response.Add(ElementNames.ENCODINGMETHOD, null);
            response.Add(ElementNames.PAYLOAD, null);

            return response;
        }

        public static IEnumerable<T> Shim<T>(System.Collections.IEnumerable enumerable)
        {
            foreach (object current in enumerable)
            {
                yield return (T)current;
            }
        }
    }
}

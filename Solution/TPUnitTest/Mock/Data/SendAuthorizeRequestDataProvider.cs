using System.Collections.Generic;
using TodoPagoConnector.Utils;

namespace TPUnitTest.Mock.Data
{
    internal class SendAuthorizeRequestDataProvider
    {
        public static Dictionary<string, object> SendAuthorizeRequestOkResponse()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            response.Add(ElementNames.STATUS_CODE, -1);
            response.Add(ElementNames.STATUS_MESSAGE, "Solicitud de Autorizacion Registrada");
            response.Add(ElementNames.URL_REQUEST, "https://developers.todopago.com.ar/formulario/commands?command=formulario&amp;m=tdbda56ab-1b64-d470-efca-5817c6216429");
            response.Add(ElementNames.REQUEST_KEY, "5b26f546-e831-1551-d801-f426f1adfede");
            response.Add(ElementNames.PUBLIC_REQUEST_KEY, "tdbda56ab-1b64-d470-efca-5817c6216429");

            return response;
        }

        public static Dictionary<string, object> SendAuthorizeRequestFailResponse()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            response.Add(ElementNames.STATUS_CODE, 98001);
            response.Add(ElementNames.STATUS_MESSAGE, "El campo CSBTCITY es obligatorio. (Min Length 2)");
            response.Add(ElementNames.URL_REQUEST, null);
            response.Add(ElementNames.REQUEST_KEY, null);
            response.Add(ElementNames.PUBLIC_REQUEST_KEY, null);

            return response;
        }

        public static Dictionary<string, object> SendAuthorizeRequest702Response()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            response.Add(ElementNames.STATUS_CODE, 702);
            response.Add(ElementNames.STATUS_MESSAGE, "Cuenta de vendedor invalida");
            response.Add(ElementNames.URL_REQUEST, null);
            response.Add(ElementNames.REQUEST_KEY, null);
            response.Add(ElementNames.PUBLIC_REQUEST_KEY, null);

            return response;
        }
    }
}

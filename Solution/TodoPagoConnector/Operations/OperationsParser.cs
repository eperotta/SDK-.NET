using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using Newtonsoft.Json.Linq;

namespace TodoPagoConnector.Operations
{
    public static class OperationsParser
    {

        public static Dictionary<string, object> parseJsonToDictionary(string json)
        {
            Dictionary<string, object> aux = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return aux;
        }

        public static User parseJsonToUser(string json)
        {

            Dictionary<string, object> aux = parseJsonToDictionary(json);

            User user = new User();
            JObject jCredentials = (JObject)aux["Credentials"];
            JObject jresultado = (JObject)jCredentials["resultado"];

            if ((int)jresultado["codigoResultado"] != 0){
                    throw new ResponseException((string)jresultado["mensajeKey"] + " - " + (string)jresultado["mensajeResultado"]);
            }else{
                    user.setApiKey((string)jCredentials["APIKey"]);
                    user.setMerchant((string)jCredentials["merchantId"]);
            }

            return user;
        }


        public static Dictionary<string, object> parseJsonToVoidRequest(string json)
        {
            Dictionary<string, object> result = parseJsonToDictionary(json);
            JObject jVoidRequest = (JObject)result["VoidResponse"];
            Dictionary<string, object> aux = parseJsonToDictionary(jVoidRequest.ToString());
            result["VoidResponse"] = aux;
            return result;
        }

        public static Dictionary<string, object> parseJsonToReturnRequest(string json)
        {
            Dictionary<string, object> result = parseJsonToDictionary(json);
            JObject jVoidRequest = (JObject)result["ReturnResponse"];
            Dictionary<string, object> aux = parseJsonToDictionary(jVoidRequest.ToString());
            result["ReturnResponse"] = aux;
            return result;
        }


    }
}

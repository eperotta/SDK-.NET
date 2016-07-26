using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Utils;
using Newtonsoft.Json.Linq;

namespace TodoPagoConnector.Operations
{
    public static class OperationsParserBSA {

        public static string generateTransactionJson(TransactionBSA transaction) {
            
            Dictionary<string, object> aux = new   Dictionary<string, object> () ;
            aux.Add(ElementNames.BSA_GENERAL_DATA, transaction.getGeneralData());
            aux.Add(ElementNames.BSA_OPERATION_DATA, transaction.getOperationData());
            aux.Add(ElementNames.BSA_TECHNICAL_DATA, transaction.getTecnicalData());

            string transactionJson = JsonConvert.SerializeObject(aux, Newtonsoft.Json.Formatting.Indented);
            return transactionJson;
        } 

        public static Dictionary<string, object> parseJsonToDictionary(string json){
            Dictionary<string, object> aux = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return aux;
        }

        public static List<Dictionary<string, Object>> parseJsonToList(string json){
            List<Dictionary<string, Object>> aux = JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(json);
            return aux;
        }


        public static TransactionBSA parseJsonToTransaction(string json){

            TransactionBSA transaction = new TransactionBSA();
            if(json != null && json.Equals("")){
                string message = "Response vacio o nulo";
                throw new ResponseException(message);
            }

            Dictionary<string, object> result = parseJsonToDictionary(json);
            if (result.ContainsKey(ElementNames.BSA_ERROR_CODE)) {
                string message = (string)result[ElementNames.BSA_ERROR_CODE] + " - " + (string)result[ElementNames.BSA_ERROR_MESSAGE];               
                throw new ResponseException(message);
            }    
            
            transaction.setChannel((string)result[ElementNames.BSA_CHANNEL]);
            transaction.setUrlHibridFormResuorces((string)result[ElementNames.BSA_URL_HIBRID_FROM_RESOURCES]);
            transaction.setPublicRequestKeys((string)result[ElementNames.BSA_PUBLIC_REQUEST_KEY]);
            transaction.setRequestKey((string)result[ElementNames.BSA_REQUEST_KEY]);
            transaction.setTransactionID((string)result[ElementNames.BSA_TRANSACTION_ID_RESPONSE]);

            return transaction;
        }

        public static PaymentMethodsBSA parseJsonToPaymentMethod(string json){

            PaymentMethodsBSA paymentMethodsBSA = new PaymentMethodsBSA();
            List<Dictionary<string, Object>> paymentMethodsBSAList = new List<Dictionary<string, Object>> ();
            string message = "Response vacio o nulo";

            if (json != null && json.Equals("")){
                throw new ResponseException(message);
            }

            try{
                List<Dictionary<string, Object>> jsonlist = parseJsonToList(json);

                foreach (Dictionary<string, Object> jsonElement in jsonlist){
                    Dictionary<string, Object> paymentMethodsBSADic = new Dictionary<string, Object>();
                    paymentMethodsBSADic.Add(ElementNames.BSA_ID_MEDIO_PAGO,(string)jsonElement[ElementNames.BSA_ID_MEDIO_PAGO]);
                    paymentMethodsBSADic.Add(ElementNames.BSA_NOMBRE,(string)jsonElement[ElementNames.BSA_NOMBRE]);
                    paymentMethodsBSADic.Add(ElementNames.BSA_TIPO_MEDI_PAGO, (string)jsonElement[ElementNames.BSA_TIPO_MEDI_PAGO]);
                    paymentMethodsBSADic.Add(ElementNames.BSA_ID_BANCO, (string)jsonElement[ElementNames.BSA_ID_BANCO]);
                    paymentMethodsBSADic.Add(ElementNames.BSA_NOMBRE_BANCO, (string)jsonElement[ElementNames.BSA_NOMBRE_BANCO]);
                    paymentMethodsBSAList.Add(paymentMethodsBSADic);
                }

            } catch (Exception ex) {
                throw new ResponseException(message);
            }

            paymentMethodsBSA.setPaymentMethodsBSAList(paymentMethodsBSAList);
            return paymentMethodsBSA;
        }

    }
}

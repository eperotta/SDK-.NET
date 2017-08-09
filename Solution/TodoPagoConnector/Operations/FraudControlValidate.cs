using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using TodoPagoConnector.Utils;
using System.Text.RegularExpressions;
namespace TodoPagoConnector.Operations
{
    internal class FraudControlValidate{

        private const string PUNCT = "[!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~]";
	    private const string ASCII = "\\P{ASCII}";
	    private const string PHONE = "[^0-9]";
	    private const string CSIT_DESCRIPTION = "CSITPRODUCTDESCRIPTION";
	    private const string CSBTSTATE = "CSBTSTATE";
	    private const char   NUMERAL = '#';	
	    private const string FIELD = "field";
	    private const string VALIDATE = "validate";
	    private const string FORMAT = "format";
	    private const string FUNCTION = "function";
	    private const string MESSAGE = "message";
	    private const string PARAMS = "params";
	    private const string DEFAULT = "default";
        private const string LOCATION_JSON = "TodoPagoConnector.Resources.validations.json";

        private Dictionary<string, Object> jsonDictionary;
        private Dictionary<string, string> resultDictionary;
        private Dictionary<string, string> CSITDictionary;
        private Dictionary<string, string> stateCodeDictionary;
	    private List<String> campError;
        private Dictionary<string, int> keyDictionary;

        public FraudControlValidate (){

            var assembly = Assembly.GetExecutingAssembly();

            string result = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(LOCATION_JSON))
            using (StreamReader reader = new StreamReader(stream)){
                result = reader.ReadToEnd();
                this.jsonDictionary = parseJsonToDictionary(result);
            }

            this.resultDictionary = new Dictionary<string, string>();
            this.CSITDictionary = new Dictionary<string, string>();
            this.campError = new List<String>(); 
            setState();
            setKeyMap();
        }


        private Dictionary<string, object> parseJsonToDictionary(String json) {
            Dictionary<string, object> aux = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return aux;
        }

        private List<Dictionary<string, Object>> parseJsonToList(string json) {
            List<Dictionary<string, Object>> aux = JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(json);
            return aux;
        }

        public Dictionary<string, string> validate(Dictionary<string, string> parameters) {

		try {
            foreach (string k in jsonDictionary.Keys) {

                     string key = k;
                     JObject json = (JObject)jsonDictionary[key];
                     JArray  jArrayValidate = (JArray)json[VALIDATE];
                     JArray  jArrayFormat = (JArray)json[FORMAT];

                     string value = String.Empty;
                     if (parameters.ContainsKey(key)) {
                         value = parameters[key];
                         selector(jArrayValidate, jArrayFormat, value, key);
                     }else{
                         if (jArrayValidate!=null) {
                             json = (JObject)jArrayValidate[0];
                             Dictionary<string, object> jsonDic = parseJsonToDictionary(json.ToString());
                             string function = (string)jsonDic[FUNCTION];
 
                             if (function.Equals("notEmpty")) {
                                addError(key);
                                addField(key, "El campo " + key + " es requerido");
                             }
                         }

                     } 
            }

            foreach (string k in parameters.Keys){
                addField(k, parameters[k]);
            }

			Dictionary<string, string> csitDictionary = csitFormat(254);
            foreach (string k in csitDictionary.Keys) {
                this.resultDictionary[k] = csitDictionary[k];
                //addField(k, csitDictionary[k]);
            }
	

		   String error = string.Empty;
           foreach (string field in this.campError) {
                error = error + field + ", " ;
           }
            			
		   if(this.campError.Count()>0){
				resultDictionary[ElementNames.ERROR] =  error;
			}
			
		} catch (Exception e) {
		  Console.WriteLine(e.Message);
		}
		return resultDictionary;

	}

	private void selector(JArray jArrayValidate, JArray jArrayFormat, string value, string field) {

		Boolean validationOK = false;

		if (jArrayValidate != null) {
			validationOK = validation(jArrayValidate, value, field);
			if (validationOK) {
                if (jArrayFormat != null){
                    value = format(jArrayFormat, value, field);
				}				
			} else {
				addError(field);
			}

            if (validation(jArrayValidate, value, field)) {
				addField(field, value);
			} else {
				addError(field);
			}

		}else{
            if (jArrayFormat != null){
                value = format(jArrayFormat, value, field);
				addField(field, value);
			}			
		}
	}

	private Boolean validation(JArray jArrayValidate, string value, string field) {

		Boolean valid = false;
        JObject json = null;
        string val = value;

		for (int i = 0; i < jArrayValidate.Count; i++) {

			string function = null;
			string message = null;
            List<string> parameters = new List<string>();
            string def = null;
            json = (JObject)jArrayValidate[i];
            Dictionary<string, object> jsonDic = parseJsonToDictionary(json.ToString());

            foreach (string k in jsonDic.Keys) {
                     string key = k;

                	if (key.Equals(FUNCTION)) {
					    function = (string)jsonDic[key];
				    }
				    if (key.Equals(MESSAGE)) {
					    message = (string)jsonDic[key];
				    }
				    if (key.Equals(DEFAULT)) {
					    def = setDefaultValue((string)jsonDic[key]);
				    }
				    if (key.Equals(PARAMS)) {
					    JArray array = (JArray)jsonDic[key];
                        for (int X = 0; X < array.Count; X++)
                        {
						    parameters.Add((string)array[X]);
					    }
				    }
            }

			if (function.Equals("notEmpty")) {
				valid = isNotEmpty(val);
				if (!valid) {
					if (def != null) {
						addField(field, def);
						val = def;
						valid = true;
					} else {
						addField(field, message);
					}
				}
			}

			if (function.Equals("regex")) {
                valid = regexValidate(parameters[0], val);
				if (!valid) {
					if (def != null) {
						addField(field, def);
                        val = def;
                        valid = true;
					} else {
						addField(field, message);
					}
				}
			}

		}
		return valid;
	}


    private String format(JArray jArrayFormat, string value, string field)
    {
        string result = value;
        JObject json = null;

        for (int i = 0; i < jArrayFormat.Count; i++) {

			int function = 0 ;
            string message = null;
            List<string> parameters = new List<string>();
            json = (JObject)jArrayFormat[i];
            Dictionary<string, object> jsonDic = parseJsonToDictionary(json.ToString());

            foreach (string k in jsonDic.Keys) {
                string key = k;

                if (key.Equals(FUNCTION)) {
                    String fun = (string)jsonDic[key];
                    function = keyDictionary[fun];
                }
                if (key.Equals(MESSAGE)) {
                    message = (string)jsonDic[key];
                }
                if (key.Equals(PARAMS)){
                    JArray array = (JArray)jsonDic[key];
                    for (int X = 0; X < array.Count; X++)
                    {
                        parameters.Add((string)array[X]);
                    }
                }
            }

			switch (function) {
			case 1:
                    result = clean(result);
				break;

			case 2:
                result = truncate(result, Convert.ToInt32(parameters[0]));
				break;

			case 3:
                result = hardcode(result, parameters[0]);
				break;

			case 4:
                result = upper(result);
				break;

			case 5:
                result = regexFormat(result, parameters[0]);
				break;

			case 6:
                result = phone(result);
				break;

			case 7:
                CSITDictionary[field] = result;
				break;

            case 8:
                result = truncateLeft(result, Convert.ToInt32(parameters[0]));
                break;

			}
		}
             
		return result;
	}


        private string hardcode(string value, string hardcode){
            if (hardcode != null && !hardcode.Equals(string.Empty)){
                value = hardcode;
            }
            return value;
        }

        private Boolean isNotEmpty(string value) {
            if (value != null && !value.Equals(string.Empty)){
                return true;
            }
            return false;
        }

        private string truncate(string value, int size){
            if (value != null && !value.Equals(string.Empty)){
                value.Trim();
                if (value.Length > size){
                    value = value.Substring(0, size);
                }
            }
            return value;
        }

        private string truncateLeft(string value, int size) {
            if (value != null && !value.Equals(string.Empty)){
                value.Trim();
                if (value.Length > size) {
                    int i = value.Length - size;
                    value = value.Substring(i, size);
                }
            }
            return value;
        }

        private string upper(string value){
            if (value != null && !value.Equals(string.Empty)) {
                value.Trim();
                value.ToUpper();
            }
            return value;
        }


        private String phone(String value){

            if (value != null && !value.Equals(string.Empty)){

                value = clean(value).Trim();
                value = Regex.Replace(value, PHONE, string.Empty, RegexOptions.None); 

                if (value.Length.Equals(8)) {
                    value = "5411" + value;
                }

                if (value.Substring(0).Equals("0")){
                    value = "54" + value;
                }

                if (value.Length < 6) {
                    value = "5411" + value;
                }
            }
            return value;
        }


        private string clean(string value){

            if (value != null && !value.Equals(string.Empty)) {
                value = value.Trim();
                //value = Regex.Replace(value, ASCII, string.Empty, RegexOptions.None); 
                value = Regex.Replace(value, PUNCT, string.Empty, RegexOptions.None); 
            }
            return value;
        }

        private Boolean regexValidate(string pattern, string value) {

           Boolean result = false;
           if (value != null && !value.Equals(string.Empty)){

               Regex regex = new Regex(pattern);
               Match match = regex.Match(value);
               if (match.Success){
                   result = true;
               }
            }
            return result;
        }

        private string regexFormat(string value, string pattern) {
            if (value != null && !value.Equals(string.Empty)){
                value = Regex.Replace(value, pattern, string.Empty, RegexOptions.None); 
            }
            return value;
        }

        private Dictionary<string, string> csitFormat(int size){

            Dictionary<string, string> mapResult = new Dictionary<string, string>();
            string value = null;
            int sizeDescription = 0;

            if (this.CSITDictionary.ContainsKey(CSIT_DESCRIPTION)){
                value = this.CSITDictionary[CSIT_DESCRIPTION];
                value = cutDescription(value, size);

                string[] aux = value.Split(NUMERAL);
                sizeDescription = aux.Length;

                foreach (string k in this.CSITDictionary.Keys) {
                    string key = k;
                    string val = this.CSITDictionary[key];
                    mapResult[key] = genericCutCsit(val, sizeDescription);
                }

            }else{
                addError(CSIT_DESCRIPTION);
            }
            return mapResult;
        }

        private string cutDescription(string values, int size){

            string result = "";
            string[] arrayValues = values.Split(NUMERAL);
            string[] aux = new string[arrayValues.Length];

            int count = arrayValues.Length;
            int x = (size / count) - 1;

            if (x >= 20) {
                for (int i = 0; i < arrayValues.Length; i++){
                    aux[i] = truncate(arrayValues[i].Trim(), x) + NUMERAL;
                    result = result + aux[i];
                }
            }else{
                int cantProduct = (size / 21);
                for (int i = 0; i < cantProduct; i++) {
                    aux[i] = truncate(arrayValues[i].Trim(), 20) + NUMERAL;
                    result = result + aux[i];
                }
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }


        private string genericCutCsit(string values, int cant){

            string result = "";
            string[] arrayValues = values.Split(NUMERAL);
            string[] aux = new string[cant];

            for (int i = 0; i < arrayValues.Length; i++){
                if (i < cant){
                    aux[i] = truncate(arrayValues[i].Trim(), 20) + NUMERAL;
                    result = result + aux[i];
                }
            }

            result = result.Substring(0, result.Length - 1);
            return result;
        }

        private void addError(string field){
            if (!this.campError.Contains(field)){
                this.campError.Add(field);
            }
        }

        private void addField(string field, string value){
            if (!this.resultDictionary.ContainsKey(field)){
                this.resultDictionary[field] = value;
            }
        }

        private string setDefaultValue(string value) {
            string result = null;
            if (value.Equals("random")) {
                Random rd = new Random();
                int C = rd.Next(9999);
                result = "ABC" + C;
            }else{
                if (value.Equals("findState")) {
                    result = findState();
                } else{
                    result = value;
                }
            }
            return result;
        }

        private void setState(){
            this.stateCodeDictionary = new Dictionary<string, string>();
            this.stateCodeDictionary["A"] = "4400";
            this.stateCodeDictionary["B"] = "1900";
            this.stateCodeDictionary["C"] = "1000";
            this.stateCodeDictionary["D"] = "5700";
            this.stateCodeDictionary["E"] = "3100";
            this.stateCodeDictionary["F"] = "5300";
            this.stateCodeDictionary["G"] = "4200";
            this.stateCodeDictionary["H"] = "3500";
            this.stateCodeDictionary["J"] = "5400";
            this.stateCodeDictionary["K"] = "4700";
            this.stateCodeDictionary["L"] = "6300";
            this.stateCodeDictionary["M"] = "5500";
            this.stateCodeDictionary["N"] = "3300";
            this.stateCodeDictionary["P"] = "3600";
            this.stateCodeDictionary["Q"] = "8300";
            this.stateCodeDictionary["R"] = "8500";
            this.stateCodeDictionary["S"] = "3000";
            this.stateCodeDictionary["T"] = "4001";
            this.stateCodeDictionary["U"] = "9103";
            this.stateCodeDictionary["V"] = "9410";
            this.stateCodeDictionary["W"] = "3400";
            this.stateCodeDictionary["X"] = "5000";
            this.stateCodeDictionary["Y"] = "4600";
            this.stateCodeDictionary["Z"] = "9400";             
        }

        private void setKeyMap(){
            this.keyDictionary = new Dictionary<string, int>();
            this.keyDictionary["clean"] = 1;
            this.keyDictionary["truncate"] = 2;
            this.keyDictionary["hardcode"] = 3;
            this.keyDictionary["upper"] = 4;
            this.keyDictionary["regex"] = 5;
            this.keyDictionary["phone"] = 6;
            this.keyDictionary["csitFormat"] = 7;
            this.keyDictionary["truncateLeft"] = 8;
        }

        private String findState() {
            String result = "1000";

            if (this.resultDictionary.ContainsKey(CSBTSTATE)){
                if (this.stateCodeDictionary.ContainsKey(this.resultDictionary[CSBTSTATE])){
                    result = this.stateCodeDictionary[(this.resultDictionary[CSBTSTATE])];
                }
            }
            return result;
        }



    }
}

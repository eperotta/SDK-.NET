using System.Collections.Generic;

namespace TodoPagoConnector{ 

    public class BSA : RestConnector {
        public BSA(string endpoint, Dictionary<string, string> headders)
              :base(endpoint,headders){   
        }

    }
}

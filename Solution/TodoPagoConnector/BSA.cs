using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using TodoPagoConnector.Model;
using TodoPagoConnector.Exceptions;
using TodoPagoConnector.Utils;
using TodoPagoConnector.Operations;

namespace TodoPagoConnector{ 

    public class BSA : RestConnector {
        public BSA(string endpoint, Dictionary<string, string> headders)
              :base(endpoint,headders){   
        }

    }
}

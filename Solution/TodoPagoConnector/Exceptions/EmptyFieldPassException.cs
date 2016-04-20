using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoPagoConnector.Exceptions
{
    public class EmptyFieldPassException : EmptyFieldException
    {
         public EmptyFieldPassException(String message)
            :base(message)
        {
        }

         public EmptyFieldPassException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

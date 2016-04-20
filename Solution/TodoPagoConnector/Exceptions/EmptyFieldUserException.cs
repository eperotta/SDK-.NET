using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoPagoConnector.Exceptions
{
    public class EmptyFieldUserException : EmptyFieldException
    {
         public EmptyFieldUserException(String message)
            :base(message)
        {
        }

         public EmptyFieldUserException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

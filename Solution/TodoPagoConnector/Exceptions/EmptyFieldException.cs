using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoPagoConnector.Exceptions
{
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException(String message)
            :base(message)
        {
        }

        public EmptyFieldException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

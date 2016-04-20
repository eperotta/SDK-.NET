using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoPagoConnector.Exceptions
{
    public class ResponseException : Exception
    {
        public ResponseException(String message)

            : base(message)
        {
        }

        public ResponseException(String message, Exception inner)
            : base(message, inner)
        {
        }

    }
}

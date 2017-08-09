namespace TPUnitTest.Mock.Data
{
    internal class RefundDataProvider
    {
        public static string ReturnRequestOkResponse()
        {
            return "{\"ReturnResponse\":{\"StatusCode\":2011,\"StatusMessage\":\"Devolucion OK\",\"AuthorizationKey\":\"a61de00b-c118-2688-77b0-16dbe5799913\",\"AUTHORIZATIONCODE\":654402}}";
        }

        public static string ReturnRequestFailResponse()
        {
            return "{\"ReturnResponse\":{\"StatusCode\":2013,\"StatusMessage\":\"No es posible obtener los importes de las comisiones para realizar la devolucion\",\"AuthorizationKey\":null,\"AUTHORIZATIONCODE\":null}}";
        }

        public static string ReturnRequest702Response()
        {
            return "{\"ReturnResponse\":{\"StatusCode\":702,\"StatusMessage\":\"Cuenta de Vendedor Invalida\",\"AuthorizationKey\":null,\"AUTHORIZATIONCODE\":null}}";
        }
        
        public static string VoidRequestOkResponse()
        {
            return "{\"VoidResponse\":{\"StatusCode\":2011,\"StatusMessage\":\"Devolucion OK\",\"AuthorizationKey\":\"a61de00b-c118-2688-77b0-16dbe5799913\",\"AUTHORIZATIONCODE\":654402}}";
        }

        public static string VoidRequestFailResponse()
        {
            return "{\"VoidResponse\":{\"StatusCode\":2013,\"StatusMessage\":\"No es posible obtener los importes de las comisiones para realizar la devolucion\",\"AuthorizationKey\":null,\"AUTHORIZATIONCODE\":null}}";
        }

        public static string VoidRequest702Response()
        {
            return "{\"VoidResponse\":{\"StatusCode\":702,\"StatusMessage\":\"Cuenta de Vendedor Invalida\",\"AuthorizationKey\":null,\"AUTHORIZATIONCODE\":null}}";
        }
    }
}

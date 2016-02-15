// -----------------------------------------------------------------------
// <copyright file="AuthorizeBinding.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TodoPagoConnector
{
    using System.ServiceModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class OperationsBinding : BasicHttpBinding
    {
        public OperationsBinding()
            : base()
        {
            this.Security.Mode = BasicHttpSecurityMode.Transport;
        }
    }

    public class OperationsEndpoint : EndpointAddress
    {
        public OperationsEndpoint(string uri)
            : base(uri + @"/Operations.OperationsHttpsSoap12Endpoint")
        {
        }
    }
}

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
    public class AuthorizeBinding: BasicHttpBinding
    {
        public AuthorizeBinding(): base()
        {
            this.Security.Mode = BasicHttpSecurityMode.Transport;
        }
    }

    public class AuthorizeEndpoint : EndpointAddress
    {
        public AuthorizeEndpoint(string uri):base(uri+@"/Authorize.AuthorizeHttpsSoap12Endpoint")
        {
        }
    }
}

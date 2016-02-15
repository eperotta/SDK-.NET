// -----------------------------------------------------------------------
// <copyright file="HeaderHttpExtension.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TodoPagoConnector.Service_Extensions
{
    using System.ServiceModel;
    using System.ServiceModel.Channels;
using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class HeaderHttpExtension
    {
        /// <summary>
        /// Scope is being passed
        /// </summary>
        /// <param name="scope"></param>
        public static void AddCustomHeaderUserInformation(OperationContextScope scope, Dictionary<string, string> headers)
        {
            //Add the basic userId
            HttpRequestMessageProperty requestProperty = new HttpRequestMessageProperty();

            foreach (var headerKey in headers.Keys)
            {
                requestProperty.Headers.Add(headerKey, headers[headerKey]);   
            }

            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestProperty;
        }
    }
}

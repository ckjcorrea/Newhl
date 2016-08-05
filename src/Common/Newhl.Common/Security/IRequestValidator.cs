using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Newhl.Common.Security
{
    /// <summary>
    /// Interface for request validate
    /// </summary>
    public interface IRequestValidator
    {
        /// <summary>
        /// Authorizes the web request.
        /// </summary>
        /// <param name="webRequest">The web request.</param><param name="requestBody">The request body.</param>
        void Authorize(WebRequest webRequest, string requestBody);
    }
}
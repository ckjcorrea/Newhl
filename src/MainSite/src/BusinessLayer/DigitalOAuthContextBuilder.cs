using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DevDefined.OAuth.Framework;

namespace AlwaysMoveForward.OAuth.BusinessLayer
{
    /// <summary>
    /// This class servers as a factory for the DevDefiend OAuth context builder so we can adjust the url when the server is running behind a load balancer
    /// </summary>
    public class AMFOAuthContextBuilder
    {
        /// <summary>
        /// This defines the setting in the .config file where the load balancer endpoints are defined
        /// </summary>
        public const string LoadBalancerEndpointsSetting = "LoadBalancerEndpoints";

        /// <summary>
        /// The OAuthContextBuilder takes a delegate parameter for adjusting the uri in its constructor.  This method is used when running behind a load balancer
        /// to strip out the port 
        /// </summary>
        /// <param name="originalUri">The original url as reported by the HttpRequestBase</param>
        /// <returns>A url with the port stripped out</returns>
        public static Uri RemovePort(Uri originalUri)
        {
            return new Uri(originalUri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                               UriFormat.UriEscaped));
        }

        /// <summary>
        /// Wrap the OAuthContextBuilder class so that we create it with the RemovePort delegate when the incoming request
        /// comes from a loadbalancer url
        /// </summary>
        /// <param name="request">The http request</param>
        /// <param name="loadBalancerEndpoints">An array of loadbalancer urls</param>
        /// <returns>A Dev defined OAuth context</returns>
        public static IOAuthContext FromHttpRequest(HttpRequestBase request, string[] loadBalancerEndpoints)
        {
            if (loadBalancerEndpoints != null)
            {
                if (loadBalancerEndpoints.Contains(request.Url.Host))
                {
                    return new OAuthContextBuilder(AMFOAuthContextBuilder.RemovePort).FromHttpRequest(request);
                }
            }

            return new OAuthContextBuilder().FromHttpRequest(request);
        }
    }
}

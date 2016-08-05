using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using DevDefined.OAuth.Provider.Inspectors;
using DevDefined.OAuth.Framework.Signing;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;

namespace AlwaysMoveForward.OAuth.Web.Code
{
    /// <summary>
    /// A factory for creating the DevDefined OAuth provider
    /// </summary>
    public class OAuthServiceProviderFactory
    {
        /// <summary>
        /// Get the OAuth provider using the token and consumer service instance
        /// </summary>
        /// <param name="tokenService">A DevDefined tokenstorage implementation</param>
        /// <param name="consumerService">A dev defined consumer storage implementation</param>
        /// <returns>A Dev Defined OAuth provider</returns>
        public static OAuthProvider GetServiceProvider(ITokenService tokenService, IConsumerService consumerService)
        {
             OAuthProvider retVal = null;

            if(tokenService != null && consumerService != null)
            {
                retVal = new OAuthProvider(
                    tokenService,
                    new SignatureValidationInspector(consumerService),
                    new NonceStoreInspector(consumerService),
                    new TimestampRangeInspector(new TimeSpan(1, 0, 0)),
                    new ConsumerValidationInspector(consumerService),
                    new OAuth10AInspector(tokenService));
            }

            return retVal;
        }
    }
}
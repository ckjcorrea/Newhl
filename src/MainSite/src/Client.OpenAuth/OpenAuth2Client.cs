using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.Messaging;

namespace AlwaysMoveForward.OAuth.Client.OpenAuth
{
    public class OpenAuth2Client
    {
        public static WebServerClient CreatWebServerClient(string authorizationEndpoint, string tokenEndpoint)
        {
            var desc = GetAuthServerDescription(authorizationEndpoint, tokenEndpoint);
            var client = new WebServerClient(desc, clientIdentifier: "xyz.apps.googleusercontent.com");
            client.ClientCredentialApplicator = ClientCredentialApplicator.PostParameter("some_password");
            return client;
        }

        public static UserAgentClient CreatUserAgentClient(string authorizationEndpoint, string tokenEndpoint, string clientIdentifier)
        {
            var desc = GetAuthServerDescription(authorizationEndpoint, tokenEndpoint);
            return new UserAgentClient(GetAuthServerDescription(authorizationEndpoint, tokenEndpoint), clientIdentifier);    
        }

        public static AuthorizationServerDescription GetAuthServerDescription(string authorizationEndpoint, string tokenEndpoint)
        {
            var authServerDescription = new AuthorizationServerDescription();
            authServerDescription.AuthorizationEndpoint = new Uri(authorizationEndpoint);
            authServerDescription.TokenEndpoint = new Uri(tokenEndpoint);
            authServerDescription.ProtocolVersion = ProtocolVersion.V20;
            return authServerDescription;
        }

        private static IAuthorizationState GetAccessTokenFromOwnAuthSvr(string authorizationEndpoint, string tokenEndpoint)
        {
            var client = new UserAgentClient(GetAuthServerDescription(authorizationEndpoint, tokenEndpoint), clientIdentifier: "RP");

            client.ClientCredentialApplicator =
                         ClientCredentialApplicator.PostParameter("data!");

            var token = client.ExchangeUserCredentialForToken(
                          "Max Muster", "test123", new[] { "http://localhost/demo" });

            return token;
        }     
    }
}

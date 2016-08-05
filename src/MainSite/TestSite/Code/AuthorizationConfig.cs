using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebPages.OAuth;
using VP.Digital.Common.Security;
using AlwaysMoveForward.OAuth.Client.OpenAuth;
using AlwaysMoveForward.OAuth.Contracts.Configuration;

namespace TestSite.Code
{
    public class AuthorizationConfig
    {
        public static void RegisterOAuthConfig()
        {
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.GetInstance("Vistaprint/Digital/MonolithEndpoints"); //EndpointConfiguration.GetInstance("Vistaprint/Digital/OpenAuthOAuthEndpoints");
            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance("Vistaprint/Digital/MonolithKeys"); //OAuthKeyConfiguration.GetInstance("Vistaprint/Digital/OpenAuthOAuthKeys");

            OAuthWebSecurity.RegisterClient(new VistaprintOAuthClient(endpointConfiguration, oauthConfiguration.ConsumerKey, oauthConfiguration.ConsumerSecret));
        }
    }
}
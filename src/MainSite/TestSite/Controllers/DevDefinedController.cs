using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using RestSharp;
using RestSharp.Authenticators;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Contracts.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client.DevDefined;
using TestSite.Models;

namespace TestSite.Controllers
{
    public class DevDefinedController : ControllerBase
    {
        public ActionResult Index()
        {
            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.GetInstance();

            ConsumerKeyModel model = new ConsumerKeyModel();
            model.ConsumerKey = oauthConfiguration.ConsumerKey;
            model.ConsumerSecret = oauthConfiguration.ConsumerSecret;
            model.CallbackUrl = this.Request.Url.Scheme + "://" + this.Request.Url.Authority + "/DevDefined/OAuthCallback";

            EndpointModel endpointModel = new EndpointModel();
            endpointModel.ServiceUri = endpointConfiguration.ServiceUri;
            endpointModel.RequestTokenUri = endpointConfiguration.RequestTokenUri;
            endpointModel.AuthorizationUri = endpointConfiguration.AuthorizationUri;
            endpointModel.AccessTokenUri = endpointConfiguration.AccessTokenUri;
            model.EndpointModel = endpointModel;

            return View(model);
        }

        public void GetRequestToken(string consumerKey, string consumerSecret, string callbackUrl, string serviceUri, string requestTokenUri, string authorizationUri, string accessTokenUri)
        {
            RequestTokenModel model = new RequestTokenModel();
            model.ConsumerKey = consumerKey;
            model.ConsumerSecret = consumerSecret;

            EndpointModel endpointModel = new EndpointModel();
            endpointModel.ServiceUri = serviceUri;
            endpointModel.RequestTokenUri = requestTokenUri;
            endpointModel.AuthorizationUri = authorizationUri;
            endpointModel.AccessTokenUri = accessTokenUri;
            model.EndpointModel = endpointModel;

            OAuthClientBase oauthClient = OAuthClient.CreateClient(consumerKey, consumerSecret, endpointModel);

            if (oauthClient != null)
            {
                IOAuthToken requestToken = oauthClient.GetRequestToken(this.GenerateRealm(TestUserId, "artie@test.com"), callbackUrl);
                model.Token = requestToken.Token;
                model.Secret = requestToken.Secret;
            }

            Session[model.Token] = model;

            string authorizationUrl = oauthClient.GetUserAuthorizationUrl(model);

            this.Response.Redirect(authorizationUrl, false);
        }

        public ActionResult OAuthCallback(string oauth_token, string oauth_verifier)
        {
            RequestTokenModel model = new RequestTokenModel();

            string requestTokenString = Request[Parameters.OAuth_Token];
            string verifier = Request[Parameters.OAuth_Verifier];

            RequestTokenModel storedRequestTokenModel = (RequestTokenModel)Session[requestTokenString];
            model.ConsumerKey = storedRequestTokenModel.ConsumerKey;
            model.ConsumerSecret = storedRequestTokenModel.ConsumerSecret;
            model.EndpointModel = storedRequestTokenModel.EndpointModel;

            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();

            OAuthClientBase oauthClient = OAuthClient.CreateClient(oauthConfiguration.ConsumerKey, oauthConfiguration.ConsumerSecret, storedRequestTokenModel.EndpointModel);

            if (string.IsNullOrEmpty(verifier))
            {
                throw new Exception("Expected a non-empty verifier value");
            }

            IOAuthToken accessToken;

            try
            {
                accessToken = oauthClient.ExchangeRequestTokenForAccessToken(storedRequestTokenModel, verifier);
                model.Token = accessToken.Token;
                model.Secret = accessToken.Secret;
            }
            catch (OAuthException authEx)
            {
                Session["problem"] = authEx.Report;
                Response.Redirect("AccessDenied.aspx");
            }

            return View(model);
        }

        public ActionResult MakeOAuthRequest(string accessToken, string accessTokenSecret, string endpointUri, string endpointAction)
        {
            OAuthProxyResponseModel retVal = new OAuthProxyResponseModel();

            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.GetInstance();
            EndpointModel endpointModel = new EndpointModel();
            endpointModel.ServiceUri = endpointUri;
            endpointModel.RequestTokenUri = endpointConfiguration.RequestTokenUri;
            endpointModel.AuthorizationUri = endpointConfiguration.AuthorizationUri;
            endpointModel.AccessTokenUri = endpointConfiguration.AccessTokenUri;

            if (!endpointUri.EndsWith(@"/") && !endpointAction.StartsWith(@"/"))
            {
                endpointUri += "/";
            }

            DefaultOAuthToken oauthToken = new DefaultOAuthToken();
            oauthToken.Token = accessToken;
            oauthToken.Secret = accessTokenSecret;

            WebRequest request = HttpWebRequest.Create(endpointUri + endpointAction);
            request.Method = "GET";
            request.ContentType = "application/json";
            OAuthClient oauthClient = new OAuthClient(oauthConfiguration.ConsumerKey, oauthConfiguration.ConsumerSecret, endpointModel);
            retVal.Response = oauthClient.ExecuteAuthorizedRequest(endpointUri + endpointAction, "application/json", string.Empty, oauthToken);

            return View("MakeOAuthRequest", retVal);
        }

        public ActionResult GetInlineOAuthToken(string consumerKey, string consumerSecret, string callbackUrl, string serviceUri, string requestTokenUri, string authorizationUri, string accessTokenUri)
        {
            RequestTokenModel model = new RequestTokenModel();
            model.ConsumerKey = consumerKey;
            model.ConsumerSecret = consumerSecret;

            EndpointModel endpointModel = new EndpointModel();
            endpointModel.ServiceUri = serviceUri;
            endpointModel.RequestTokenUri = requestTokenUri;
            endpointModel.AuthorizationUri = authorizationUri;
            endpointModel.AccessTokenUri = accessTokenUri;
            model.EndpointModel = endpointModel;

            OAuthClient oauthClient = new OAuthClient(model.ConsumerKey, model.ConsumerSecret, model.EndpointModel);

            try
            {
                IOAuthToken accessToken = oauthClient.GetInlineAccessToken(Realm.GetDefault());
                model.Token = accessToken.Token;
                model.Secret = accessToken.Secret;
            }
            catch (OAuthException authEx)
            {
                Session["problem"] = authEx.Report;
                Response.Redirect("AccessDenied.aspx");
            }

            return View("OauthCallback", model);
        }
    }
}
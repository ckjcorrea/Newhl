using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using RestSharp;
using RestSharp.Authenticators;
using AlwaysMoveForward.OAuth.Common;
using AlwaysMoveForward.OAuth.Client.RestSharp;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Contracts.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using TestSite.Models;

namespace TestSite.Controllers
{
    public class RestSharpController : ControllerBase
    {
        public ActionResult Index()
        {
            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.GetInstance();

            ConsumerKeyModel model = new ConsumerKeyModel();
            model.ConsumerKey = oauthConfiguration.ConsumerKey;
            model.ConsumerSecret = oauthConfiguration.ConsumerSecret;
            model.CallbackUrl = this.Request.Url.Scheme + "://" + this.Request.Url.Authority + "/RestSharp/OAuthCallback";

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

            AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient oauthClient = new AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient("", model.ConsumerKey, model.ConsumerSecret, endpointModel);

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

            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();

            AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient oauthClient = new AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient("", storedRequestTokenModel.ConsumerKey, storedRequestTokenModel.ConsumerSecret, storedRequestTokenModel.EndpointModel);

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

            AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient oauthClient = new AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient("", model.ConsumerKey, model.ConsumerSecret, model.EndpointModel);

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

        public ActionResult MakeOAuthRequest(string accessToken, string accessTokenSecret, string endpointUri, string endpointAction)
        {
            OAuthProxyResponseModel retVal = new OAuthProxyResponseModel();

            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();

            RestClient restClient = new RestClient(endpointUri);
            restClient.Authenticator = OAuth1Authenticator.ForProtectedResource(oauthConfiguration.ConsumerKey, oauthConfiguration.ConsumerSecret, accessToken, accessTokenSecret);
            
            string[] parameterItems = null;

            if (endpointAction.Contains("?"))
            {
                string[] actionElements = endpointAction.Split('?');

                parameterItems = actionElements[1].Split('&');

                endpointAction = actionElements[0];
            }

            RestRequest request = new RestRequest(endpointAction, Method.GET);
            request.RequestFormat = DataFormat.Json;

            if (parameterItems != null)
            {
                for (int i = 0; i < parameterItems.Length; i++)
                {
                    string[] parameterElements = parameterItems[i].Split('=');

                    request.AddParameter(parameterElements[0], parameterElements[1]);
                }
            }

            IRestResponse response = restClient.Execute(request);

            if (response != null)
            {
                retVal.Response = response.Content;
            }

            return View("MakeOAuthRequest", retVal);
        }
    }
}
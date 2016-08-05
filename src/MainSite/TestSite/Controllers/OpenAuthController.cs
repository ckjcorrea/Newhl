using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth;
using Microsoft.Web.WebPages.OAuth;
using VP.Digital.Common.Security;
using VP.Digital.Common.Utilities.Logging;
using AlwaysMoveForward.OAuth.Client.OpenAuth;
using AlwaysMoveForward.OAuth.Common;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Contracts.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using TestSite.Models;
using TestSite.Code;

namespace TestSite.Controllers
{
    public class OpenAuthController : ControllerBase
    {
        public DefaultDigitalSecurityPrincipal CurrentPrincipal
        {
            get
            {
                DefaultDigitalSecurityPrincipal retVal = System.Threading.Thread.CurrentPrincipal as DefaultDigitalSecurityPrincipal;

                if (retVal == null)
                {
                    try
                    {
                        retVal = new DefaultDigitalSecurityPrincipal(null);
                        System.Threading.Thread.CurrentPrincipal = retVal;
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger(this.GetType().Name).Error(e);
                    }
                }

                return retVal;
            }

            set
            {
                System.Threading.Thread.CurrentPrincipal = value;
                this.HttpContext.User = value;
            }
        }
        
        [RequestAuthenticationFilter]
        public virtual ActionResult Index(string returnUrl = "")
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("/Callback/1");
            }

            if (this.CurrentPrincipal.IsAuthenticated == true)
            {
                return this.Redirect(returnUrl);
            }

            return new OAuthSignIn
            {
                ReturnUrl = Url.Action("Callback", new { returnUrl = returnUrl })
            };
        }

        // Callback page invoked when the user has completed the oauth action.
        public virtual ActionResult Callback(string returnUrl)
        {
            if (this.Request[VistaprintOAuthClient.FailedQueryParameter] == "1")
            {
                return this.Failure();
            }

            DotNetOpenAuth.AspNet.AuthenticationResult authResult =
                OAuthWebSecurity.VerifyAuthentication(
                    Url.Action("Callback", new { returnUrl = returnUrl }));

            if (!authResult.IsSuccessful)
            {
                return this.RedirectToAction("Failure", new { returnUrl = returnUrl });
            }

            return this.Redirect(returnUrl);
        }

        // something failed during log in. 
        // usually this means the user didn't grant permission.
        public virtual ActionResult Failure()
        {
            return this.View();
        }

        private class OAuthSignIn : ActionResult
        {
            public string ReturnUrl { private get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(VistaprintOAuthClient.AuthProviderName, this.ReturnUrl);
            }
        }        

        private ConsumerKeyModel GenerateConsumerKeyModel()
        {
            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();

            ConsumerKeyModel model = new ConsumerKeyModel();
            model.ConsumerKey = oauthConfiguration.ConsumerKey;
            model.ConsumerSecret = oauthConfiguration.ConsumerSecret;
            model.CallbackUrl = this.Request.Url.Scheme + "://" + this.Request.Url.Authority + "/OpenAuth/OAuthCallback";

            model.EndpointModel = this.GenerateEndPointModel();

            return model;
        }

        public ActionResult ExplicitIndex()
        {
            ConsumerKeyModel model = this.GenerateConsumerKeyModel();
            model.CallbackUrl = this.Request.Url.Scheme + "://" + this.Request.Url.Authority + "/OpenAuth/OAuthCallback";

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

            Session["endpoints"] = model;

            OAuthClient oauthClient = new OAuthClient(endpointModel, TestSite.MvcApplication.GetTokenManager(consumerKey, consumerSecret));

            if (oauthClient != null)
            {
                Realm realm = this.GenerateRealm(OpenAuthController.TestUserId, "artieopenauth@test.com");
                oauthClient.GetRequestToken(realm, callbackUrl);
            }
        }       

        public ActionResult OAuthCallback(string oauth_token, string oauth_verifier)
        {
            RequestTokenModel model = new RequestTokenModel();
            RequestTokenModel sessionModel = Session["endpoints"] as RequestTokenModel;
            model = sessionModel;

            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();

            if (string.IsNullOrEmpty(oauth_verifier))
            {
                throw new Exception("Expected a non-empty verifier value");
            }

            OAuthClient oauthClient = new OAuthClient(sessionModel.EndpointModel, TestSite.MvcApplication.GetTokenManager(sessionModel.ConsumerKey, sessionModel.ConsumerSecret));

            if (oauthClient != null)
            {
                IOAuthToken oauthToken = oauthClient.ExchangeRequestTokenForAccessToken(oauth_verifier);

                if (oauthToken != null)
                {
                    model.Token = oauthToken.Token;
                    model.Secret = oauthToken.Secret;       
                }
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

            OAuthClient oauthClient = new OAuthClient(endpointModel, null);

            if (oauthClient != null)
            {
                WebConsumer webConsumer = oauthClient.CreateConsumer(TestSite.MvcApplication.GetTokenManager(oauthConfiguration.ConsumerKey, oauthConfiguration.ConsumerSecret));
                System.Net.HttpWebRequest oAuthReq = webConsumer.PrepareAuthorizedRequest(new DotNetOpenAuth.Messaging.MessageReceivingEndpoint(endpointUri + endpointAction, DotNetOpenAuth.Messaging.HttpDeliveryMethods.GetRequest | DotNetOpenAuth.Messaging.HttpDeliveryMethods.AuthorizationHeaderRequest), accessToken);

                try
                {
                    // No more ProtocolViolationException!
                    using (HttpWebResponse response = (HttpWebResponse)oAuthReq.GetResponse())
                    {
                        // Get the stream containing content returned by the server.
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream);

                            // Read the content. 
                            retVal.Response = reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException we)
                {
                    retVal.Response = we.Message;
                }
            }

            return View("MakeOAuthRequest", retVal);
        }
    }
}
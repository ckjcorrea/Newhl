using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using VP.Digital.Common.Security;
using AlwaysMoveForward.OAuth.Contracts;

namespace AlwaysMoveForward.OAuth.Client.OpenAuth
{
    // we may want to move this in to the account/identity service at a future time and make it more widely usable.
    public class VistaprintOAuthClient : OAuth2Client
    {
        public static readonly string AuthProviderName = "Digital";
        public static readonly string FailedQueryParameter = "failed";

        private string requestToken = string.Empty;

        public VistaprintOAuthClient(IOAuthEndpoints oauthEndpoints, string consumerKey, string consumerSecret)
            : base(AuthProviderName)
        {
            this.OAuthEndpoints = oauthEndpoints;
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        public IOAuthEndpoints OAuthEndpoints { get; private set; }

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }

        public override void RequestAuthentication(HttpContextBase context, Uri returnUrl)
        {
            this.requestToken = this.QueryRequestToken();
            base.RequestAuthentication(context, returnUrl);
        }

        public override AuthenticationResult VerifyAuthentication(HttpContextBase context, Uri returnPageUrl)
        {
            string returnedRequestToken = context.Request.QueryString[OAuthParameters.RequestToken];
            if (string.IsNullOrEmpty(returnedRequestToken))
            {
                return AuthenticationResult.Failed;
            }

            string accessToken = this.QueryAccessToken(returnPageUrl, returnedRequestToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                return AuthenticationResult.Failed;
            }

            var userData = this.GetUserData(accessToken);
            if (userData == null)
            {
                return AuthenticationResult.Failed;
            }

            string accountId = userData[OAuthParameters.UserId];
            string userName;

            if (!userData.TryGetValue(OAuthParameters.UserName, out userName) && !userData.TryGetValue("name", out userName))
            {
                userName = accountId;
            }

            userData[OAuthParameters.AccessToken] = accessToken;

            return new AuthenticationResult(
                isSuccessful: true, provider: this.ProviderName, providerUserId: accountId, userName: userName, extraData: new Dictionary<string, string>());
        }

        protected override Uri GetServiceLoginUrl(Uri returnUrl)
        {
            long timestamp = GetTimestamp();
            string responseUri = returnUrl.AbsoluteUri;
            var failureBuilder = new UriBuilder(returnUrl.AbsoluteUri);
            failureBuilder.AppendQueryArgument(VistaprintOAuthClient.FailedQueryParameter, "1");
            var failureUri = failureBuilder.Uri.AbsoluteUri;

            var urlBuilder = new UriBuilder(this.OAuthEndpoints.GetFullAuthorizationUri());
            urlBuilder.AppendQueryArgument(OAuthParameters.ApiKey, this.ConsumerKey);
            urlBuilder.AppendQueryArgument(OAuthParameters.RequestToken, this.requestToken);
            urlBuilder.AppendQueryArgument(OAuthParameters.Timestamp, timestamp.ToString(CultureInfo.InvariantCulture));
            urlBuilder.AppendQueryArgument(OAuthParameters.Auth, GetHash(this.ConsumerSecret + this.ConsumerKey + this.requestToken + timestamp.ToString(CultureInfo.InvariantCulture)));
            urlBuilder.AppendQueryArgument(OAuthParameters.ResponseUri, responseUri);
            urlBuilder.AppendQueryArgument(OAuthParameters.FailureUri, failureUri);

            return urlBuilder.Uri;
        }

        protected override IDictionary<string, string> GetUserData(string accessToken)
        {
            // TODO: Get more data, probably from EVA
            // not sure if we want to do it here, or somewhere else.
            return new Dictionary<string, string> { { OAuthParameters.UserId, accessToken } };
        }

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
        {
            var timestamp = GetTimestamp();

            var builder = new UriBuilder(this.OAuthEndpoints.GetFullAccessTokenUri());
            builder.AppendQueryArgument(OAuthParameters.ApiKey2, this.ConsumerKey);
            builder.AppendQueryArgument(OAuthParameters.RequestToken2, this.requestToken);
            builder.AppendQueryArgument(OAuthParameters.Timestamp, timestamp.ToString(CultureInfo.InvariantCulture));
            builder.AppendQueryArgument(OAuthParameters.Auth, GetHash(this.ConsumerSecret + this.ConsumerKey + authorizationCode + timestamp.ToString(CultureInfo.InvariantCulture)));

            return GetToken(builder.Uri);
        }

        private static long GetTimestamp()
        {
            TimeSpan unix = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (long)unix.TotalSeconds;
        }

        private static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        private static string GetHash(string data)
        {
            // Get the bytes for the message
            byte[] bytes = GetBytes(data);

            // Run hash on the byte array
            SHA256 sha256 = SHA256.Create();
            byte[] computedHash = sha256.ComputeHash(bytes);

            // Convert to string
            return Convert.ToBase64String(computedHash);
        }

        private static string GetToken(Uri uri)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string data = client.DownloadString(uri);
                    if (string.IsNullOrEmpty(data))
                    {
                        return null;
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Deserialize<string>(data);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("uri", uri);
                return string.Empty;
            }
        }

        private string QueryRequestToken()
        {
            var timestamp = GetTimestamp();

            var builder = new UriBuilder(this.OAuthEndpoints.GetFullRequestTokenUri());
            builder.AppendQueryArgument(OAuthParameters.ApiKey2, this.ConsumerKey);
            builder.AppendQueryArgument(OAuthParameters.Timestamp, timestamp.ToString(CultureInfo.InvariantCulture));
            builder.AppendQueryArgument(OAuthParameters.Auth, GetHash(this.ConsumerSecret + this.ConsumerKey + timestamp.ToString(CultureInfo.InvariantCulture)));
            
            return GetToken(builder.Uri);
        }
    }
}

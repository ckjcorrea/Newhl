using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// A class representing a Request Token
    /// </summary>
    public class RequestToken : IToken, IOAuthToken
    {       
        /// <summary>
        /// The default lifetime for RequestTokens
        /// </summary>
        private const int DefaultLifetimeInMinutes = 1440;

        /// <summary>
        /// A default constructor for the class
        /// </summary>
        public RequestToken()
        {
            this.Id = 0;
            this.DateCreated = DateTime.UtcNow;
            this.ExpirationDate = DateTime.UtcNow.AddMinutes(RequestToken.DefaultLifetimeInMinutes);
            this.DateAuthorized = DateTime.MaxValue;
        }

        /// <summary>
        /// Gets or sets the Database id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the callback url
        /// </summary>
        public String CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the current access state
        /// </summary>
        public TokenState State { get; set; }

        /// <summary>
        /// Gets or sets the session handle (used by DevDefined)
        /// </summary>
        public string SessionHandle { get; set; }

        /// <summary>
        /// Gets or sets the token string
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the secret
        /// </summary>
        string IToken.TokenSecret 
        {
            get { return this.Secret; }
            set { this.Secret = value; }
        }

        /// <summary>
        /// Gets or sets the key of the Consumer
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        public Realm Realm { get; set; }

        /// <summary>
        /// An explicit implementation to satisfy the IConsumer interface
        /// </summary>
        string IConsumer.Realm
        {
            get
            {
                string retVal = string.Empty;

                if (this.Realm != null)
                {
                    retVal = this.Realm.ToString();
                }

                return retVal;
            }
            set { this.Realm = Realm.Parse(value); }
        }

        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public virtual DateTime DateCreated { get; set; }

        public bool UsedUp
        {
            get
            {
                bool retVal = false;

                if((this.IsAuthorized() == true  && this.AccessToken != null) ||
                   this.ExpirationDate < DateTime.UtcNow)
                {
                    retVal = true;
                }

                return retVal;
            }
        }
        /// <summary>
        /// Gets or sets the verifier code established during authorization
        /// </summary>
        public string VerifierCode { get; set; }

        /// <summary>
        /// Gets or sets the user that authorized this token
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user that authorized this token
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The date that the request token was authorized
        /// </summary>
        public DateTime? DateAuthorized { get; set; }

        /// <summary>
        /// Gets or sets the Access token associated with this request
        /// </summary>
        public AccessToken AccessToken { get; set; }

        public bool IsAuthorized()
        {
            bool retVal = false;

            if(!string.IsNullOrEmpty(VerifierCode) &&
                DateAuthorized != DateTime.MaxValue)
            {
                retVal = true;
            }

            return retVal;
        }
        /// <summary>
        /// Generate the full callback url from the elements contained in the class
        /// </summary>
        /// <returns></returns>
        public string GenerateCallBackUrl()
        {
            string retVal = string.Empty;

            if (this.IsAuthorized() == true)
            {
                if (!string.IsNullOrEmpty(this.CallbackUrl))
                {
                    retVal = this.CallbackUrl;

                    if (!retVal.Contains("?"))
                    {
                        retVal += "?";
                    }
                    else
                    {
                        retVal += "&";
                    }

                    retVal += Constants.TokenParameter + "=" + UriUtility.UrlEncode(this.Token);
                    retVal += "&" + Constants.VerifierCodeParameter + "=" + UriUtility.UrlEncode(this.VerifierCode);
                }
            }

            return retVal;
        }
    }
}

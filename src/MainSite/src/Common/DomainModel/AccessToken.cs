using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.OAuth.Client;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// A class to represent an Access Token
    /// </summary>
    public class AccessToken : IToken, IAccessToken
    {
        /// <summary>
        /// A default constructor for the class
        /// </summary>
        public AccessToken()
        {
            this.Id = 0;
            this.DateCreated = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the database id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The date and time that this access token was granted
        /// </summary>
        public DateTime DateGranted { get; set; }

        /// <summary>
        /// Gets or sets the expiration date
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the SessionHandle (used with DevDefined)
        /// </summary>
        public string SessionHandle { get; set; }

        /// <summary>
        /// Gets or sets the access token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the access token secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets when this access token was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the access token secret
        /// </summary>
        string IToken.TokenSecret 
        {
            get { return this.Secret; }
            set { this.Secret = value; }
        }

        /// <summary>
        /// Gets or sets the consumer key
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the realm
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

        public bool IsExpired
        {
            get
            {
                bool retVal = true;

                if (this.ExpirationDate >= DateTime.UtcNow)
                {
                    retVal = false;
                }

                return retVal;
            }
        }
    }
}

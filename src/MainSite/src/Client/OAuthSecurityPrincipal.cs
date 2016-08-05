using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Security;

namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// This is a security principal to use with oauth that has the user associated with the token,
    /// and the realm associated with the token.
    /// </summary>
    public class OAuthSecurityPrincipal : DefaultSecurityPrincipal
    {
        /// <summary>
        /// Take both the current user and the realm as inputs to the constructor
        /// </summary>
        /// <param name="localUser">The current user</param>
        /// <param name="dataRealm">The current realm</param>
        public OAuthSecurityPrincipal(User localUser, IAccessToken accessToken)
            : base(localUser)
        {
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Gets the AccessToken associated with the current principal
        /// </summary>
        public IAccessToken AccessToken { get; private set; }

        /// <summary>
        /// Gets the realm associated with the current access token;
        /// </summary>
        public Realm Realm
        {
            get
            {
                Realm retVal = null;

                if (this.AccessToken != null && this.AccessToken.Realm != null)
                {
                    retVal = this.AccessToken.Realm;
                }

                return retVal;
            }
        }

        /// <summary>
        /// The AlwaysMoveForward user generated from the Realm;
        /// </summary>
        private User realmUser;

        /// <summary>
        /// Gets the AMF user generated from the realm;
        /// </summary>
        public User RealmUser
        {
            get
            {
                if (realmUser == null)
                {
                    if(this.Realm != null)
                    {
                        realmUser = new User() { Email = this.Realm.DataName, Id = int.Parse(this.Realm.DataId) };
                    }
                }

                return realmUser;
            }
        }
    }
}

using System.Security.Principal;

namespace Newhl.Common.Security
{
    /// <summary>
    /// Implement the .Net Security principal interface.  In the past I've also had this implement IIdentity, and just pass 
    /// the calls through to the contained user, but for now I'm just implementing the IPrincipal and we'll see how it goes
    /// </summary>
    /// <typeparam name="TUser">User Type</typeparam>
    public abstract class SecurityPrincipalBase<TUser> : IPrincipal, IIdentity where TUser : class
    {
        /// <summary>
        ///  An enumeration that defines what Authentication Types Newhl supports
        /// </summary>
        public enum ImplementedAuthenticationType
        {
            /// <summary>
            /// The user was authenticated with OAuth
            /// </summary>
            Login,
            OAuth,
        }

        /// <summary>
        /// The constructor for this principal class.  It takes an instance of  TUser that implements IIdentity.
        /// </summary>
        /// <param name="user">User Type</param>
        public SecurityPrincipalBase(TUser user) : this(user, ImplementedAuthenticationType.OAuth) { }

        /// <summary>
        /// A constructor that allows the caller to define the Authentication type used to authenticate the user
        /// </summary>
        /// <param name="user">User Type</param>
        /// <param name="authenticationType">Authentication Type</param>
        public SecurityPrincipalBase(TUser user, ImplementedAuthenticationType authenticationType)
        {
            this.User = user;
            this.AuthenticationType = authenticationType.ToString();
        }

        /// <summary>
        ///  Gets the current user
        /// </summary>
        public TUser User { get; private set; }

        #region IIdentity

        /// <summary>
        /// Gets the Authentication type used, default to OAuth as that will be the preferred
        /// </summary>
        public string AuthenticationType { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current Identity is Authenticated, since there isn't any user, It can't be authenticated yet.
        /// </summary>
        public virtual bool IsAuthenticated
        {
            get
            {
                bool retVal = false;

                if (this.User != null)
                {
                    retVal = true;
                }

                return retVal;
            }
        }

        /// <summary>
        /// Gets the name of the user.  Default this to string.Empty since how to get the name is dependant upon the TUser object.
        /// One possibility is to have an interface to return the name, but that is sort of what IIdentity is for.
        /// </summary>
        public abstract string Name { get; }

        #endregion

        #region IPrincipal

        /// <summary>
        /// Gets the contained IIdentity
        /// </summary>
        public IIdentity Identity
        {
            get { return this; }
        }

        /// <summary>
        /// Determines if this user in a specific role.  For now just return true.  In general the contained user will have a list of roles
        /// the user is in that you look through for the specific role.  Since we aren't quite sure how that will pan out just yet here
        /// in Newhl I'm holding off on a specific role container.
        /// </summary>
        /// <param name="role">Role name to check</param>
        /// <returns>Whether or not it's in a particular role</returns>
        public virtual bool IsInRole(string role)
        {
            return true;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "RequestTokens")]
    public class RequestToken
    {
        /// <summary>
        /// Defines the Id field name for creating queries
        /// </summary>
        public const string IdFieldName = "Id";

        /// <summary>
        /// Defines the token field name for creating queries
        /// </summary>
        public const string TokenFieldName = "Token";

        /// <summary>
        /// Defines the consumer key field name for creating queries
        /// </summary>
        public const string ConsumerKeyFieldName = "ConsumerKey";

        /// <summary>
        /// Defines the access token field name for creating queries
        /// </summary>
        public const string AccessTokenFieldName = "AccessToken";      

        /// <summary>
        /// Defines the request token field name that defines the created date
        /// </summary>
        public const string DateCreatedFieldName = "DateCreated";

        /// <summary>
        /// A string to represent the verifier code field name for creating queries
        /// </summary>
        public const string VerifierCodeFieldName = "VerifierCode";

        /// <summary>
        /// A string to represent the verifier code field name for creating queries
        /// </summary>
        public const string UserIdFieldName = "UserId";

        /// <summary>
        /// A string to represent the verifier code field name for creating queries
        /// </summary>
        public const string UserNameFieldName = "UserName";

        /// <summary>
        /// A default constructor for the class
        /// </summary>
        public RequestToken()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Gets or sets the Database id
        /// </summary>
        [NHibernate.Mapping.Attributes.Id(0, Name = IdFieldName, UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")] 
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the key of the Consumer
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the token string
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Token { get; set; }

        /// <summary>
        /// Gets or sets the secret
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string TokenSecret { get; set; }

        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Realm { get; set; }

        /// <summary>
        /// Gets or sets the callback url
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the current access state
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual int State { get; set; }

        /// <summary>
        /// Gets or sets the expiration date
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the user id that authorized the request
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual long UserId { get; set; }

        /// <summary>
        /// Gets or sets the username that authorized the request.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the generated verifier code when authorized.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string VerifierCode { get; set; }

        /// <summary>
        /// The date the token was authorized.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime? DateAuthorized { get; set; }

        /// <summary>
        /// Gets or sets the Access token associated with this request
        /// </summary>
        [NHibernate.Mapping.Attributes.ManyToOne(Cascade = "all", Column = "AccessTokenId", ClassType = typeof(AccessToken), Unique = true)]
        public virtual AccessToken AccessToken { get; set; }
    }
}

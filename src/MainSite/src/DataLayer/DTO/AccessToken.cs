using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "AccessTokens")]
    public class AccessToken
    {
        /// <summary>
        /// A string to represent the field name for creating queries
        /// </summary>
        public const string IdFieldName = "Id";

        /// <summary>
        ///  A string to represent the token field name for creating queries
        /// </summary>
        public const string TokenFieldName = "Token";

        /// <summary>
        /// A default constructor for the class
        /// </summary>
        public AccessToken()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Gets or sets the database id
        /// </summary>
        [NHibernate.Mapping.Attributes.Id(0, Name = IdFieldName, UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")] 
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the consumer key
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the realm
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Realm { get; set; }

        /// <summary>
        /// Gets or sets the access token
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Token { get; set; }

        /// <summary>
        /// Gets or sets the access token secret
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string TokenSecret { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual long UserId { get; set; }

        /// <summary>
        /// The date and time that this access token was granted
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// The date and time that this access token was granted
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateGranted { get; set; }

        /// <summary>
        /// Gets or sets the expiration date
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime ExpirationDate { get; set; }
    }
}

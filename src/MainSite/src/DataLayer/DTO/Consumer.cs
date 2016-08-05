using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "Consumers")]
    public class Consumer
    {
        /// <summary>
        /// A string to define the ConsumerKey field for creating queries
        /// </summary>
        public const string ConsumerKeyFieldName = "ConsumerKey";

        /// <summary>
        /// A string to define the email field name for creating queries
        /// </summary>
        public const string EmailFieldName = "ContactEmail";

        /// <summary>
        /// Gets or sets the consumer key
        /// </summary>
        [NHibernate.Mapping.Attributes.Id(0, Name = ConsumerKeyFieldName, UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "assigned")] 
        public virtual string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the Public Key
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the consumer
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets if the access tokens should be auto granted
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual bool AutoGrant { get; set; }

        /// <summary>
        /// Gets or sets how many hours before an access token expires for this consumer
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual int AccessTokenLifetime { get; set; }
    }
}

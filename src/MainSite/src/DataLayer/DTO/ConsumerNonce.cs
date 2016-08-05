using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "ConsumerNonce")]
    public class ConsumerNonce
    {
        /// <summary>
        /// Define the Nonce Field name to use with queries
        /// </summary>
        public const string NonceFieldName = "Nonce";

        /// <summary>
        /// Gets or sets the nonce value        
        /// </summary>
        [NHibernate.Mapping.Attributes.Id(0, Name = NonceFieldName, UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "assigned")]
        public virtual string Nonce { get; set; }

        /// <summary>
        /// Gets or sets the consumer associated with this nonce
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the Date time this was created
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime Timestamp { get; set; }
    }

}

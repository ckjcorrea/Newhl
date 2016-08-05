using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "LoginAttempts")]
    public class LoginAttempt
    {
        /// <summary>
        /// Defines the Id field name for creating queries
        /// </summary>
        public const string IdFieldName = "Id";

        /// <summary>
        /// Defines the username field name for creating queries
        /// </summary>
        public const string UserNameField = "UserName";


        /// <summary>
        /// Gets or sets the Database id
        /// </summary>
        [NHibernate.Mapping.Attributes.Id(0, Name = IdFieldName, UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the source of the login attempt
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Source { get; set; }

        /// <summary>
        /// Gets or sets the status of the login attempt
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual bool WasSuccessfull { get; set; }

        /// <summary>
        /// Gets or sets the Date of the login attempt
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime AttemptDate { get; set; }

        /// <summary>
        /// Gets or sets the user these login attempst are associated with
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string UserName { get; set; }
    }
}

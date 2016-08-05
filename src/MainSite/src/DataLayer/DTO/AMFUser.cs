using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "AMFUsers")]
    public class AMFUser
    {
        /// <summary>
        /// Defines the Id field name for creating queries
        /// </summary>
        public const string IdFieldName = "Id";

        /// <summary>
        /// Defines the Email field name for creating queries
        /// </summary>
        public const string EmailFieldName = "Email";

        /// <summary>
        /// Default constructor for the class
        /// </summary>
        public AMFUser()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Gets o sets the database id.
        /// </summary>
        [NHibernate.Mapping.Attributes.Id(0, Name = IdFieldName, UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")] 
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the users email address
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the users first name 
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the users LastName
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the salt used with the password
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the hashed password
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the forgotten password hint
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string PasswordHint { get; set; }

        /// <summary>
        /// Gets the current status of the user
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual int UserStatus { get; set; }

        /// <summary>
        /// Gets the role of the user
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual int Role { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string ResetToken { get; set; }
    }
}

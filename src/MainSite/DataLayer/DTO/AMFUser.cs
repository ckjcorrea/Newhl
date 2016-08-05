using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DTO
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
        /// Gets or sets the users USAHockeyNum
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string USAHockeyNum  { get; set; }

        /// <summary>
        /// Gets or sets the users Date of Birth.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DOB { get; set; }

        /// <summary>
        /// Gets or sets the users Address1
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the users Address2
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the users City
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string City { get; set; }

        /// <summary>
        /// Gets or sets the users State
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string State { get; set; }

        /// <summary>
        /// Gets or sets the users ZipCode
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the users Primary Phone
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Phone1 { get; set; }

        /// <summary>
        /// Gets or sets the users Secondary
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Phone2 { get; set; }

        /// <summary>
        /// Gets or sets the users first Emergency Contact
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Emergency1 { get; set; }

        /// <summary>
        /// Gets or sets the users second Emergency Contact
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Emergency2 { get; set; }

        /// <summary>
        /// Gets or sets the users Years of Experience
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string YearsExp { get; set; }

        /// <summary>
        /// Gets or sets the users Experience Level
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Level { get; set; }

        /// <summary>
        /// Gets or sets the users Internet field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Internet { get; set; }

        /// <summary>
        /// Gets or sets the users Referral field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Referral { get; set; }

        /// <summary>
        /// Gets or sets the users Tournament field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Tournament { get; set; }

        /// <summary>
        /// Gets or sets the users Other field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Other { get; set; }

        /// <summary>
        /// Gets or sets the users LTP field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string LTP { get; set; }

        /// <summary>
        /// Gets or sets the users Tuesday field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Tuesday { get; set; }

        /// <summary>
        /// Gets or sets the users Wednesday field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Wednesday { get; set; }

        /// <summary>
        /// Gets or sets the users Stickhandling field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Stickhandling { get; set; }

        /// <summary>
        /// Gets or sets the users Somerville field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Somerville { get; set; }

        /// <summary>
        /// Gets or sets the users Games field
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string Games { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateCreated { get; set; }

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
        /// Gets or sets the forgotten password hint
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string PasswordHint { get; set; }
 

/*
       [NHibernate.Mapping.Attributes.Property]
        public virtual string ResetToken { get; set; }
*/
    }
}

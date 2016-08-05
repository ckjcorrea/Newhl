using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newhl.MainSite.Web.Models
{
    /// <summary>
    /// The class defines the inputs for the Register all
    /// </summary>
    public class ProgramModel
    {
        /// <summary>
        /// Gets or sets the user Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the users last name
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Gets or sets the user email that will be used for logging in
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the USAHockeyNum
        /// </summary>
        public string USAHockeyNum { get; set; }        

        /// <summary>
        /// Gets or sets the password hint
        /// </summary>
        public DateTime DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Emergency1 { get; set; }
        public string Emergency2 { get; set; }
        public int YearsExp { get; set; }
        public string Level { get; set; }
        public string Internet { get; set; }
        public string Referral { get; set; }
        public string Tournament { get; set; }
        public string Other { get; set; }
        public string LTP { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Stickhandling { get; set; }
        public string Somerville { get; set; }
        public string Games { get; set; }



        /// <summary>
        /// Gets or sets the oauth token to be authorized
        /// </summary>
        public string OAuthToken { get; set; }
    }
}
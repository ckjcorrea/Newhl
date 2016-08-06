using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.Common.DomainModel;
using Newhl.Common.Encryption;

namespace Newhl.MainSite.Common.DomainModel
{
    /// <summary>
    /// An extension of uSER to allow for storing of a password
    /// </summary>
    public class AMFUserLogin : User
    {
        /// <summary>
        /// Defines how many login failures are allowed before locking the account
        /// </summary>
        public const int MaxAllowedLoginFailures = 10;

        public const int SaltIterations = 1000;

        /// <summary>
        /// Defines how long to lock the user out for after failed login attempts
        /// </summary>
        public static double AccountLockTimeout = 30;


        /// <summary>
        /// Initialize id so that it is marked as unsaved.
        /// </summary>
        public AMFUserLogin()
        {
            this.Id = 0;
            this.DateCreated = DateTime.UtcNow;
            this.UserStatus = UserStatus.Active;
            this.Role = RoleType.Id.User;
        }

        public string GenerateNewPassword()
        {
            string retVal = string.Empty;
            Random random = new Random();
            string legalChars = "abcdefghijklmnopqrstuvwxzyABCDEFGHIJKLMNOPQRSTUVWXZY1234567890";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(legalChars.Substring(random.Next(0, legalChars.Length - 1), 1));
            }

            retVal = sb.ToString();

            this.UpdatePassword(retVal);

            return retVal;
        }
        
        public void UpdatePassword(string unencryptedPassword)
        {
            SHA1HashUtility passwordHashUtility = new SHA1HashUtility();
            this.PasswordHash = passwordHashUtility.HashPassword(unencryptedPassword);
            this.PasswordSalt = Convert.ToBase64String(passwordHashUtility.Salt);
        }

        public void UpdatePassword(string passwordHash, string passwordSalt)
        {
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
        }
        /// <summary>
        /// The salt associated with the hashed password
        /// </summary>
        public string PasswordSalt { get; private set; }

        /// <summary>
        /// Gets or sets the actually hashed password
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Gets or sets the date time that the user is created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the forgotten password hint
        /// </summary>
        public string PasswordHint { get; set; }

        /// <summary>
        /// Gets the current status of the user
        /// </summary>
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// Gets or sets the current user role
        /// </summary>
        public RoleType.Id Role { get; set; }

        public string USAHockeyNum { get; set; }
        public string DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Emergency1 { get; set; }
        public string Emergency2 { get; set; }
        public string YearsExp { get; set; }
        public PlayerLevel Level { get; set; }
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

        public IList<Payment> Payments { get; set; }

        public Payment AddPayment(decimal amount, PaymentMethods paymentMethod, string additionalDetails)
        {
            if (this.Payments == null)
            {
                this.Payments = new List<Payment>();
            }

            Payment newPayment = new Payment();
            newPayment.Amount = amount;
            newPayment.DateSubmitted = DateTime.Now;
            newPayment.PaymentMethod = paymentMethod;
            newPayment.AdditionalDetails = additionalDetails;
            newPayment.Player = this;

            this.Payments.Add(newPayment);

            return newPayment;
        }
    }
}

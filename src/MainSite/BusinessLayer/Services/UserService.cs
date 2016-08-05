using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.Common.Business;
using Newhl.Common.Configuration;
using Newhl.Common.DomainModel;
using Newhl.Common.Encryption;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.Common.Factories;
using Newhl.MainSite.DataLayer.Repositories;

namespace Newhl.MainSite.BusinessLayer.Services
{
    /// <summary>
    /// A service for managing user accounts
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public UserService(IAMFUserRepository userRepository, ILoginAttemptRepository loginAttemptRepository)
        {
            this.UserRepository = userRepository;
            this.LoginAttemptRepository = loginAttemptRepository;
        }

        /// <summary>
        /// Gets and sets the contained user repository
        /// </summary>
        protected IAMFUserRepository UserRepository { get; private set; }

        /// <summary>
        /// Gets and sets the contained login Attempt repository
        /// </summary>
        protected ILoginAttemptRepository LoginAttemptRepository { get; private set; }

        /// <summary>
        /// Get all of the users.
        /// </summary>
        /// <returns>A list of users</returns>
        public IList<AMFUserLogin> GetAll()
        {
            return this.UserRepository.GetAll();
        }

        /// <summary>
        /// Register a user with the system
        /// </summary>
        /// <param name="userName">The username of the user</param>
        /// <param name="password">The users password</param>
        /// <param name="passwordHint">The password hint for forgotten passwords</param>
        /// <param name="firstName">The users password</param>
        /// <param name="lastName">The users last name</param>
        /// <returns>An instance of a user</returns>
        public AMFUserLogin Register(string userName, string password, string passwordHint, string firstName, string lastName)
        {
            AMFUserLogin retVal = null;

            AMFUserLogin userLogin = UserFactory.Create(userName, password, firstName, lastName, passwordHint);
            retVal = this.UserRepository.Save(userLogin);

            return retVal;
        }

        public AMFUserLogin Register(String FirstName, String LastName, String Email, String USAHockeyNum, String DOB, String Address1, String Address2, String City, String State, String ZipCode, String Phone1, String Phone2, String Emergency1, String Emergency2, String YearsExp, String Level, String Internet, String Referral, String Tournament, String Other, String LTP, String Tues, String Wed, String Stickhandling, String Somerville, String Games, DateTime dateCreated, UserStatus userStatus, String passwordHint)
        {
            AMFUserLogin retVal = null;

            AMFUserLogin userLogin = UserFactory.Create(FirstName, LastName, Email, USAHockeyNum, DOB, Address1, Address2, City, State, ZipCode, Phone1, Phone2, Emergency1, Emergency2, YearsExp, Level, Internet, Referral, Tournament, Other, LTP, Tues, Wed, Stickhandling, Somerville, Games, dateCreated, userStatus, passwordHint);
            retVal = this.UserRepository.Save(userLogin);

            return retVal;

        }


        /// <summary>
        /// Update the editable fields for a User
        /// </summary>
        /// <param name="userLogin">The source user</param>
        /// <returns>The updated user</returns>       
        public AMFUserLogin Update(long userId, string firstName, string lastName, UserStatus userStatus, RoleType.Id userRole)
        {
            AMFUserLogin retVal = this.UserRepository.GetById(userId);

            if (retVal != null)
            {
                retVal.FirstName = firstName;
                retVal.LastName = lastName;
                retVal.UserStatus = userStatus;
                retVal.Role = userRole;

                retVal = this.UserRepository.Save(retVal);
            }

            return retVal;
        }

        public AMFUserLogin Update(String firstName, String lastName, String email, String usaHockeyNum, String dOB, String address1, String address2, String city, String state, String zipCode, String phone1, String phone2, String emergency1, String emergency2, String yearsExp, String level, String internet, String referral, String tournament, String other, String lTP, String tues, String wed, String stickhandling, String somerville, String games, DateTime dateCreated, UserStatus userStatus, String passwordHint )
        {
            AMFUserLogin retVal = this.UserRepository.GetByEmail(email);

            if (retVal != null)
            {
                retVal.FirstName = firstName;
                retVal.LastName = lastName;
                retVal.Email = email;
                retVal.USAHockeyNum = usaHockeyNum;
                retVal.DOB = dOB;
                retVal.Address1 = address1;
                retVal.Address2 = address2;
                retVal.City = city;
                retVal.State = state;
                retVal.ZipCode = zipCode;
                retVal.Phone1 = phone1;
                retVal.Phone2 = phone2;
                retVal.Emergency1 = emergency1;
                retVal.Emergency2 = emergency2;
                retVal.YearsExp = yearsExp;
                retVal.Level = level;
                retVal.Internet = internet;
                retVal.Referral = referral;
                retVal.Tournament = tournament;
                retVal.Other = other;
                retVal.LTP = lTP;
                retVal.Tuesday = tues;
                retVal.Wednesday = wed;
                retVal.Stickhandling = stickhandling;
                retVal.Somerville = somerville;
                retVal.Games = games;
                retVal.DateCreated = dateCreated;
                retVal.UserStatus = userStatus;
                retVal.PasswordHint = passwordHint;

                retVal = this.UserRepository.Save(retVal);
            }

            return retVal;
        }


        /// <summary>
        /// Update the editable fields for a User
        /// </summary>
        /// <param name="userLogin">The source user</param>
        /// <returns>The updated user</returns>       
        public AMFUserLogin Update(long userId, string firstName, string lastName, string password)
        {
            AMFUserLogin retVal = this.UserRepository.GetById(userId);

            if (retVal != null)
            {
                retVal.FirstName = firstName;
                retVal.LastName = lastName;
                retVal.UpdatePassword(password);
                
                retVal = this.UserRepository.Save(retVal);
            }

            return retVal;
        }


        /// <summary>
        /// Logon a user by the username and password
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">The unencrypted password</param>
        /// <returns>The user if one is found to match</returns>
        public AMFUserLogin LogonUser(string userName, string password, string loginSource)
        {
            AMFUserLogin retVal = null;

            AMFUserLogin targetUser = this.UserRepository.GetByEmail(userName);

            if (targetUser != null && targetUser.UserStatus == UserStatus.Active)
            {
                byte[] passwordSalt = Convert.FromBase64String(targetUser.PasswordSalt);

                if (SHA1HashUtility.ValidatePassword(password, targetUser.PasswordHash, passwordSalt, AMFUserLogin.SaltIterations) == true)
                {
                    retVal = targetUser;
                }
            }

            if (retVal == null)
            {
                this.AddLoginAttempt(false, loginSource, userName, targetUser);
            }
            else
            {
                this.AddLoginAttempt(true, loginSource, userName, targetUser);
            }

            return retVal;
        }

        /// <summary>
        /// Find a user by its id
        /// </summary>
        /// <param name="userId">The id of the user to look for</param>
        /// <returns>The user if one is found</returns>
        public AMFUserLogin GetUserById(long userId)
        {
            return this.UserRepository.GetById(userId);
        }

        /// <summary>
        /// Find a user by its email
        /// </summary>
        /// <param name="email">The email of the user to look for</param>
        /// <returns>The user if one is found</returns>
        public AMFUserLogin GetByEmail(string email)
        {
            return this.UserRepository.GetByEmail(email);
        }

        /// <summary>
        /// Search for a user by its email
        /// </summary>
        /// <param name="email">Search the email field for similar strings</param>
        /// <returns>The user if one is found</returns>
        public IList<AMFUserLogin> SearchByEmail(string email)
        {
            return this.UserRepository.SearchByEmail(email);
        }

        /// <summary>
        /// Update the login attempt tracking information based upon the last login status
        /// </summary>
        /// <param name="didLoginSucceed">Did the last login attempt succeed</param>
        public UserStatus AddLoginAttempt(bool didLoginSucceed, string source, string userName, AMFUserLogin relatedUser)
        {
            if (source == null)
            {
                source = string.Empty;
            }

            LoginAttempt newLoginAttempt = LoginAttemptFactory.Create(didLoginSucceed, source, userName);
            this.LoginAttemptRepository.Save(newLoginAttempt);

            UserStatus retVal = UserStatus.Active;            

            if (didLoginSucceed == true)
            {
                retVal = UserStatus.Active;
            }
            else
            {
                int failureCount = this.GetLoginFailureCount(userName, AMFUserLogin.MaxAllowedLoginFailures);

                if (failureCount == AMFUserLogin.MaxAllowedLoginFailures)
                {
                    retVal = UserStatus.Locked;
                }
            }

            if (relatedUser != null)
            {
                relatedUser.UserStatus = retVal;
                relatedUser = this.UserRepository.Save(relatedUser);
            }

            return retVal;
        }

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <returns>The failed login count</returns>
        public int GetLoginFailureCount(string userName)
        {
            return this.GetLoginFailureCount(userName, AMFUserLogin.MaxAllowedLoginFailures);
        }

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <param name="maxItemsToCheck">The number of items to check for failure</param>
        /// <returns>The failed login count</returns>
        public int GetLoginFailureCount(string userName, int maxItemsToCheck)
        {
            int retVal = 0;

            IList<LoginAttempt> loginsForUserName = this.LoginAttemptRepository.GetByUserName(userName);

            if(loginsForUserName != null)
            {
                IEnumerable<LoginAttempt> loginFailureWindow = loginsForUserName.Take(maxItemsToCheck);

                foreach (LoginAttempt loginAttempt in loginFailureWindow)
                {
                    if (loginAttempt.WasSuccessfull == false)
                    {
                        retVal++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return retVal;
        }

        public IList<LoginAttempt> GetLoginHistory(string userName)
        {
            return this.LoginAttemptRepository.GetByUserName(userName);
        }

        public void ResetPassword(string userEmail, EmailConfiguration emailConfig)
        {
            AMFUserLogin targetUser = this.UserRepository.GetByEmail(userEmail);

            string emailBody = "A user was not found with that email address.  Please try again.";

            if (targetUser != null)
            {
                if (targetUser != null)
                {
                    string newPassword = targetUser.GenerateNewPassword();
                    emailBody = "Sorry you had a problem entering your password, your new password is " + newPassword;

                    this.UserRepository.Save(targetUser);
                }

                EmailManager emailManager = new EmailManager(emailConfig);
                emailManager.SendEmail(emailConfig.FromAddress, userEmail, "New Password", emailBody);
            }
        }

        public bool Delete(long id)
        {
            bool retVal = false;

            AMFUserLogin targetUser = this.UserRepository.GetById(id);

            if(targetUser!=null)
            {
                retVal = this.UserRepository.Delete(targetUser);
            }

            return retVal;
        }
    }
}
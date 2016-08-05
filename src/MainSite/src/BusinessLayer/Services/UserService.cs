using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Encryption;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
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

            AMFUserLogin userLogin = new AMFUserLogin();
            userLogin.Email = userName;
            userLogin.FirstName = firstName;
            userLogin.LastName = lastName;
            userLogin.PasswordHint = passwordHint;
            userLogin.UpdatePassword(password);

            retVal = this.UserRepository.Save(userLogin);

            return retVal;
        }

        /// <summary>
        /// Update the editable fields for a User
        /// </summary>
        /// <param name="userLogin">The source user</param>
        /// <returns>The updated user</returns>       
        public AMFUserLogin Update(long userId, string firstName, string lastName, UserStatus userStatus, OAuthRoles userRole)
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

            LoginAttempt newLoginAttempt = new LoginAttempt();
            newLoginAttempt.AttemptDate = DateTime.UtcNow;
            newLoginAttempt.WasSuccessfull = didLoginSucceed;
            newLoginAttempt.Source = source;
            newLoginAttempt.UserName = userName;

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
                    string newPassword = AMFUserLogin.GenerateNewPassword();

                    emailBody = "Sorry you had a problem entering your password, your new password is " + newPassword;

                    targetUser.UpdatePassword(newPassword);

                    this.UserRepository.Save(targetUser);
                }

                EmailManager emailManager = new EmailManager(emailConfig);
                emailManager.SendEmail(emailConfig.FromAddress, userEmail, "New Password", emailBody);
            }
        }
    }
}
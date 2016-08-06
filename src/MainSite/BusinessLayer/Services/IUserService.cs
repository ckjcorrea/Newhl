using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.Common.Configuration;
using Newhl.Common.DomainModel;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.BusinessLayer.Services
{
    /// <summary>
    /// The functions provided by the User service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get all of the users.
        /// </summary>
        /// <returns>A list of users</returns>
        IList<AMFUserLogin> GetAll();


        /// <summary>
        /// Register a user with the system
        /// </summary>
        /// <param name="userName">The username of the user</param>
        /// <param name="password">The users password</param>
        /// <param name="firstName">The users first name</param>
        /// <param name="lastName">The users last name</param>
        /// <returns>An instance of a user</returns>
        AMFUserLogin Register(string userName, string password, string passwordHint, string firstName, string lastName);

        AMFUserLogin Register(String firstName, String lastName, String userName, string password, string passwordHint, String USAHockeyNum, String DOB, String Address1, String Address2, String City, String State, String ZipCode, String Phone1, String Phone2, String Emergency1, String Emergency2, String YearsExp, PlayerLevel playerLevel, String Internet, String Referral, String Tournament, String Other);


        /// <summary>
        /// Update a user with values an admin can change
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userStatus"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        AMFUserLogin Update(long userId, string firstName, string lastName, UserStatus userStatus, RoleType.Id userRole);

        AMFUserLogin Update(long userId, String FirstName, String LastName, String Email, String USAHockeyNum, String DOB, String Address1, String Address2, String City, String State, String ZipCode, String Phone1, String Phone2, String Emergency1, String Emergency2, String YearsExp, PlayerLevel Level, String Internet, String Referral, String Tournament, String Other);

        /// <summary>
        /// Update a user with values a regular user can change.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AMFUserLogin Update(long userId, string firstName, string lastName, string password);

        /// <summary>
        /// Logon a user by the username and password
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">The unencrypted password</param>
        /// <returns>The user if one is found to match</returns>
        AMFUserLogin LogonUser(string userName, string password, string loginSource);

        /// <summary>
        /// Find a user by its id
        /// </summary>
        /// <param name="userId">The id of the user to look for</param>
        /// <returns>The user if one is found</returns>
        AMFUserLogin GetUserById(long userId);

        /// <summary>
        /// Find a user by its email
        /// </summary>
        /// <param name="email">The email of the user to look for</param>
        /// <returns>The user if one is found</returns>
        AMFUserLogin GetByEmail(string email);

        /// <summary>
        /// Search for a user by its email
        /// </summary>
        /// <param name="email">Search the email field for similar strings</param>
        /// <returns>The user if one is found</returns>
        IList<AMFUserLogin> SearchByEmail(string email);

        /// <summary>
        /// Update the login attempt tracking information based upon the last login status
        /// </summary>
        /// <param name="didLoginSucceed">Did the last login attempt succeed</param>
        UserStatus AddLoginAttempt(bool didLoginSucceed, string source, string userName, AMFUserLogin relatedUser);

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <returns>The failed login count</returns>
        int GetLoginFailureCount(string userName);

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        /// <param name="userName">The username to check</param>
        /// <param name="maxItemsToCheck">The number of items to check for failure</param>
        /// <returns>The failed login count</returns>
        int GetLoginFailureCount(string userName, int maxItemsToCheck);

        IList<LoginAttempt> GetLoginHistory(string userName);

        void ResetPassword(string userEmail, EmailConfiguration emailConfig);

        bool Delete(long id);

        Payment MakePayment(long playerId, PaymentMethods paymentMethod, decimal paymentAmount, string additionalDetails);
    }
}
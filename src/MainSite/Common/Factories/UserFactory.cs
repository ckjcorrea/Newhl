using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Common.Factories
{
    public class UserFactory
    {

        public static AMFUserLogin Create(string userName, string password, string firstName, string lastName, string passwordHint)
        {
            AMFUserLogin retVal = new AMFUserLogin();
            retVal.Email = userName;
            retVal.FirstName = firstName;
            retVal.LastName = lastName;
            retVal.PasswordHint = passwordHint;
            retVal.UpdatePassword(password);

            return retVal;
        }


        public static AMFUserLogin Create(String firstName, String lastName, String email, String usaHockeyNum, String dOB, String address1, String address2, String city, String state, String zipCode, String phone1, String phone2, String emergency1, String emergency2, String yearsExp, String level, String internet, String referral, String tournament, String other, String lTP, String tues, String wed, String stickhandling, String somerville, String games, DateTime dateCreated, UserStatus userStatus, String passwordHint)
        {
            AMFUserLogin retVal = new AMFUserLogin();
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
           // if (userStatus != null)
            retVal.UserStatus = userStatus;
            retVal.PasswordHint = passwordHint;

            return retVal;
        }
    }
}

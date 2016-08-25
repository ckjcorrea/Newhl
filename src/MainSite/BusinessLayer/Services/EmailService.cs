using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.Common.Configuration;
using Newhl.Common.Business;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.BusinessLayer.Services
{
    public class EmailService : IEmailService
    {
        bool shouldSend = false;
        public void SendThankYouForRegisteringEmail(string emailAddress, EmailConfiguration emailConfig)
        {
            this.SendThankYouForRegisteringEmail(emailAddress, emailConfig, new EmailManager(EmailConfiguration.GetInstance()));
        }

        public void SendThankYouForRegisteringEmail(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager)
        {
            if(!emailConfig.IsDebugMode)
            {
                String emailBody = "Thank you for submitting your registration for a NEWHL Program. ";
                emailManager.SendEmail(emailConfig.FromAddress, emailAddress, "New NEWHL Registration", emailBody);
            }
        }

        public void SendThankYouForPaymentEmail(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo, PlayerSeason playerSeasonInfo)
        {
            //if (!emailConfig.IsDebugMode)
            //{
                EmailManager emailManager = new EmailManager(EmailConfiguration.GetInstance());
                String emailBody = "Thank you for submitting your registration for a NEWHL Program. ";
                emailBody = GenerateEmailDetails(playerInfo, playerSeasonInfo);
                emailManager.SendEmail(emailConfig.FromAddress, emailAddress, "Payment Details for NEWHL Registration", emailBody);
            //}
        }

        public void SendAdminNotificationEmail(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo)
        {
            this.SendAdminNotificationEmail(emailAddress, emailConfig, new EmailManager(EmailConfiguration.GetInstance()), playerInfo);
        }

        private string GenerateEmailDetails(AMFUserLogin playerInfo)
        {
            string retVal = "\n" + "\n" + "First Name: " + playerInfo.FirstName + "\n" + "Last Name: " + playerInfo.LastName + "\n" + "Email Address: " + playerInfo.Email + "\n" +
                    "USA Hockey Number: " + playerInfo.USAHockeyNum + "\n" + "DOB: " + playerInfo.DOB + "\n" + "Address: " + playerInfo.Address1 + " " + playerInfo.Address2 + "\n" +
                    "City: " + playerInfo.City + "\n" + "State: " + playerInfo.State + "\n" + "Zip Code: " + playerInfo.ZipCode + "\n" + "Primary Phone: " + playerInfo.Phone1 + "\n" +
                    "Secondary Phone: " + playerInfo.Phone2 + "\n" + "Emergency Contacts: " + playerInfo.Emergency1 + ", " + playerInfo.Emergency2 + "\n" + "Years Experience: " + playerInfo.YearsExp + "\n" +
                    "Experience Level: " + playerInfo.Level + "\n" + "\n";

            return retVal;
        }

        private string GenerateEmailDetails(AMFUserLogin playerInfo, PlayerSeason playerSeasonInfo)
        {
            string retVal = "\n" + "\n" + "First Name: " + playerInfo.FirstName + "\n" + "Last Name: " + playerInfo.LastName + "\n" + "Email Address: " + playerInfo.Email + "\n" +
                    "USA Hockey Number: " + playerInfo.USAHockeyNum + "\n" + "DOB: " + playerInfo.DOB + "\n" + "Address: " + playerInfo.Address1 + " " + playerInfo.Address2 + "\n" +
                    "City: " + playerInfo.City + "\n" + "State: " + playerInfo.State + "\n" + "Zip Code: " + playerInfo.ZipCode + "\n" + "Primary Phone: " + playerInfo.Phone1 + "\n" +
                    "Secondary Phone: " + playerInfo.Phone2 + "\n" + "Emergency Contacts: " + playerInfo.Emergency1 + ", " + playerInfo.Emergency2 + "\n" + "Years Experience: " + playerInfo.YearsExp + "\n" +
                    "Experience Level: " + playerInfo.Level + "\n" + "\n" + "Programs requested: " + "\n";

            for (int i = 0; i < playerSeasonInfo.Programs.Count; i++)
            {
                retVal = retVal + playerSeasonInfo.Programs[i].Name + "  ";
            }

            retVal = retVal + "\n" + "\n" + "Payment Details listed below: " + "\n" ;
            for (int x = 0; x < playerSeasonInfo.Payments.Count/2; x++)
            {
                retVal = retVal + "Payment Method: " + playerSeasonInfo.Payments[x].PaymentMethod + ", ";
                if (playerSeasonInfo.Payments[x].PaymentMethod.Equals(Newhl.MainSite.Common.DomainModel.PaymentMethods.Check))
                {
                    retVal = retVal + "Check Number: " + playerSeasonInfo.Payments[x].AdditionalDetails + ", ";
                }
                retVal = retVal + "Payment Amount: " + playerSeasonInfo.Payments[x].Amount + ", ";
                retVal = retVal + "Payment Promised On: " + playerSeasonInfo.Payments[x].DateSubmitted + " " + "\n";
            }
            return retVal;
        }

        public void SendAdminNotificationEmail(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager, AMFUserLogin playerInfo)
        {
            if (!emailConfig.IsDebugMode)
            {
                string emailBody = "The following player registered for a NEWHL Program. Registration information details below. The request was sent at " + DateTime.Now.ToString() + this.GenerateEmailDetails(playerInfo);
                emailManager.SendEmail(emailConfig.FromAddress, emailAddress, "New NEWHL Registration", emailBody);
            }
        }

        public void SendPlayerEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo)
        {
            this.SendPlayerEmailConfiguration(emailAddress, emailConfig, new EmailManager(EmailConfiguration.GetInstance()), playerInfo);
        }

        public void SendPlayerEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager, AMFUserLogin playerInfo)
        {
            if (!emailConfig.IsDebugMode)
            {
                string emailBody = "Your new registration information will be displayed below." + this.GenerateEmailDetails(playerInfo);
                emailManager.SendEmail(emailConfig.FromAddress, emailAddress, "New NEWHL Registration", emailBody);
            }
        }

        public void SendPlayerUpdateEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo)
        {
            this.SendPlayerUpdateEmailConfiguration(emailAddress, emailConfig, new EmailManager(EmailConfiguration.GetInstance()), playerInfo);
        }

        public void SendPlayerUpdateEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager, AMFUserLogin playerInfo)
        {
            if (!emailConfig.IsDebugMode)
            {
                string emailBody = " Your new registration information will be displayed below." + this.GenerateEmailDetails(playerInfo);
                emailManager.SendEmail(emailConfig.FromAddress, emailAddress, "New NEWHL Registration", emailBody);
            }
        }

    }
}

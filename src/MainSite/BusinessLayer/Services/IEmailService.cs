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
    public interface IEmailService
    {
        void SendThankYouForRegisteringEmail(string emailAddress, EmailConfiguration emailConfig);
        void SendThankYouForRegisteringEmail(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager);
        void SendThankYouForPaymentEmail(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo, PlayerSeason playerSeasonInfo);

        void SendAdminNotificationEmail(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo);
        void SendAdminNotificationEmail(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager, AMFUserLogin playerInfo);

        void SendPlayerEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo);
        void SendPlayerEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager, AMFUserLogin playerInfo);

        void SendPlayerUpdateEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, AMFUserLogin playerInfo);
        void SendPlayerUpdateEmailConfiguration(string emailAddress, EmailConfiguration emailConfig, EmailManager emailManager, AMFUserLogin playerInfo);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.Common.Configuration;
using Newhl.Common.DomainModel;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.BusinessLayer.Services
{
    public interface IPaymentService
    {
        bool CancelPromise(long playerId, long playerSeasonId, long paymentId);

        Payment ConfirmPromise(long playerId, long playerSeasonId, long paymentId);

        Payment AddSeasonPayment(long playerId, PaymentMethods paymentMethod, decimal paymentAmount, string additionalDetails, long playerSeasonId);
        Payment VerifyUserPayment();
    }
}

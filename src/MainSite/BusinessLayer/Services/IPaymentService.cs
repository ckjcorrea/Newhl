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
        IList<Payment> GetByPlayerId(long playerId);

        Payment GetById(long paymentId);

        bool CancelPromise(long userId, long paymentId);

        Payment ConfirmPromise(long userId, long paymentId);

        Payment AddUserPayment(long playerId, PaymentMethods paymentMethod, decimal paymentAmount, string additionalDetails);

        Payment VerifyUserPayment();
    }
}

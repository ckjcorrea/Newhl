using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.DataLayer.Repositories;

namespace Newhl.MainSite.BusinessLayer.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentService(IPaymentRepository paymentRepository, IAMFUserRepository userRepository)
        {
            this.PaymentRepository = paymentRepository;
            this.UserRepository = userRepository;
        }

        /// <summary>
        /// Gets and sets the contained user repository
        /// </summary>
        protected IPaymentRepository PaymentRepository { get; private set; }
        protected IAMFUserRepository UserRepository { get; private set; }

        public IList<Payment> GetByPlayerId(long playerId)
        {
            return this.PaymentRepository.GetByUserId(playerId);
        }

        public Payment AddUserPayment(long playerId, PaymentMethods paymentMethod, decimal paymentAmount, string additionalDetails)
        {
            Payment retVal = null;

            AMFUserLogin targetUser = this.UserRepository.GetById(playerId);
            
            if(targetUser != null)
            {
                retVal = new Payment();
                retVal.Amount = paymentAmount;
                retVal.DateSubmitted = DateTime.Now;
                retVal.PaymentMethod = paymentMethod;
                retVal.AdditionalDetails = additionalDetails;
                retVal.Player = targetUser;

                retVal = this.PaymentRepository.Save(retVal);
            }

            return retVal;
        }

        public Payment VerifyUserPayment()
        {
            throw new NotImplementedException();
        }
    }
}

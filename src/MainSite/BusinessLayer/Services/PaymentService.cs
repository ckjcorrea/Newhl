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
        public PaymentService(IPlayerSeasonRepository playerSeasonRepository)
        {
            this.PlayerSeasonRepository = playerSeasonRepository;
        }

        /// <summary>
        /// Gets and sets the contained user repository
        /// </summary>
        protected IPlayerSeasonRepository PlayerSeasonRepository { get; private set; }

        public Payment AddSeasonPayment(long playerId, PaymentMethods paymentMethod, decimal paymentAmount, string additionalDetails, long playerSeasonId)
        {
            Payment retVal = null;

            PlayerSeason targetSeason = this.PlayerSeasonRepository.GetById(playerSeasonId);

            if (targetSeason != null && targetSeason.PlayerId == playerId)
            {
                retVal = new Payment();
                retVal.Amount = paymentAmount;
                retVal.DateSubmitted = DateTime.Now;
                retVal.PaymentMethod = paymentMethod;
                retVal.AdditionalDetails = additionalDetails;
                retVal.PlayerSeasonId = targetSeason.Id;
                retVal.TransactionId = Guid.NewGuid();

                targetSeason.Payments.Add(retVal);
                targetSeason = this.PlayerSeasonRepository.Save(targetSeason);

                retVal = targetSeason.Payments.FirstOrDefault(p => p.TransactionId == retVal.TransactionId);
            }

            return retVal;
        }

        public bool CancelPromise(long playerId, long playerSeasonId, long paymentId)
        {
            bool retVal = false;

            PlayerSeason targetSeason = this.PlayerSeasonRepository.GetById(playerSeasonId);

            if (targetSeason != null && targetSeason.PlayerId == playerId)
            {
                Payment targetPayment = targetSeason.Payments.FirstOrDefault(p => p.Id == paymentId);
                
                if(targetPayment != null)
                {
                    if(targetPayment.State == PaymentStates.Promised)
                    {
                        targetSeason.Payments.Remove(targetPayment);
                        this.PlayerSeasonRepository.Save(targetSeason);
                        retVal = true;
                    }
                }
            }

            return retVal;
        }
        public Payment ConfirmPromise(long playerId, long playerSeasonId, long paymentId)
        {
            Payment retVal = null;

            PlayerSeason targetSeason = this.PlayerSeasonRepository.GetById(playerSeasonId);

            if (targetSeason != null && targetSeason.PlayerId == playerId)
            {
                retVal = targetSeason.Payments.FirstOrDefault(p => p.Id == paymentId);

                if (retVal != null)
                {
                    if (retVal.State == PaymentStates.Promised)
                    {
                        retVal.State = PaymentStates.Confirmed;
                        this.PlayerSeasonRepository.Save(targetSeason);
                    }
                }
            }

            return retVal;
        }

        public Payment VerifyUserPayment()
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models.API
{
    public class DisplaySeasonPayment
    {
        public DisplaySeasonPayment(Payment source)
        {
            this.Initialize(source);
        }

        public void Initialize(Payment source)
        {
            this.Id = source.Id;
            this.PlayerSeasonId = source.PlayerSeasonId;
            this.Amount = source.Amount;
            this.PaymentMethod = source.PaymentMethod.ToString();
            this.DateSubmitted = source.DateSubmitted.ToShortDateString();

            if(source.DateVerified.HasValue)
            {
                this.DateVerified = source.DateVerified.Value.ToShortDateString();
            }

            this.VerificationIdentifier = source.VerificationIdentifier;
            this.AdditionalDetails = source.AdditionalDetails;
            this.State = source.State.ToString();
            this.TransactionId = source.TransactionId;
        }

        public long Id { get; set; }

        public long PlayerSeasonId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }

        public string DateSubmitted { get; set; }

        public string DateVerified { get; set; }

        public string VerificationIdentifier { get; set; }

        public string AdditionalDetails { get; set; }

        public string State { get; set; }

        public Guid TransactionId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newhl.MainSite.Common.DomainModel
{
    public class Payment
    {
        public long Id { get; set; }

        public long PlayerSeasonId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethods PaymentMethod { get; set; }

        public DateTime DateSubmitted { get; set; }

        public DateTime? DateVerified { get; set; }

        public string VerificationIdentifier { get; set; }

        public string AdditionalDetails { get; set; }

        public PaymentStates State { get; set; }
    }
}

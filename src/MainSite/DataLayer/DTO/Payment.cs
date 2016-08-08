using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "Payments")]
    public class Payment
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long PlayerSeasonId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual decimal Amount { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual PaymentMethods PaymentMethod { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateSubmitted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime? DateVerified { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string VerificationIdentifier { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string AdditionalDetails { get; set; }

        [NHibernate.Mapping.Attributes.Property(Column ="PaymentState")]
        public virtual PaymentStates State { get; set; }

    }
}

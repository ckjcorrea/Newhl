using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "Programs")]
    public class Program
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        public virtual long SeasonId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DayOfWeek DayOfWeek { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime StartTime { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual float Price { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateCreated { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual bool IsActive { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Location { get; set; }
    }
}

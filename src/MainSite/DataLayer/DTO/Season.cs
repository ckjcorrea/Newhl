using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newhl.MainSite.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "Seasons")]
    public class Season
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime StartDate { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime EndDate { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateCreated { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual bool IsActive { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "Programs", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "SeasonId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(Program))]
        public virtual IList<Program> Programs { get; set; }
    }
}

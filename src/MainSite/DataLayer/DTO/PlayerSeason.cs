using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newhl.MainSite.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "PlayerSeasons")]
    public class PlayerSeason
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long SeasonId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long PlayerId { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "PlayerSeasonPrograms", Cascade = "Save-Update")]
        [NHibernate.Mapping.Attributes.Key(1, Column = "PlayerSeasonId")]
        [NHibernate.Mapping.Attributes.ManyToMany(2, Column = "ProgramId", ClassType = typeof(DTO.Program))]
        public virtual IList<DTO.Program> Programs {get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "Payments", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "PlayerSeasonId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(DTO.Payment))]
        public virtual IList<DTO.Payment> Payments { get; set; }
    }
}

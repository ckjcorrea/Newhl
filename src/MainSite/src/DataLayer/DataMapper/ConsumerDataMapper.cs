using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.DataMapper
{
    /// <summary>
    /// A data mapper going to/from the domain model and the dto
    /// </summary>
    internal class ConsumerDataMapper : DataMapBase<Consumer, DTO.Consumer>
    {
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static ConsumerDataMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<Consumer, DTO.Consumer>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<Consumer, DTO.Consumer>();
            }

            existingMap = Mapper.FindTypeMapFor<DTO.Consumer, Consumer>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.Consumer, Consumer>();
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Consumer Map(DTO.Consumer source, Consumer destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override DTO.Consumer Map(Consumer source, DTO.Consumer destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}

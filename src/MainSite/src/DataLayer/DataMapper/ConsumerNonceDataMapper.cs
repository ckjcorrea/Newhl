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
    class ConsumerNonceDataMapper : DataMapBase<ConsumerNonce, DTO.ConsumerNonce>
    {
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static ConsumerNonceDataMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<ConsumerNonce, DTO.ConsumerNonce>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<ConsumerNonce, DTO.ConsumerNonce>();
            }

            existingMap = Mapper.FindTypeMapFor<DTO.ConsumerNonce, ConsumerNonce>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.ConsumerNonce, ConsumerNonce>();
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
        public override ConsumerNonce Map(DTO.ConsumerNonce source, ConsumerNonce destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override DTO.ConsumerNonce Map(ConsumerNonce source, DTO.ConsumerNonce destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}

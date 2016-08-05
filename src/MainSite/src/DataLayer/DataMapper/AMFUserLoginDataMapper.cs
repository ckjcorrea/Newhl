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
    internal class AMFUserLoginDataMapper : DataMapBase<AMFUserLogin, DTO.AMFUser>
    {        
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static AMFUserLoginDataMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<LoginAttempt, DTO.LoginAttempt>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<LoginAttempt, DTO.LoginAttempt>();
            }

            existingMap = Mapper.FindTypeMapFor<DTO.LoginAttempt, LoginAttempt>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.LoginAttempt, LoginAttempt>();
            }

            existingMap = Mapper.FindTypeMapFor<AMFUserLogin, DTO.AMFUser>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<AMFUserLogin, DTO.AMFUser>();

            }

            existingMap = Mapper.FindTypeMapFor<DTO.AMFUser, AMFUserLogin>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.AMFUser, AMFUserLogin>();
            }

        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override AMFUserLogin Map(DTO.AMFUser source, AMFUserLogin destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override DTO.AMFUser Map(AMFUserLogin source, DTO.AMFUser destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}

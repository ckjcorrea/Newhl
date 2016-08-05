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
    internal class LoginAttemptDataMapper : DataMapBase<LoginAttempt, DTO.LoginAttempt>
    {        
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static LoginAttemptDataMapper()
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
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override LoginAttempt Map(DTO.LoginAttempt source, LoginAttempt destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override DTO.LoginAttempt Map(LoginAttempt source, DTO.LoginAttempt destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}

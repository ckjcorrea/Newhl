using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.DataMapper
{
    /// <summary>
    /// A data mapper going to/from the domain model and the dto
    /// </summary>
    internal class RequestTokenDataMapper : DataMapBase<RequestToken, DTO.RequestToken>
    {       
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static RequestTokenDataMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<AccessToken, DTO.AccessToken>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<AccessToken, DTO.AccessToken>()
                    .ForMember(dest => dest.TokenSecret, opt => opt.MapFrom(src => src.Secret))
                    .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.ToString()));
            }

            existingMap = Mapper.FindTypeMapFor<DTO.AccessToken, AccessToken>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.AccessToken, AccessToken>()
                    .ForMember(dest => dest.SessionHandle, opt => opt.Ignore())
                    .ForMember(dest => dest.Secret, opt => opt.MapFrom(src => src.TokenSecret))
                    .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => Realm.Parse(src.Realm)));
            }            

            existingMap = Mapper.FindTypeMapFor<RequestToken, DTO.RequestToken>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<RequestToken, DTO.RequestToken>()
                    .ForMember(dest => dest.TokenSecret, opt => opt.MapFrom(src => src.Secret))
                    .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.ToString()));
            }

            existingMap = Mapper.FindTypeMapFor<DTO.RequestToken, RequestToken>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.RequestToken, RequestToken>()
                    .ForMember(dest => dest.SessionHandle, opt => opt.Ignore())
                    .ForMember(dest => dest.Secret, opt => opt.MapFrom(src => src.TokenSecret))
                    .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => Realm.Parse(src.Realm)))
                    .ForMember(dest => dest.UsedUp, opt => opt.Ignore());
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
        public override RequestToken Map(DTO.RequestToken source, RequestToken destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override DTO.RequestToken Map(RequestToken source, DTO.RequestToken destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}

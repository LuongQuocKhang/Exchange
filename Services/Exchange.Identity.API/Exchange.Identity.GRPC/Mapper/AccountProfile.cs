using AutoMapper;
using Exchange.Identity.GRPC.Entities;
using Exchange.Identity.GRPC.Model;
using Exchange.Identity.GRPC.Protos;
using Google.Protobuf.WellKnownTypes;

namespace Exchange.Identity.GRPC.Mapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<JWTResponseData, JWTResponse>()
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(x => x.IsVerified))
                .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(x => x.IsExpired));

            CreateMap<AuthenticateJWT, JWTResponseData>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.Password))
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(x => x.AccessToken))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(x => x.RefreshToken))
                .ForMember(dest => dest.AccessTokenExpiredIn, opt => opt.MapFrom(x => x.AccessTokenExpiredIn.ToDateTime()))
                .ForMember(dest => dest.RefreshTokenExpiredIn, opt => opt.MapFrom(x => x.RefreshTokenExpiredIn.ToDateTime()));


            CreateMap<TBL_ADM_JWT_WHITE_LIST, AddJWTToken>().ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.Password))
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(x => x.AccessToken))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(x => x.RefreshToken))
                .ForMember(dest => dest.AccessTokenExpiredIn, opt => opt.MapFrom(x => x.AccessTokenExpiredIn.ToDateTime()))
                .ForMember(dest => dest.RefreshTokenExpiredIn, opt => opt.MapFrom(x => x.RefreshTokenExpiredIn.ToDateTime()));
        }
    }
}

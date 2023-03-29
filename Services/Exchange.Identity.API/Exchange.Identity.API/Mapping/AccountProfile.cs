using AutoMapper;
using Exchange.Identity.API.Models;
using Exchange.Identity.GRPC.Protos;
using Google.Protobuf.WellKnownTypes;
using JWTResponse = Exchange.Identity.API.Models.JWTResponse;

namespace Exchange.Identity.API.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<JWTRequestAuthorize, AuthenticateJWT>()
                //.ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.user_name))
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(x => x.access_token))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(x => x.refresh_token));
            //.ForMember(dest => dest.AccessTokenExpiredIn, opt => opt.MapFrom(x => x.access_token_expired_in.ToTimestamp()))
            //.ForMember(dest => dest.RefreshTokenExpiredIn, opt => opt.MapFrom(x => x.refresh_token_expired_in.ToTimestamp()));

            CreateMap<JWTResponse, AddJWTToken>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.user_name))
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(x => x.access_token))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(x => x.refresh_token));
            
        }
    }
}

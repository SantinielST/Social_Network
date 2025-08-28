using AutoMapper;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;
using SocialNetwork.ViewModels;

namespace SocialNetwork;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ViewModel -> BLL
        CreateMap<RegisterViewModel, User>()
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src =>
                src.Year.HasValue && src.Month.HasValue && src.Date.HasValue
                    ? DateTime.SpecifyKind(new DateTime(src.Year.Value, src.Month.Value, src.Date.Value), DateTimeKind.Utc)
                    : DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc)
            ))
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
 
        CreateMap<LoginViewModel, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        // BLL -> DAL
        CreateMap<User, UserEntity>();

        // DAL -> BLL
        CreateMap<UserEntity, User>();
    }
}

using AutoMapper;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;
using SocialNetwork.ViewModels;

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
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailReg))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(c => c.Login));

        CreateMap<LoginViewModel, UserEntity>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

        CreateMap<User, UserEntity>();
        CreateMap<UserEntity, User>();

        CreateMap<UserEditViewModel, User>();
        CreateMap<User, UserEditViewModel>().ForMember(x => x.UserId, opt => opt.MapFrom(c => c.Id));

        CreateMap<UserWithFriendExt, User>();
        CreateMap<User, UserWithFriendExt>();
    }
}
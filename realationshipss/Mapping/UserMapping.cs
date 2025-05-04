using AutoMapper;
using realationshipss.DTOS.UserDtos;
using realationshipss.Entities;

namespace realationshipss.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping() {

            //CreateMap<UserDto, User>()
            //    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            //    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            //CreateMap<User, UserDto>();


            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role ?? "User"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<LoginDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) //mapping only teh email
                .ForAllMembers(src => src.Ignore()); //ignoring other fields

        }

    }
}

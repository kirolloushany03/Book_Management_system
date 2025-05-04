using AutoMapper;
using realationshipss.DTOS.BookAuthorDtos;
using realationshipss.Entities;

namespace realationshipss.Mapping
{
    public class BookAuthorMappingProfile : Profile
    {
        public BookAuthorMappingProfile()
        {

            CreateMap<BookAuthorDto, BookAuthor>()
                .ForMember(dest => dest.ContributionPercentage, opt => opt.MapFrom(src => src.CPercent))
                .ReverseMap();
        }
    }
}
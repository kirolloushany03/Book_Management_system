using AutoMapper;
using realationshipss.DTOS.AuthorDtos.Requests;
using realationshipss.DTOS.AuthorDtos.Responses;
using realationshipss.DTOS.BookAuthorDtos;
using realationshipss.Entities;

namespace realationshipss.Mapping
{
    public class AuthorMappingProfile : Profile
    {

        public AuthorMappingProfile() {
            CreateMap<CreateAuthorDto, Author>();

            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src =>
                    src.BookAuthors.Select(ba => new AutorBookDtorel
                    {
                        BookId = ba.BookId,
                        CPercent = ba.ContributionPercentage
                    }).ToList()));

            CreateMap<Author, AuthorWithID>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src =>
                    src.BookAuthors.Select(ba => new AutorBookDtorel
                    {
                        BookId = ba.BookId,
                        CPercent = ba.ContributionPercentage
                    }).ToList()));
        }
    }
}

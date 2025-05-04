using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using realationshipss.DTOS;
using realationshipss.DTOS.BookAuthorDtos;
using realationshipss.DTOS.BookDtos.Requests;
using realationshipss.DTOS.BookDtos.Responses;
using realationshipss.Entities;

namespace realationshipss.Mapping
{
    public class BookMappingProfile : Profile
    {
    
        public BookMappingProfile() {


            CreateMap<CreateBookDto, Book>();

            CreateMap<UpdateBookDto, Book>();

            CreateMap<Book, BooksDto>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src =>
                            src.BookAuthors.Select(ba  => new BookAuthordtorel
                            {
                                AuthorId = ba.AuthorId,
                                CPercent = ba.ContributionPercentage
                            }).ToList()));

            CreateMap<Book, BookWithID>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src =>
                    src.BookAuthors.Select(ba => new BookAuthordtorel
                    {
                        AuthorId = ba.AuthorId,
                        CPercent = ba.ContributionPercentage
                    }).ToList()));
            //so we need to create bookauthorddto that will show only the author id and the cperect
            //need authorbookdto that will show only the book id and the c perecnt
        }
    }
}

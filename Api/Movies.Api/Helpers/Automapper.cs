using AutoMapper;
using Movies.BL.Models;
using Movies.DAL.Entities;
using MoviesFetcher.DTOs;

namespace MoviesFetcher.Helpers
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<SearchResult, SearchResultDTO>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.MovieTitle))
                .ForMember(dest => dest.LastSearched, opt => opt.MapFrom(src => src.Modified));

            CreateMap<Response<List<SearchResult>>, Response<List<SearchResultDTO>>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}

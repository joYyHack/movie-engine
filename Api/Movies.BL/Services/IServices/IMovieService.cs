using Movies.BL.Models;

namespace Movies.BL.Services.IServices
{
    public interface IMovieService
    {
        Task<Response<MoviesSearchResult>> SearchMoviesByTitle(string title, uint page = 1);
        Task<Response<MoviesSearchResult>> SearchBunchMoviesByTitle(string title, uint pages = 5);
        Task<Response<MovieFullData>> GetMovieByIMDbId(string IMDbId);
    }
}

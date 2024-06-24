using Movies.BL.Models;
using Movies.BL.Services.IServices;

namespace Movies.BL.Services
{
    public class MovieService : IMovieService
    {
        private readonly ISearchResultService _searchResultService;
        private readonly OmdbClient _omdbClient;
        public MovieService(ISearchResultService searchResultService, OmdbClient omdbClient)
        {
            _searchResultService = searchResultService;
            _omdbClient = omdbClient;
        }

        public async Task<Response<MoviesSearchResult>> SearchMoviesByTitle(string title, uint page)
        {
            ArgumentNullException.ThrowIfNull(title, nameof(title));

            var movies = await _omdbClient.SearchMoviesByTitle(title, page);
            await _searchResultService.SaveSearchQuery(title);

            return movies;
        }

        public async Task<Response<MovieFullData>> GetMovieByIMDbId(string IMDbId)
        {
            ArgumentNullException.ThrowIfNull(IMDbId, nameof(IMDbId));

            var movie = await _omdbClient.GetMovieByIMDbId(IMDbId);

            return movie;
        }
    }
}

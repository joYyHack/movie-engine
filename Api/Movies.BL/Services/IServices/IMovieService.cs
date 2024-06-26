using Movies.BL.Models;

namespace Movies.BL.Services.IServices
{
    /// <summary>
    /// Defines methods for movie-related operations.
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Searches for movies by title.
        /// </summary>
        /// <param name="title">The title of the movie to search for.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a response with the search results.</returns>
        Task<Response<MoviesSearchResult>> SearchMoviesByTitle(string title, uint page = 1);

        /// <summary>
        /// Searches for multiple pages of movies by title.
        /// </summary>
        /// <param name="title">The title of the movies to search for.</param>
        /// <param name="pages">The number of pages to search for.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a response with the search results.</returns>
        Task<Response<MoviesSearchResult>> SearchBunchMoviesByTitle(string title, uint pages = 5);

        /// <summary>
        /// Retrieves full movie data by IMDb ID.
        /// </summary>
        /// <param name="IMDbId">The IMDb ID of the movie to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a response with the movie data.</returns>
        Task<Response<MovieFullData>> GetMovieByIMDbId(string IMDbId);
    }
}

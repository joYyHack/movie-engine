using Microsoft.Extensions.Options;
using Movies.BL.Models;
using Movies.BL.Options;
using Newtonsoft.Json;
using System.Net;

namespace Movies.BL.Services
{
    /// <summary>
    /// A client for interacting with the OMDb API.
    /// </summary>
    public class OmdbClient
    {
        private readonly OMDB _omdb;
        private readonly HttpClient _client;

        public OmdbClient(IOptions<OMDB> omdb, HttpClient client)
        {
            _omdb = omdb.Value;

            _client = client;
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();
        }

        /// <summary>
        /// Searches for movies by title.
        /// </summary>
        /// <param name="title">The title of the movie.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a response with the search results.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the title is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the page number is less than 1.</exception>
        public async Task<Response<MoviesSearchResult>> SearchMoviesByTitle(string title, uint page = 1)
        {
            ArgumentNullException.ThrowIfNull(title, nameof(title));

            if (page < 1)
                throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than 0");

            var searchMovies = _omdb.GetSearchUri(title, page);
            return await CallOBDbApi<MoviesSearchResult>(searchMovies);
        }

        /// <summary>
        /// Searches for multiple pages of movies by title.
        /// </summary>
        /// <param name="title">The title of the movies.</param>
        /// <param name="pages">The number of pages to search for.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a response with the search results.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the title is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of pages is less than 1.</exception>
        public async Task<Response<MoviesSearchResult>> SearchBunchMoviesByTitle(string title, uint pages = 5)
        {
            ArgumentNullException.ThrowIfNull(title, nameof(title));

            if (pages < 1)
                throw new ArgumentOutOfRangeException(nameof(pages), "Page number must be greater than 0");

            Response<MoviesSearchResult> response = await SearchMoviesByTitle(title, 1);

            if (response.Succeeded && response.Data is not null && response.Data.TotalResults > 0)
            {
                for (uint i = 2; i <= pages && response.Data.Movies.Count <= response.Data.TotalResults; i++)
                {
                    var tempResponse = await SearchMoviesByTitle(title, i);

                    if (tempResponse.Failed
                        || tempResponse.Data.TotalResults == 0
                        || tempResponse.Data.Movies is null
                        || tempResponse.Data.Movies.Count == 0)
                    {
                        break;
                    }

                    response.Data.Movies.AddRange(tempResponse.Data.Movies);
                }
            }

            return response;
        }

        /// <summary>
        /// Retrieves full movie data by IMDb ID.
        /// </summary>
        /// <param name="IMDbId">The IMDb ID of the movie.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a response with the movie data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the IMDb ID is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the IMDb ID does not start with "tt".</exception>
        public async Task<Response<MovieFullData>> GetMovieByIMDbId(string IMDbId)
        {
            ArgumentNullException.ThrowIfNull(IMDbId, nameof(IMDbId));

            if (!IMDbId.StartsWith("tt"))
                throw new ArgumentException("IMDbId must start with 'tt'", nameof(IMDbId));

            var getMovie = _omdb.GetMovieUri(IMDbId);
            return await CallOBDbApi<MovieFullData>(getMovie);
        }

        /// <summary>
        /// Calls the OMDb API with the specified URI.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="uri">The URI to call.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a response with the data.</returns>
        private async Task<Response<T>> CallOBDbApi<T>(Uri uri) where T : BaseOMDbResponse
        {
            using var response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<T>(content);

            return Convert.ToBoolean(movie?.Response) ? movie : ($"{nameof(OmdbClient)}:{nameof(CallOBDbApi)} - {movie?.Error}", HttpStatusCode.NotFound);
        }
    }
}

using Microsoft.Extensions.Options;
using Movies.BL.Models;
using Movies.BL.Options;
using Newtonsoft.Json;
using System.Net;

namespace Movies.BL.Services
{
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

        public async Task<Response<MoviesSearchResult>> SearchMoviesByTitle(string title, uint page = 1)
        {
            var searchMovies = _omdb.GetSearchUri(title, page);
            return await CallOBDbApi<MoviesSearchResult>(searchMovies);
        }

        public async Task<Response<MovieFullData>> GetMovieByIMDbId(string IMDbId)
        {
            var getMovie = _omdb.GetMovieUri(IMDbId);
            return await CallOBDbApi<MovieFullData>(getMovie);
        }

        private async Task<Response<T>> CallOBDbApi<T>(Uri uri) where T : BaseOMDbResponse
        {
            using var response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<T>(content);

            return Convert.ToBoolean(movie?.Response) ? movie : ($"{nameof(OmdbClient)}:{nameof(GetMovieByIMDbId)} - {movie?.Error}", HttpStatusCode.NotFound);
        }
    }
}

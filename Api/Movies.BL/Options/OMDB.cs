namespace Movies.BL.Options
{
    public class OMDB
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }

        public Uri GetSearchUri(string title, uint page = 1) => new($"{BaseUrl}?apikey={ApiKey}&s={title}&page={page}");
        public Uri GetMovieUri(string IMDbId) => new($"{BaseUrl}?apikey={ApiKey}&i={IMDbId}");
    }
}

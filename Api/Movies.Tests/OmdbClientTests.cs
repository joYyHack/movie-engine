using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Movies.BL.Models;
using Movies.BL.Options;
using Movies.BL.Services;
using Newtonsoft.Json;
using System.Net;

namespace Movies.BL.Tests
{
    public class OmdbClientTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private Mock<IOptions<OMDB>> _omdbOptionsMock;

        private HttpClient _httpClient;
        private OMDB _omdbOptions;
        private OmdbClient _omdbClient;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            _omdbOptions = new OMDB
            {
                ApiKey = "fake-api-key",
                BaseUrl = "http://www.omdbapi.com/"
            };

            _omdbOptionsMock = new Mock<IOptions<OMDB>>();
            _omdbOptionsMock.Setup(o => o.Value).Returns(_omdbOptions);

            _omdbClient = new OmdbClient(_omdbOptionsMock.Object, _httpClient);
        }

        [Test]
        public async Task SearchMoviesByTitle_WithValidTitle_ReturnsMovies()
        {
            // Arrange
            var title = "Inception";

            var responseContent = new MoviesSearchResult
            {
                Response = "True",
                Movies = new()
                {
                    new() { Title = "Inception" }
                },
                TotalResults = 1
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseContent))
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);


            // Act
            var result = await _omdbClient.SearchMoviesByTitle(title);

            // Assert

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.Movies.Count, Is.EqualTo(1));
            Assert.That(result.Data.Movies[0].Title, Is.EqualTo("Inception"));
        }

        [Test]
        public void SearchMoviesByTitle_WithNullTitle_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _omdbClient.SearchMoviesByTitle(null));
        }

        [Test]
        public void SearchMoviesByTitle_WithInvalidPage_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _omdbClient.SearchMoviesByTitle("Inception", 0));
        }

        [Test]
        public async Task GetMovieByIMDbId_WithValidIMDbId_ReturnsMovie()
        {
            // Arrange
            var IMDbId = "tt1375666";
            var responseContent = new MovieFullData
            {
                Response = "True",
                Title = "Inception",
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseContent))
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _omdbClient.GetMovieByIMDbId(IMDbId);

            // Assert
            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Data.Title, Is.EqualTo("Inception"));
        }

        [Test]
        public void GetMovieByIMDbId_WithNullIMDbId_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _omdbClient.GetMovieByIMDbId(null));
        }

        [Test]
        public void GetMovieByIMDbId_WithInvalidIMDbId_ThrowsArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _omdbClient.GetMovieByIMDbId("1234567"));
        }
    }
}

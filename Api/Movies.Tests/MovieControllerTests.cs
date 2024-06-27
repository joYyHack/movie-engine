using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Movies.BL.Models;
using Movies.BL.Services.IServices;
using Movies.DAL.Entities;
using MoviesFetcher.Controllers;
using MoviesFetcher.DTOs;

namespace Movies.BL.Tests
{
    public class MovieControllerTests
    {
        private Mock<IMovieService> _movieServiceMock;
        private Mock<ISearchResultService> _searchResultServiceMock;
        private Mock<ILogger<MovieController>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private MovieController _movieController;

        [SetUp]
        public void Setup()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _searchResultServiceMock = new Mock<ISearchResultService>();
            _loggerMock = new Mock<ILogger<MovieController>>();
            _mapperMock = new Mock<IMapper>();

            _movieController = new MovieController(_movieServiceMock.Object, _searchResultServiceMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Search_WithValidTitle_ReturnsMovies()
        {
            // Arrange
            var title = "Inception";
            var page = 1u;
            var response = new Response<MoviesSearchResult>
            {
                Failed = false,
                Data = new()
                {
                    Movies = new()
                    {
                        new() { Title = "Inception" }
                    }
                }
            };

            _movieServiceMock.Setup(s => s.SearchMoviesByTitle(title, page)).ReturnsAsync(response);

            // Act
            var result = await _movieController.Search(title, page) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var resultValue = result.Value as Response<MoviesSearchResult>;

            Assert.That(resultValue.Succeeded, Is.True);
            Assert.That(resultValue.Data.Movies.Count, Is.EqualTo(1));
            Assert.That(resultValue.Data.Movies[0].Title, Is.EqualTo("Inception"));
        }

        [Test]
        public async Task Search_WithServiceFailure_ReturnsBadRequest()
        {
            // Arrange
            var title = "Inception";
            var page = 1u;
            var response = new Response<MoviesSearchResult>
            {
                Failed = true,
                Message = "Failed to fetch movies"
            };

            _movieServiceMock.Setup(s => s.SearchMoviesByTitle(title, page)).ReturnsAsync(response);

            // Act
            var result = await _movieController.Search(title, page) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));

            var resultValue = result.Value as Response<MoviesSearchResult>;

            Assert.That(resultValue.Succeeded, Is.False);
            Assert.That(resultValue.Message, Is.EqualTo("Failed to fetch movies"));
        }

        [Test]
        public void Search_WithException_ThrowsException()
        {
            // Arrange
            var title = "Inception";
            var page = 1u;

            _movieServiceMock.Setup(s => s.SearchMoviesByTitle(title, page)).ThrowsAsync(new Exception("Some error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _movieController.Search(title, page));

            _loggerMock.Verify(x => x.Log(
                It.Is<LogLevel>(log => log == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((msg, t) => msg.ToString() == "Some error"),
                It.Is<Exception>(ex => typeof(Exception) == ex.GetType()),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Test]
        public async Task SearchBunch_WithValidTitle_ReturnsMovies()
        {
            // Arrange
            var title = "Inception";
            var pages = 5u;
            var response = new Response<MoviesSearchResult>
            {
                Failed = false,
                Data = new()
                {
                    Movies = new()
                    {
                        new() { Title = "Inception" }
                    }
                }
            };

            _movieServiceMock.Setup(s => s.SearchBunchMoviesByTitle(title, pages)).ReturnsAsync(response);

            // Act
            var result = await _movieController.SearchBunch(title, pages) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var resultValue = result.Value as Response<MoviesSearchResult>;

            Assert.That(resultValue.Succeeded, Is.True);
            Assert.That(resultValue.Data.Movies.Count, Is.EqualTo(1));
            Assert.That(resultValue.Data.Movies[0].Title, Is.EqualTo("Inception"));
        }

        [Test]
        public async Task Get_WithValidIMDbId_ReturnsMovie()
        {
            // Arrange
            var IMDbId = "tt1375666";
            var response = new Response<MovieFullData>
            {
                Failed = false,
                Data = new()
                {
                    Title = "Inception",
                }
            };

            _movieServiceMock.Setup(s => s.GetMovieByIMDbId(IMDbId)).ReturnsAsync(response);

            // Act
            var result = await _movieController.Get(IMDbId) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var resultValue = result.Value as Response<MovieFullData>;

            Assert.That(resultValue.Succeeded, Is.True);
            Assert.That(resultValue.Data.Title, Is.EqualTo("Inception"));
        }

        [Test]
        public void GetLastSearchResults_ReturnsLatestSearchResults()
        {
            // Arrange
            var searchResults = new Response<List<SearchResult>>
            {
                Failed = false,
                Data = new()
                {
                    new() { MovieTitle = "Inception" }
                }
            };

            var searchResultDtos = new Response<List<SearchResultDTO>>
            {
                Failed = false,
                Data = new()
                {
                    new() { MovieTitle = "Inception" }
                }
            };

            _searchResultServiceMock.Setup(s => s.GetLatestSearchResults()).Returns(searchResults);
            _mapperMock.Setup(m => m.Map<Response<List<SearchResult>>, Response<List<SearchResultDTO>>>(searchResults)).Returns(searchResultDtos);

            // Act
            var result = _movieController.GetLastSearchResults() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var resultValue = result.Value as Response<List<SearchResultDTO>>;

            Assert.That(resultValue.Succeeded, Is.True);
            Assert.That(resultValue.Data.Count, Is.EqualTo(1));
            Assert.That(resultValue.Data[0].MovieTitle, Is.EqualTo("Inception"));
        }
    }
}

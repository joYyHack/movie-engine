using Microsoft.Extensions.Options;
using Moq;
using Movies.BL.Options;
using Movies.BL.Services;
using Movies.DAL.Entities;
using Movies.DAL.Repo.IRepo;
using Movies.DAL.UnitOfWork;
using System.Linq.Expressions;

namespace Movies.BL.Tests
{
    public class SearchResultServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IBaseRepo<SearchResult>> _searchResultRepoMock;
        private Mock<IOptions<SearchConfig>> _searchConfigMock;
        private SearchResultService _searchResultService;

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _searchResultRepoMock = new Mock<IBaseRepo<SearchResult>>();

            _uowMock.Setup(u => u.Repo<SearchResult>()).Returns(_searchResultRepoMock.Object);

            _searchConfigMock = new Mock<IOptions<SearchConfig>>();
            _searchConfigMock.Setup(o => o.Value).Returns(new SearchConfig { MaxResults = 5 });

            _searchResultService = new SearchResultService(_uowMock.Object, _searchConfigMock.Object);
        }

        [Test]
        public void GetLatestSearchResults_ReturnsLatestResultsOrderedByDesc()
        {
            // Arrange
            var searchResults = new List<SearchResult>
            {
                new() { MovieTitle = "Movie1", Modified = DateTime.Now.AddMinutes(-5) },
                new() { MovieTitle = "Movie2", Modified = DateTime.Now.AddMinutes(-10) },
                new() { MovieTitle = "Movie3", Modified = DateTime.Now.AddMinutes(-1) },
                new() { MovieTitle = "Movie4", Modified = DateTime.Now.AddMinutes(-4) },
                new() { MovieTitle = "Movie5", Modified = DateTime.Now.AddMinutes(-7) },
                new() { MovieTitle = "Movie6", Modified = DateTime.Now.AddMinutes(-8) },
                new() { MovieTitle = "Movie7", Modified = DateTime.Now.AddMinutes(-6) },
            };

            _searchResultRepoMock.Setup(repo => repo.GetAll()).Returns(searchResults.AsQueryable());

            // Act
            var result = _searchResultService.GetLatestSearchResults();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(5));
            Assert.That(result.Data[0].MovieTitle, Is.EqualTo("Movie3"));
        }

        [Test]
        public async Task SaveSearchQuery_ExistingTitle_UpdatesSearchResult()
        {
            // Arrange
            var title = "Movie1";
            var existingSearchResult = new SearchResult { MovieTitle = title, Modified = DateTime.Now.AddMinutes(-5) };

            _searchResultRepoMock.Setup(repo => repo.GetBy(It.IsAny<Expression<Func<SearchResult, bool>>>()))
                .Returns(new List<SearchResult> { existingSearchResult }.AsQueryable());

            // Act
            await _searchResultService.SaveSearchQuery(title);

            // Assert
            _searchResultRepoMock.Verify(repo => repo.Update(It.Is<SearchResult>(sr => sr.MovieTitle == title)), Times.Once);
            _searchResultRepoMock.Verify(repo => repo.Add(It.IsAny<SearchResult>()), Times.Never);
        }

        [Test]
        public async Task SaveSearchQuery_NewTitle_AddsSearchResult()
        {
            // Arrange
            var title = "New Movie";

            _searchResultRepoMock.Setup(repo => repo.GetBy(It.IsAny<Expression<Func<SearchResult, bool>>>()))
                .Returns(Enumerable.Empty<SearchResult>().AsQueryable());

            // Act
            await _searchResultService.SaveSearchQuery(title);

            // Assert
            _searchResultRepoMock.Verify(repo => repo.Add(It.Is<SearchResult>(sr => sr.MovieTitle == title)), Times.Once);
            _searchResultRepoMock.Verify(repo => repo.Update(It.IsAny<SearchResult>()), Times.Never);
        }
    }
}
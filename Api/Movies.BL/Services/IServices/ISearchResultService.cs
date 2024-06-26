using Movies.BL.Models;
using Movies.DAL.Entities;

namespace Movies.BL.Services.IServices
{
    /// <summary>
    /// Defines methods for handling search results operations.
    /// </summary>
    public interface ISearchResultService
    {
        /// <summary>
        /// Retrieves the latest search results.
        /// </summary>
        /// <returns>A response containing a list of the latest search results.</returns>
        Response<List<SearchResult>> GetLatestSearchResults();

        /// <summary>
        /// Saves a search query by title.
        /// </summary>
        /// <param name="title">The title of the movie to save the search query for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveSearchQuery(string title);
    }
}

using Movies.BL.Models;
using Movies.DAL.Entities;

namespace Movies.BL.Services.IServices
{
    public interface ISearchResultService
    {
        Response<List<SearchResult>> GetLatestSearchResults();
        Task SaveSearchQuery(string title);
    }
}

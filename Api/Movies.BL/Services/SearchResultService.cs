using Microsoft.Extensions.Options;
using Movies.BL.Models;
using Movies.BL.Options;
using Movies.BL.Services.IServices;
using Movies.DAL.Entities;
using Movies.DAL.Repo.IRepo;
using Movies.DAL.UnitOfWork;

namespace Movies.BL.Services
{
    public class SearchResultService : ISearchResultService
    {
        private readonly SearchConfig _searchConfig;

        private readonly IUnitOfWork _uow;
        private readonly IBaseRepo<SearchResult> _searchResultRepo;
        public SearchResultService(IUnitOfWork uow, IOptions<SearchConfig> searchConfig)
        {
            _searchConfig = searchConfig.Value;
            _uow = uow;

            _searchResultRepo = _uow.Repo<SearchResult>();
        }

        public Response<List<SearchResult>> GetLatestSearchResults()
        {
            return _searchResultRepo
                .GetAll()
                .OrderByDescending(x => x.Modified)
                .Take(_searchConfig.MaxResults).ToList();
        }

        public async Task SaveSearchQuery(string title)
        {
            var result = _searchResultRepo
                .GetBy(search => title.Trim().ToLower() == search.MovieTitle.Trim().ToLower())
                .FirstOrDefault();

            if (result != null)
            {
                result.MovieTitle = title;
                await _searchResultRepo.Update(result);
            }
            else
            {
                await _searchResultRepo.Add(new() { MovieTitle = title });
            }
        }
    }
}

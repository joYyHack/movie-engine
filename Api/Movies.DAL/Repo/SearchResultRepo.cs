using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;
using Movies.DAL.Repo.IRepo;

namespace Movies.DAL.Repo
{
    public class SearchResultRepo : BaseRepo<SearchResult>, ISearchResultRepo
    {
        public SearchResultRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;
using Movies.DAL.Repo.IRepo;

namespace Movies.DAL.Repo
{
    public class SearchResultRepo(DbContext dbContext) : BaseRepo<SearchResult>(dbContext), ISearchResultRepo
    {
    }
}

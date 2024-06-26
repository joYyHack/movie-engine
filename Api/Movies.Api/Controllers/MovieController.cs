using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.BL.Models;
using Movies.BL.Services.IServices;
using Movies.DAL.Entities;
using MoviesFetcher.Attributes;
using MoviesFetcher.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MoviesFetcher.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;
    private readonly ISearchResultService _searchResultService;

    public MovieController(IMovieService movieService, ISearchResultService searchResultService, ILogger<MovieController> logger, IMapper mapper)
    {
        _movieService = movieService;
        _searchResultService = searchResultService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Searches for movies by title. (10 movies returned per page)
    /// </summary>
    /// <param name="title">The title of the movie.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <returns>A list of movies matching the search criteria.</returns>
    /// <response code="200">Returns the list of movies.</response>
    /// <response code="500">If the request is invalid.</response>
    [HttpGet("search-by-title")]
    [ProducesResponseType(typeof(Response<MoviesSearchResult>), 200)]
    [ProducesResponseType(typeof(Response<string>), 500)]
    public async Task<IActionResult> Search([Required] string title, [Range(1, 100)] uint page = 1)
    {
        try
        {
            var response = await _movieService.SearchMoviesByTitle(title, page);

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Searches for a bunch of movies by title.(i.e. 1 page == 10 movies, 5 pages == 50 movies)
    /// </summary>
    /// <param name="title">The title of the movies.</param>
    /// <param name="pages">The number of pages for pagination.</param>
    /// <returns>A list of movies matching the search criteria.</returns>
    /// <response code="200">Returns the list of movies.</response>
    /// <response code="500">If the request is invalid.</response>
    [HttpGet("search-bunch-by-title")]
    [ProducesResponseType(typeof(Response<MoviesSearchResult>), 200)]
    [ProducesResponseType(typeof(Response<string>), 500)]
    public async Task<IActionResult> SearchBunch([Required] string title, [Range(1, 100)] uint pages = 5)
    {
        try
        {
            var response = await _movieService.SearchBunchMoviesByTitle(title, pages);

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Gets a movie by IMDb ID.
    /// </summary>
    /// <param name="IMDbId">The IMDb ID of the movie.</param>
    /// <returns>The movie matching the IMDb ID.</returns>
    /// <response code="200">Returns the movie.</response>
    /// <response code="500">If the request is invalid.</response>
    [HttpGet("get-by-imdbid")]
    [ProducesResponseType(typeof(Response<MovieFullData>), 200)]
    [ProducesResponseType(typeof(Response<string>), 500)]
    public async Task<IActionResult> Get([IMDbId] string IMDbId)
    {
        try
        {
            var response = await _movieService.GetMovieByIMDbId(IMDbId);

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Gets the latest search results. (5 latest searches)
    /// </summary>
    /// <returns>The latest search results.</returns>
    /// <response code="200">Returns the latest search results.</response>
    /// <response code="500">If the request is invalid.</response>
    [HttpGet("last-search-results")]
    [ProducesResponseType(typeof(Response<List<SearchResultDTO>>), 200)]
    [ProducesResponseType(typeof(Response<string>), 500)]
    public IActionResult GetLastSearchResults()
    {
        try
        {
            var response = _searchResultService.GetLatestSearchResults();

            return response.Succeeded
                ? Ok(_mapper.Map<Response<List<SearchResult>>, Response<List<SearchResultDTO>>>(response))
                : BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }
}

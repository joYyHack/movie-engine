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

    [HttpGet("search-by-title")]
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

    [HttpGet("get-by-imdbid")]
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

    [HttpGet("last-search-results")]
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

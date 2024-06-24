using Microsoft.EntityFrameworkCore;
using Movies.BL.Options;
using Movies.BL.Services;
using Movies.BL.Services.IServices;
using Movies.DAL;
using Movies.DAL.UnitOfWork;
using MoviesFetcher.Extensions;
using MoviesFetcher.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Appsettings
builder.Services.Configure<OMDB>(builder.Configuration.GetSection(nameof(OMDB)));
builder.Services.Configure<SearchConfig>(builder.Configuration.GetSection(nameof(SearchConfig)));

// Automapper
builder.Services.AddAutoMapper(typeof(Automapper));

//Add DbContext
builder.Services.AddDbContext<MoviesDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repo
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// HttpClient
builder.Services.AddHttpClient<OmdbClient>();

// Services
builder.Services.AddScoped<ISearchResultService, SearchResultService>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseErrorHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MigrateDb();

app.Run();

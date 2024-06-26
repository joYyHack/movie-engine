using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.BL.Options;
using Movies.BL.Services;
using Movies.BL.Services.IServices;
using Movies.DAL;
using Movies.DAL.UnitOfWork;
using MoviesFetcher.Extensions;
using MoviesFetcher.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Movies API - V1",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(AppContext.BaseDirectory, "Movies.Api.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseErrorHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MigrateDb();

app.Run();

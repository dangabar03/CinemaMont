using System.Text.Json.Serialization;
using CinemaMont.Dtos;

namespace CinemaMont.Services;

public interface ITmbdService
{
    public Task<List<TmdbDto>> GetPopularMoviesAsync();
}
public class TmdbService(HttpClient httpClient, IConfiguration configuration) : ITmbdService
{
    public async Task<List<TmdbDto>> GetPopularMoviesAsync()
    {
        var apiKey = configuration["TMDB:ApiKey"];
        var imageBaseUrl = configuration["TMDB:ImageBaseUrl"];

        var response = await httpClient.GetFromJsonAsync<TmdbPopularMoviesResponse>(
            $"movie/popular?api_key={apiKey}");

        if (response?.Results is null)
        {
            return [];
        }

        return response.Results.Select(m => new TmdbDto
        {
            Title = m.Title,
            Overview = m.Overview,
            ImageUrl = m.PosterPath is not null ? $"{imageBaseUrl}{m.PosterPath}" : null
        }).ToList();
    }

    private class TmdbPopularMoviesResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbMovieResult>? Results { get; set; }
    }

    private class TmdbMovieResult
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("overview")]
        public string? Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }
    }
}

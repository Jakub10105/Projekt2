using System.Text.Json;
using System.Globalization;
using WeatherAggregator.Interfaces;
using WeatherAggregator.Models;

namespace WeatherAggregator.Providers;

public class MeteoProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;
    public string Name => "Open-Meteo API";

    public MeteoProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetCurrentWeatherAsync(string city, double lat, double lon)
    {
        try
        {

            string url = string.Create(CultureInfo.InvariantCulture, 
                $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true");
            
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); 

            string jsonResponse = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            double temp = doc.RootElement.GetProperty("current_weather").GetProperty("temperature").GetDouble();

            return new WeatherData
            {
                ProviderName = Name,
                City = city,
                TemperatureCelsius = temp
            };
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Network error communicating with {Name}: {ex.Message}", ex);
        }
    }
}
using WeatherAggregator.Models;

namespace WeatherAggregator.Interfaces;

public interface IWeatherProvider
{
    string Name { get; }
    Task<WeatherData> GetCurrentWeatherAsync(string city, double lat, double lon);
}
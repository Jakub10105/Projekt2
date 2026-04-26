namespace WeatherAggregator.Providers;
using WeatherAggregator.Interfaces;
using WeatherAggregator.Models;

public class MockWeatherProvider : IWeatherProvider
{
    public string Name => "Mock Weather API";

    public async Task<WeatherData> GetCurrentWeatherAsync(string city, double lat, double lon)
    {
        // Simulace zpoždění sítě
        await Task.Delay(500); 
        return new WeatherData
        {
            ProviderName = Name, 
            City = city, 
            TemperatureCelsius = 15.0 // testovaci temperatura pro simulaci
        };
    }
}
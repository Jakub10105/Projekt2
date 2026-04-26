using WeatherAggregator.Interfaces;
using WeatherAggregator.Models;

namespace WeatherAggregator.Services;

public class WeatherAggregatorService
{
    private readonly IEnumerable<IWeatherProvider> _providers;

    public WeatherAggregatorService(IEnumerable<IWeatherProvider> providers)
    {
        _providers = providers;
    }

    public async Task GetAndProcessWeatherAsync(string city, double lat, double lon)
    {
        Console.WriteLine($"\nStarting data fetch for {city}...");

        var tasks = new List<Task<WeatherData>>();

        foreach (var provider in _providers)
        {
            tasks.Add(provider.GetCurrentWeatherAsync(city, lat, lon));
        }

        try
        {
            WeatherData[] results = await Task.WhenAll(tasks);

            Console.WriteLine("\n--- Weather Data Received ---");
            double totalTemp = 0;
            
            foreach (var result in results)
            {
                Console.WriteLine($"[{result.ProviderName}] Temp: {result.TemperatureCelsius}°C");
                totalTemp += result.TemperatureCelsius;
            }

            double averageTemp = totalTemp / results.Length;
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Aggregated (Average) Temperature: {averageTemp:F1}°C\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] An error occurred during aggregation: {ex.Message}");
        }
    }
}
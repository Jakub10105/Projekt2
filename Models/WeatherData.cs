namespace WeatherAggregator.Models;

public class WeatherData
{
    public string ProviderName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public double TemperatureCelsius { get; set; }
}
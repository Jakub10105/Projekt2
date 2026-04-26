using WeatherAggregator.Interfaces;
using WeatherAggregator.Providers;
using WeatherAggregator.Services;

using HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

var providers = new List<IWeatherProvider>
{
    new MeteoProvider(httpClient),
    new MockWeatherProvider() 
};

var aggregatorService = new WeatherAggregatorService(providers);

Console.WriteLine("Weather Aggregator");

while (true)
{
    Console.WriteLine("Select an option:");
    Console.WriteLine("1 - Check weather in Uničov");
    Console.WriteLine("0 - Exit");
    Console.Write("Your choice: ");
    
    string? input = Console.ReadLine();

    if (input == "0") break;

    if (input == "1")
    {
        try
        {
            await aggregatorService.GetAndProcessWeatherAsync("Uničov", 49.7709, 17.1214);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical Application Error: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Invalid selection, please try again.\n");
    }
}

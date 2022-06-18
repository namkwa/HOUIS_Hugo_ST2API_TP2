using System;
using System.Text.Json;
using System.Threading.Tasks;
namespace TP2 {
    class Program {
        static async Task Main(string[] args) {
            await DisplayWeatherInformation("Moscow", WeatherInformation.weather);

            await DisplayWeatherInformation("Oslo", WeatherInformation.sunrise);
            await DisplayWeatherInformation("Oslo", WeatherInformation.sunset);

            await DisplayWeatherInformation("Jakarta", WeatherInformation.temperature);

            await DisplayMostWindyCity();

            List<string> locations = new List<string>() { "Kiev", "Moscow", "Berlin" };
            List<WeatherInformation> requestedWeatherLocations = new List<WeatherInformation>() { WeatherInformation.humidity, WeatherInformation.pressure };
            await DisplayMultipleWeatherInformation(locations, requestedWeatherLocations);
            Console.ReadLine();
        }
        static async Task DisplayWeatherInformation(string location, WeatherInformation requestedWeatherInformation) {
            CurrentWeather weatherInformationLocation = await Api.GetDataFromApi(location);
            switch (requestedWeatherInformation) {
                case WeatherInformation.weather:
                    Console.WriteLine("The weather in " + weatherInformationLocation.name + " is : " + weatherInformationLocation.weather[0].main);
                    break;
                case WeatherInformation.temperature:
                    Console.WriteLine("The temperature in " + weatherInformationLocation.name + " is : " + weatherInformationLocation.main.temp + " degrees");
                    break;
                case WeatherInformation.sunrise:
                    Console.WriteLine("In " + weatherInformationLocation.name + ", the sun will rise at : " + DateUtils.ConvertDate(weatherInformationLocation.sys.sunrise));
                    break;
                case WeatherInformation.sunset:
                    Console.WriteLine("In " + weatherInformationLocation.name + ", the sun will set at : " + DateUtils.ConvertDate(weatherInformationLocation.sys.sunset));
                    break;
                case WeatherInformation.humidity:
                    Console.WriteLine("The humidity in " + weatherInformationLocation.name + " is : " + weatherInformationLocation.main.humidity + "%");
                    break;
                case WeatherInformation.pressure:
                    Console.WriteLine("The pressure in " + weatherInformationLocation.name + " is : " + weatherInformationLocation.main.pressure + " hPa");
                    break;
                default:
                    Console.WriteLine("This type of data isn't available");
                    break;
            }
        }
        static async Task DisplayMostWindyCity() {
            CurrentWeather weatherInformationNewYork = await Api.GetDataFromApi("New%20York");
            CurrentWeather weatherInformationTokyo = await Api.GetDataFromApi("Tokyo");
            CurrentWeather weatherInformationParis = await Api.GetDataFromApi("Paris");
            List<CurrentWeather> currentWeatherList = new List<CurrentWeather>() { weatherInformationNewYork, weatherInformationTokyo, weatherInformationParis };
            CurrentWeather MostWindyCity = currentWeatherList.MaxBy(weatherInformation => weatherInformation.wind.speed);
            Console.WriteLine("The most windy city is " + MostWindyCity.name + " with a wind speed of : " + MostWindyCity.wind.speed);
        }
        static async Task DisplayMultipleWeatherInformation(List<string> locations, List<WeatherInformation> requestedWeatherInformations) {
            foreach (string location in locations) {
                foreach (WeatherInformation weatherInformation in requestedWeatherInformations) {
                    await DisplayWeatherInformation(location, weatherInformation);
                }
            }
        }
    }
}

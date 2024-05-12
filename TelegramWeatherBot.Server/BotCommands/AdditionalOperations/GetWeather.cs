using System.Globalization;
using OpenWeather;
using OpenWeather.Models.CurrentWeather;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramWeatherBotServer.BotCommands.AdditionalOperations;

public class GetWeather(OpenWeatherService weatherService) : Command
{
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Update update)
    {
        CurrentWeather? weather;
        var userLocation = update.Message!.Location;

        string weatherQuery;
        if (userLocation is not null)
        {
            var lat = userLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var lon = userLocation.Longitude.ToString(CultureInfo.InvariantCulture);

            weather = await weatherService.GetCurrentWeather(lon, lat);
            weatherQuery = $"широта: {lat} \nдовгота: {lon}";
        }
        else
        {
            weatherQuery = update.Message.Text ?? string.Empty;
            var weatherLocations = await weatherService.GetLocations(weatherQuery);
            if (weatherLocations is null || weatherLocations.Count < 1)
            {
                await CommandUtils.SendErrorMessage(botClient, update);
                return;
            }

            var location = weatherLocations.First();
            weather = await weatherService.GetCurrentWeather(location.Lon, location.Lat);
        }

        if (weather is null)
        {
            await CommandUtils.SendErrorMessage(botClient, update);
            return;
        }

        await CommandUtils.SendWeatherMessage(botClient, update.Message.Chat.Id.ToString(), weather, weatherQuery);
    }
}
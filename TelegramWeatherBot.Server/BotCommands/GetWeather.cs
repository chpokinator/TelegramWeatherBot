using System.Globalization;
using OpenWeather;
using OpenWeather.Models.CurrentWeather;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramWeatherBotServer.Utilities;

namespace TelegramWeatherBotServer.BotCommands;

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

            weather = weatherService.GetCurrentWeather(lon, lat);
            weatherQuery = $"широта: {lat} \nдовгота: {lon}";
        }
        else
        {
            weatherQuery = update.Message.Text ?? string.Empty;
            var weatherLocations = weatherService.GetLocations(weatherQuery);
            if (weatherLocations?.Count < 1)
            {
                await SendErrorMessage(botClient, update);
                return;
            }

            var location = weatherLocations!.First();
            weather = weatherService.GetCurrentWeather(location.Lon, location.Lat);
        }

        if (weather is null)
        {
            await SendErrorMessage(botClient, update);
            return;
        }

        await SendWeatherMessage(botClient, update, weather, weatherQuery);
    }

    private async Task SendWeatherMessage(
        ITelegramBotClient botClient,
        Update update,
        CurrentWeather weather,
        string? weatherQuery)
    {
        var weatherInfo = weather.Weather?.FirstOrDefault();
        var weatherDescription = GetWeatherDescription(weatherInfo);
        var timeZoneHours = CommonUtils.ToHours(weather.Timezone);
        var iconUrl = weatherService.GetIconUrl(weatherInfo?.Icon);

        var text = $"*{CommonUtils.EscapeString(weatherQuery)}* \n\n" +
                   $"*Опис:* _{weatherDescription}_ \n" +
                   $"*Температура:* _{CommonUtils.EscapeString(weather.Main?.Temp)}\u00b0C_ \n" +
                   $"*Відчувається як:* _{CommonUtils.EscapeString(weather.Main?.FeelsLike)}\u00b0C_ \n" +
                   $"*Вологість:* _{weather.Main?.Humidity}%_ \n" +
                   $"*Вітер:* _{CommonUtils.EscapeString(weather.Wind?.Speed)}м/с_ \n" +
                   $"*Хмарність:* _{weather.Clouds?.Cloudiness}%_ \n" +
                   $"*Час виміру:* _{CommonUtils.ToDateTime(weather.DateTime):HH:mm}, пояс {GetHoursWithSymbol(timeZoneHours)} год_ \n" +
                   $"*Світанок:* _{CommonUtils.ToDateTime(weather.Sys?.Sunrise):HH:mm}, пояс {GetHoursWithSymbol(timeZoneHours)} год_ \n" +
                   $"*Захід:* _{CommonUtils.ToDateTime(weather.Sys?.Sunset):HH:mm}, пояс {GetHoursWithSymbol(timeZoneHours)} год_ ";

        await botClient.SendPhotoAsync(
            chatId: update.Message!.Chat.Id,
            photo: InputFile.FromUri(iconUrl),
            caption: text,
            parseMode: ParseMode.MarkdownV2,
            replyMarkup: new ReplyKeyboardRemove());
    }

    private static async Task SendErrorMessage(ITelegramBotClient botClient, Update update)
    {
        var text = "Інформації щодо локації не було знайдено 😢\\. Спробуйте ще раз";
        await botClient.SendTextMessageAsync(update.Message!.Chat.Id, text);
    }

    private static string GetWeatherDescription(Weather? weatherInfo)
    {
        return weatherInfo?.Description is null
            ? string.Empty
            : $"{char.ToUpper(weatherInfo.Description[0])}{weatherInfo.Description[1..]}";
    }

    private static string GetHoursWithSymbol(double hours)
    {
        return CommonUtils.EscapeString(
            hours > 0
                ? $"+{hours.ToString(CultureInfo.InvariantCulture)}"
                : hours.ToString(CultureInfo.InvariantCulture));
    }
}
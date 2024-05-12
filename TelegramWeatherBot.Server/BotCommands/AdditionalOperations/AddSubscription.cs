using System.Globalization;
using OpenWeather;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramWeatherBotServer.Services.Models;
using TelegramWeatherBotServer.Services.UsersManagement;
using TelegramWeatherBotServer.Utilities;
using WeatherLocation = OpenWeather.Models.Locations.Location;

namespace TelegramWeatherBotServer.BotCommands.AdditionalOperations;

public class AddSubscription(OpenWeatherService weatherService, UsersManagementService usersManagementService) : Command
{
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Update update)
    {
        var userLocation = update.Message!.Location;
        List<WeatherLocation>? weatherLocations;
        if (userLocation is not null)
        {
            var lat = userLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var lon = userLocation.Longitude.ToString(CultureInfo.InvariantCulture);
            weatherLocations = await weatherService.GetLocations(lat, lon);
        }
        else
        {
            var weatherQuery = update.Message.Text ?? string.Empty;
            weatherLocations = await weatherService.GetLocations(weatherQuery);
        }

        if (weatherLocations is null || weatherLocations.Count < 1)
        {
            await CommandUtils.SendErrorMessage(botClient, update);
            return;
        }

        var weatherLocation = weatherLocations.First();
        var telegramUserId = update.Message.From!.Id.ToString();
        var chatId = update.Message.Chat.Id.ToString();
        
        var result = await usersManagementService
            .AddSubscription(CreateSubscription(weatherLocation, telegramUserId, chatId));

        if (result)
        {
            await SendSubscriptionMessage(botClient, weatherLocation, chatId);
            return;
        }

        await SendSubscriptionAddingError(botClient, chatId);
    }

    private static async Task SendSubscriptionMessage(
        ITelegramBotClient botClient,
        WeatherLocation weatherLocation,
        string chatId)
    {
        var text = $"Успішно додана розсилка погоди: *{CommonUtils.EscapeString(weatherLocation.LocalNames!.Uk)}* 🚀";
        await botClient.SendTextMessageAsync(
            chatId, text, parseMode: ParseMode.MarkdownV2, replyMarkup: new ReplyKeyboardRemove());
    }

    private static async Task SendSubscriptionAddingError(ITelegramBotClient botClient, string chatId)
    {
        var text = "Розсилку не було додано. Спробуйте ще раз";
        await botClient
            .SendTextMessageAsync(chatId, text, parseMode: ParseMode.MarkdownV2, replyMarkup: new ReplyKeyboardRemove());
    }

    private static Subscription CreateSubscription(WeatherLocation location, string telegramUserId, string chatId)
    {
        return new Subscription
        {
            TelegramUserId = telegramUserId,
            ChatId = chatId,
            LocationName = location.LocalNames!.Uk,
            Coordinates = $"{location.Lat}:{location.Lon}"
        };
    }
}
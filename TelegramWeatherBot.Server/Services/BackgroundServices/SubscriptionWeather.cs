using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenWeather;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramWeatherBotServer.BotCommands;
using TelegramWeatherBotServer.Services.Models;
using TelegramWeatherBotServer.Services.UsersManagement;
using TelegramWeatherBotServer.Settings;
using TelegramWeatherBotServer.Utilities;

namespace TelegramWeatherBotServer.Services.BackgroundServices;

public class SubscriptionWeather(
    ITelegramBotClient botClient, 
    UsersManagementService usersManagementService,
    OpenWeatherService weatherService,
    IOptions<CommonSettings> options) : BackgroundService
{
    private readonly CommonSettings _commonSettings = options.Value;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromMinutes(_commonSettings.SubscriptionsSyncTimeoutInMinutes));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            Console.WriteLine("Started tick for Subscription weather background service");
            await SendSubscriptionWeather();
            Console.WriteLine("Ended tick for Subscription weather background service");
        }

        Console.WriteLine("Subscription weather background service stopped");
    }

    private async Task SendSubscriptionWeather()
    {
        var subscriptions = await usersManagementService.GetSubscriptions();
        if (subscriptions is null || subscriptions.Count < 1)
        {
            return;
        }

        var groupedSubscriptions = subscriptions.GroupBy(x => x.TelegramUserId);
        foreach (var subscriptionsGroup in groupedSubscriptions)
        {
            foreach (var subscription in subscriptionsGroup)
            {
                if (string.IsNullOrEmpty(subscription.TelegramUserId) || string.IsNullOrEmpty(subscription.ChatId))
                {
                    continue;
                }

                try
                {
                    var coordsSplit = subscription.Coordinates?.Split(':') ?? [];
                    if (coordsSplit.Length < 2)
                    {
                        await SendUnsuccessfulSubscriptionWeather(subscription.LocationName, subscription.ChatId);
                        continue;
                    }

                    var weatherInfo = await weatherService.GetCurrentWeather(coordsSplit[1], coordsSplit[0]);
                    if (weatherInfo?.Main is null)
                    {
                        await SendUnsuccessfulSubscriptionWeather(subscription.LocationName, subscription.ChatId);
                        continue;
                    }

                    var coords = string.Join(", ", coordsSplit);
                    await SendSubscriptionInfo(subscription.LocationName, coords, subscription.ChatId);
                    await CommandUtils.SendWeatherMessage(
                        botClient,
                        subscription.ChatId,
                        weatherInfo,
                        $"{subscription.LocationName}, {coords}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"There's an error occured while sending mesage to: {subscription.ChatId} chat\n{exception.Message}");
                }
                
            }
        }
    }

    private async Task SendUnsuccessfulSubscriptionWeather(string? location, string chatId)
    {
        await botClient.SendTextMessageAsync(
            chatId,
            $"*Не вдалося отримати інформацію щодо збереженої розсилки:*\n_{CommonUtils.EscapeString(location)}_",
            parseMode: ParseMode.MarkdownV2);
    }

    private async Task SendSubscriptionInfo(string? location, string coords, string chatId)
    {
        await botClient.SendTextMessageAsync(
            chatId,
            $"*Погодна інформація за розсилкою:*\n_{CommonUtils.EscapeString(location)}, {CommonUtils.EscapeString(coords)}_",
            parseMode: ParseMode.MarkdownV2);
    }
}
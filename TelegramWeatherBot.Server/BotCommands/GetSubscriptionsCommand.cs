using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramWeatherBotServer.Services.Models;
using TelegramWeatherBotServer.Services.UsersManagement;
using TelegramWeatherBotServer.Utilities;

namespace TelegramWeatherBotServer.BotCommands;

public class GetSubscriptionsCommand(UsersManagementService usersManagementService) : Command
{
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Update update)
    {
        var chatId = update.Message!.Chat.Id.ToString();
        var subscriptions = await usersManagementService.GetUserSubscriptions(update.Message!.From!.Id.ToString());
        await SendSubscriptions(botClient, chatId, subscriptions);
    }

    private static async Task SendSubscriptions(ITelegramBotClient botClient, string chatId, List<Subscription>? subscriptions)
    {
        if (subscriptions is null || subscriptions.Count < 1)
        {
            await botClient.SendTextMessageAsync(chatId, "Активних розсилок не було знайдено");
            return;
        }

        await botClient
            .SendTextMessageAsync(chatId, CreateSubscriptionsMessage(subscriptions!), parseMode: ParseMode.MarkdownV2);
    }

    private static string CreateSubscriptionsMessage(IEnumerable<Subscription> subscriptions)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("*Активні розсилки:*\n");

        var isFirst = true;
        foreach (var subscription in subscriptions)
        {
            var splitCoords = subscription.Coordinates?.Split(':');
            if (isFirst)
            {
                isFirst = !isFirst;
            }
            else
            {
                stringBuilder.AppendLine("\n——————\n");
            }
            stringBuilder.AppendLine($"_Назва: {CommonUtils.EscapeString(subscription.LocationName)}_");
            stringBuilder.AppendLine($"_Координати: {CommonUtils.EscapeString(string.Join(", ", splitCoords ?? []))}_");
        }

        return stringBuilder.ToString();
    }
}
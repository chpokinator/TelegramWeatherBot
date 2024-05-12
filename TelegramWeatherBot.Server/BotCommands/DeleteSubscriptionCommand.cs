using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramWeatherBotServer.Services.UsersManagement;

namespace TelegramWeatherBotServer.BotCommands;

public class DeleteSubscriptionCommand(UsersManagementService usersManagementService) : Command
{
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Update update)
    {
        var telegramUserId = update.Message!.From!.Id.ToString();
        var chatId = update.Message!.Chat.Id;
        var result = await usersManagementService.RemoveSubscriptions(telegramUserId);

        var text = result ? "Успішно видалено створені розсилки" : "Розсилок ще не було додано";
        await botClient.SendTextMessageAsync(chatId, text);
    }
}
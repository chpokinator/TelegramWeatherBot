using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramWeatherBotServer.BotCommands;

public class DefaultCommand : Command
{
    public override Task ExecuteAsync(ITelegramBotClient botClient, Update update)
    {
        return Task.CompletedTask;
    }
}
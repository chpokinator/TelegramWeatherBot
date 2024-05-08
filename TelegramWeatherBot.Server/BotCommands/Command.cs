using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramWeatherBotServer.BotCommands;

public abstract class Command
{
    public abstract Task ExecuteAsync(ITelegramBotClient botClient, Update update);
}
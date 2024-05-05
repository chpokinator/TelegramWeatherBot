using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramWeatherBotServer.Settings;

namespace TelegramWeatherBotServer.Services;

public class TelegramBot(IOptions<CommonSettings> commonSettings)
{
    private readonly TelegramBotClient _botClient = new(commonSettings.Value.BotToken);

    private TelegramBotClient GetBotClient()
    {
        return _botClient;
    }
}
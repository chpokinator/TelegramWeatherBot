using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace TelegramWeatherBotServer.Services.BackgroundServices;

public class TelegramBotUpdate(CommandsCoordinator coordinator, ITelegramBotClient botClient) : BackgroundService
{
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly CommandsCoordinator _coordinator = coordinator;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _botClient.StartReceiving(UpdateAsync, PollingErrorAsync, cancellationToken: stoppingToken);
        return Task.CompletedTask;
    }
    
    private async Task UpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        await _coordinator.StartCommandsExecution(botClient, update);
    }
    
    private static Task PollingErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}
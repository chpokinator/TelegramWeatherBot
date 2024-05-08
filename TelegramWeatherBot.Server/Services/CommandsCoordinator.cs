using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramWeatherBotServer.BotCommands;

namespace TelegramWeatherBotServer.Services;

public class CommandsCoordinator(CommandsService commandsService)
{
    private readonly Dictionary<string, List<Command>> _commands = commandsService.GetCommands();
    private readonly List<UserOperation> _usersOperations = [];

    public Task StartCommandsExecution(ITelegramBotClient botClient, Update update)
    {
        var message = update.Message;
        if (message is null)
        {
            return Task.CompletedTask;
        }
        
        var userId = message.From?.Id.ToString();
        if (string.IsNullOrEmpty(userId))
        {
            return Task.CompletedTask;
        }
        
        var chatId = message.Chat.Id.ToString();
        AddUserOperationIfNotExists(userId, chatId);

        if (message.Entities?.FirstOrDefault()?.Type is not MessageEntityType.BotCommand)
        {
            return ExecuteCommand(botClient, update, userId);
        }
        
        var commandName = message.EntityValues!.First();
        return ExecuteCommand(botClient, update, userId, commandName);

    }
    
    private async Task ExecuteCommand(
        ITelegramBotClient botClient,
        Update update,
        string userId,
        string commandsKey)
    {
        var userOperation = GetUserOperation(userId);
        if (userOperation is null)
        {
            return;
        }

        var commands = GetCommands(commandsKey);
        if (commands is null)
        {
            return;
        }
        
        userOperation.SetCommandsQueue(commands);
        var currentCommand = userOperation.CommandsQueue!.Dequeue();
        await currentCommand.ExecuteAsync(botClient, update);
    }
    
    private async Task ExecuteCommand(
        ITelegramBotClient botClient,
        Update update,
        string userId)
    {
        var userOperation = GetUserOperation(userId);
        if (userOperation is null)
        {
            return;
        }

        if (userOperation.CommandsQueue?.TryDequeue(out var currentCommand) is null or false)
        {
            return;
        }

        await currentCommand.ExecuteAsync(botClient, update);
    }

    private void AddUserOperationIfNotExists(string userId, string chatId)
    {
        var userOperation = GetUserOperation(userId);
        if (userOperation is not null)
        {
            return;
        }
        
        userOperation = new UserOperation(userId, chatId);
        _usersOperations.Add(userOperation);
    }

    private List<Command>? GetCommands(string commandsKey)
    {
        return _commands.GetValueOrDefault(commandsKey);
    }

    private UserOperation? GetUserOperation(string userId)
    {
        return _usersOperations.FirstOrDefault(x => x.TelegramUserId == userId);
    }
}
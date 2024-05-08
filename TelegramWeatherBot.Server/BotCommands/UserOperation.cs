namespace TelegramWeatherBotServer.BotCommands;

public class UserOperation(string telegramUserId, string chatId)
{
    public string TelegramUserId { get; private set; } = telegramUserId;
    public string ChatId { get; private set; } = chatId;
    public Queue<Command>? CommandsQueue { get; private set; }

    public void SetCommandsQueue(IEnumerable<Command> commands) => CommandsQueue = new Queue<Command>(commands);
}
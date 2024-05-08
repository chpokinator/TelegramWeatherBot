using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static TelegramWeatherBotServer.BotCommands.Commands;

namespace TelegramWeatherBotServer.BotCommands;

public class HelpCommand : Command
{
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Update update)
    {
        await SendHelpMessage(botClient, update);
    }

    private static async Task SendHelpMessage(ITelegramBotClient botClient, Update update)
    {
        var updateMessage = update.Message;
        var botName = botClient.GetMyNameAsync().Result.Name;
        
        var text = $"Привіт *{updateMessage!.From?.FirstName}*, я *{botName}*\\. Я допоможу тобі дізнатися актуальну інформацію щодо погоди\\. 😎 \n\n" +
                      $"Перелік доступних команд: \n" +
                      $"{HELP} — _відобразити поточне повідомлення_ \n" +
                      $"{GET_WEATHER} — _дізнатися актуальні дані щодо погоди_ \n" +
                      $"{ADD_SUBSCRIPTION} — _додати розсилку на актуальну погоду_ \n" +
                      $"{GET_SUBSCRIPTION} — _переглянути активовану розсилку_ \n" +
                      $"{DELETE_SUBSCRIPTION} — _відмовитися від розсилки_";

        await botClient.SendTextMessageAsync(updateMessage.Chat.Id, text, parseMode: ParseMode.MarkdownV2);
    }
}
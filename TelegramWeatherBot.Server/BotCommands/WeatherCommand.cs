using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramWeatherBotServer.BotCommands;

public class WeatherCommand : Command
{
    public override async Task ExecuteAsync(ITelegramBotClient botClient, Update update)
    {
        var text = "Для отримання актуальної інформації щодо погоди, введіть назву населеного пункту, міста або " +
                   "надайте локацію за допомогою клавіші *_Надати геопозицію_*\n\n" +
                   "Формат пошукового запиту:\n _\\{назва\\}, \\{поштовий індекс\\}, \\{код країни\\}_\n" +
                   "Приклад:\n _позняки, ua_";

        var replyKeyboard = new ReplyKeyboardMarkup(new[]
        {
            KeyboardButton.WithRequestLocation("Надати геопозицію 📌")
        })
        {
            ResizeKeyboard = true
        };
        
        await botClient.SendTextMessageAsync(
            update.Message!.Chat.Id, text, parseMode: ParseMode.MarkdownV2, replyMarkup: replyKeyboard);
    }
}
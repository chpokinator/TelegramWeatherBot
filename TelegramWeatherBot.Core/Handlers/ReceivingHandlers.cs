using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramWeatherBot.Core.Handlers;

public class ReceivingHandlers
{
    public static async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        var message = update.Message;
        if (message is null)
        {
            return;
        }

        if (message.Entities?.FirstOrDefault()?.Type is MessageEntityType.BotCommand)
        {
            if (message.From is null)
            {
                Console.WriteLine("From property is empty");
            }
            
            await botClient
                .SendTextMessageAsync(message.From!.Id, "я твою команду получил", cancellationToken: cancellationToken);
            Console.WriteLine($"было отправлено юзеру {message.From.Id} {message.From.Username} команду {message.EntityValues?.First()}");
            
            return;
        }
        
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new List<KeyboardButton>{"/start", "/puk"}
            })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(message.From!.Id, "выбери что-то", replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);


        // if (string.IsNullOrEmpty(message.Text))
        // {
        //     await botClient
        //         .SendTextMessageAsync(message.Chat.Id, "Я тебя не понимаю", cancellationToken: cancellationToken);
        //     Console.WriteLine($"было отправлено сообщение в чат {message.Chat.Id}");
        // }
    }
    
    public static Task HandlePollingErrorAsync(
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
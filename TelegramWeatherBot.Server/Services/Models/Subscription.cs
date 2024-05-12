namespace TelegramWeatherBotServer.Services.Models;

public class Subscription
{
    public string? TelegramUserId { get; set; }
    public string? ChatId { get; set; }
    public string? LocationName { get; set; }
    public string? Coordinates { get; set; }
}
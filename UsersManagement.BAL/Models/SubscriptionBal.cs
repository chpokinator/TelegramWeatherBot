namespace UsersManagement.BAL.Models;

public class SubscriptionBal
{ 
    public required string TelegramUserId { get; set; }
    public required string ChatId { get; set; }
    public required string LocationName { get; set; }
    public required string Coordinates { get; set; }
}
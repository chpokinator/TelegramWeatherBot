using System.ComponentModel.DataAnnotations;

namespace UsersManagement.DAL.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    
    [MaxLength(30)]
    public required string TelegramUserId { get; set; }
    
    [MaxLength(30)]
    public required string ChatId { get; set; }
    
    [MaxLength(250)]
    public required string LocationName { get; set; }
    
    [MaxLength(250)]
    public required string Coordinates { get; set; }
}
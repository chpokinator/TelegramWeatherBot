using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Dto;

public class SubscriptionDto
{
    [Required(AllowEmptyStrings = false)]
    public required string TelegramUserId { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public required string ChatId { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public required string LocationName { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public required string Coordinates { get; set; }
}
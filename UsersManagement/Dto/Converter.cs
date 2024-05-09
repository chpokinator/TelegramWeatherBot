using UsersManagement.BAL.Models;

namespace UsersManagement.Dto;

public static class Converter
{
    public static SubscriptionBal ToBal(this SubscriptionDto subscriptionDto)
    {
        return new SubscriptionBal
        {
            TelegramUserId = subscriptionDto.TelegramUserId,
            ChatId = subscriptionDto.ChatId,
            LocationName = subscriptionDto.LocationName,
            Coordinates = subscriptionDto.Coordinates
        };
    }

    public static List<SubscriptionDto> ToEnumerableDto(this IEnumerable<SubscriptionBal> subscriptions)
    {
        return subscriptions.Select(ToDto).ToList();
    }

    private static SubscriptionDto ToDto(this SubscriptionBal subscription)
    {
        return new SubscriptionDto
        {
            TelegramUserId = subscription.TelegramUserId,
            ChatId = subscription.ChatId,
            LocationName = subscription.LocationName,
            Coordinates = subscription.Coordinates
        };

    }
}
using UsersManagement.BAL.Models;
using UsersManagement.DAL.Entities;

namespace UsersManagement.BAL.Services;

public static class Converter
{
    public static Subscription ToDal(this SubscriptionBal subscriptionBal)
    {
        return new Subscription
        {
            TelegramUserId = subscriptionBal.TelegramUserId,
            ChatId = subscriptionBal.ChatId,
            LocationName = subscriptionBal.LocationName,
            Coordinates = subscriptionBal.Coordinates
        };
    }

    public static List<SubscriptionBal> ToEnumerableBal(this IEnumerable<Subscription> subscriptions)
    {
        return subscriptions.Select(ToBal).ToList();
    }

    private static SubscriptionBal ToBal(this Subscription subscription)
    {
        return new SubscriptionBal
        {
            TelegramUserId = subscription.TelegramUserId,
            ChatId = subscription.ChatId,
            LocationName = subscription.LocationName,
            Coordinates = subscription.Coordinates
        };

    }
}
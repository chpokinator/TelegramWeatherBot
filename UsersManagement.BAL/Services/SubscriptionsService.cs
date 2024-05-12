using UsersManagement.BAL.Models;
using UsersManagement.DAL.Repositories.Interfaces;

namespace UsersManagement.BAL.Services;

public class SubscriptionsService(ISubscriptionsRepository repository)
{
    public Task<bool> RemoveUserSubscriptions(string telegramUserId)
    {
        return repository.RemoveSubscriptions(telegramUserId);
    }

    public Task<bool> CreateSubscription(SubscriptionBal subscription)
    {
        return repository.AddSubscription(subscription.ToDal());
    }

    public async Task<List<SubscriptionBal>> GetUserSubscriptions(string telegramUserId)
    {
        var subscriptions = await repository.GetSubscriptions(telegramUserId);
        return subscriptions.ToEnumerableBal();
    }

    public async Task<List<SubscriptionBal>> GetSubscriptions()
    {
        var subscriptions = await repository.GetSubscriptions();
        return subscriptions.ToEnumerableBal();
    }
}
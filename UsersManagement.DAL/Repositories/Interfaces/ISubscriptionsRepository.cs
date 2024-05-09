using UsersManagement.DAL.Entities;

namespace UsersManagement.DAL.Repositories.Interfaces;

public interface ISubscriptionsRepository
{
    Task<bool> RemoveSubscriptions(string telegramUserId);
    Task<List<Subscription>> GetSubscriptions(string telegramUserId);
    Task<bool> AddSubscription(Subscription subscription);
}
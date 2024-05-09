using Microsoft.EntityFrameworkCore;
using UsersManagement.DAL.Entities;
using UsersManagement.DAL.Repositories.Interfaces;

namespace UsersManagement.DAL.Repositories;

public class SubscriptionsRepository(DataContext context) : ISubscriptionsRepository
{
    public async Task<bool> RemoveSubscriptions(string telegramUserId)
    {
        var activeSubscriptions = await context.Subscriptions
            .AsNoTracking()
            .Where(x => x.TelegramUserId == telegramUserId).ToListAsync();

        if (activeSubscriptions.Count < 1)
        {
            return false;
        }
        
        context.Subscriptions.RemoveRange(activeSubscriptions);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Subscription>> GetSubscriptions(string telegramUserId)
    {
        return await context.Subscriptions.AsNoTracking().ToListAsync();
    }

    public async Task<bool> AddSubscription(Subscription subscription)
    {
        await context.Subscriptions.AddAsync(subscription);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }
}
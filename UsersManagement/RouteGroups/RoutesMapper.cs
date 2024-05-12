using UsersManagement.RouteHandlers;

namespace UsersManagement.RouteGroups;

public static class RoutesMapper
{
    public static RouteGroupBuilder MapSubscriptionsApi(this RouteGroupBuilder group)
    {
        group.MapPost("/", SubscriptionsHandlers.AddSubscription);
        group.MapDelete("/{telegramUserId}", SubscriptionsHandlers.RemoveSubscriptions);
        group.MapGet("/", SubscriptionsHandlers.GetSubscriptions);
        group.MapGet("/{telegramUserId}", SubscriptionsHandlers.GetUserSubscriptions);
        return group;
    }
}
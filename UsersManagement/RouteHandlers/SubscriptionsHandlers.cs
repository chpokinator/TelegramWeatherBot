using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.BAL.Services;
using UsersManagement.Dto;

namespace UsersManagement.RouteHandlers;

public static class SubscriptionsHandlers
{
    public static async Task<IResult> AddSubscription(
        [FromBody, Required] SubscriptionDto subscriptionDto,
        SubscriptionsService service)
    {
        var result = await service.CreateSubscription(subscriptionDto.ToBal());
        return result ? TypedResults.Ok() : TypedResults.BadRequest();
    }

    public static async Task<IResult> RemoveSubscriptions(
        [Required(AllowEmptyStrings = false)]
        string telegramUserId,
        SubscriptionsService service)
    {
        var result = await service.RemoveUserSubscriptions(telegramUserId);
        return result ? TypedResults.Ok() : TypedResults.BadRequest();
    }

    public static async Task<IResult> GetUserSubscriptions(
        [Required(AllowEmptyStrings = false)]
        string telegramUserId,
        SubscriptionsService service)
    {
        var result = await service.GetUserSubscriptions(telegramUserId);
        return TypedResults.Json(result.ToEnumerableDto());
    }
}
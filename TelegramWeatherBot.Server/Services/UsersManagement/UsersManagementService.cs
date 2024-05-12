using System.Net;
using HttpHelpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using TelegramWeatherBotServer.Services.Models;
using TelegramWeatherBotServer.Settings;

namespace TelegramWeatherBotServer.Services.UsersManagement;

public class UsersManagementService(RequestSender sender, IOptions<CommonSettings> settings)
{
    private readonly CommonSettings _settings = settings.Value;

    public async Task<bool> AddSubscription(Subscription subscription)
    {
        var body = new JObject
        {
            { "Coordinates", subscription.Coordinates },
            { "ChatId", subscription.ChatId },
            { "LocationName", subscription.LocationName },
            { "TelegramUserId", subscription.TelegramUserId }
        };

        var message = new RequestBuilder(HttpMethod.Post, _settings.UsersManagementServiceHost)
            .SetJsonBody(body)
            .Build();


        var response = await sender.SendRequest(message);
        return response?.StatusCode is HttpStatusCode.OK;
    }

    public async Task<bool> RemoveSubscriptions(string telegramUserId)
    {
        var message = new RequestBuilder(
                HttpMethod.Delete,
                $"{_settings.UsersManagementServiceHost}/{telegramUserId}")
            .Build();

        var response = await sender.SendRequest(message);
        return response?.StatusCode is HttpStatusCode.OK;
    }

    public async Task<List<Subscription>?> GetUserSubscriptions(string telegramUserId)
    {
        var message = new RequestBuilder(
                HttpMethod.Get,
                $"{_settings.UsersManagementServiceHost}/{telegramUserId}")
            .Build();

        var response = await sender.SendRequest(message);
        var token = response.ToJToken();
        return token is not JArray jArray ? default : jArray.ToObject<List<Subscription>>();
    }

    public async Task<List<Subscription>?> GetSubscriptions()
    {
        var message = new RequestBuilder(
                HttpMethod.Get,
                $"{_settings.UsersManagementServiceHost}")
            .Build();

        var response = await sender.SendRequest(message);
        var token = response.ToJToken();
        return token is not JArray jArray ? default : jArray.ToObject<List<Subscription>>();
    }
}
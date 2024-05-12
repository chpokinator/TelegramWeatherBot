using OpenWeather;
using TelegramWeatherBotServer.BotCommands;
using TelegramWeatherBotServer.BotCommands.AdditionalOperations;
using TelegramWeatherBotServer.Services.UsersManagement;

namespace TelegramWeatherBotServer.Services;

public class CommandsService(OpenWeatherService weatherService, UsersManagementService usersManagementService)
{
    public Dictionary<string, List<Command>> GetCommands()
    {
        return new Dictionary<string, List<Command>>
        {
            { Commands.DEFAULT, [new DefaultCommand()] },
            { Commands.START, [new HelpCommand()] },
            { Commands.HELP, [new HelpCommand()] },
            { Commands.GET_WEATHER, [new WeatherCommand(), new GetWeather(weatherService)] },
            { Commands.ADD_SUBSCRIPTION, [new AddSubscriptionCommand(), new AddSubscription(weatherService, usersManagementService)] },
            { Commands.GET_SUBSCRIPTION, [new GetSubscriptionsCommand(usersManagementService)] },
            { Commands.DELETE_SUBSCRIPTION, [new DeleteSubscriptionCommand(usersManagementService)] }
        };
    }
}
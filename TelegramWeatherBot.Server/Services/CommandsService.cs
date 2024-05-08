using OpenWeather;
using TelegramWeatherBotServer.BotCommands;

namespace TelegramWeatherBotServer.Services;

public class CommandsService(OpenWeatherService weatherService)
{
    public Dictionary<string, List<Command>> GetCommands()
    {
        return new Dictionary<string, List<Command>>
        {
            { Commands.DEFAULT, [new DefaultCommand()] },
            { Commands.START, [new HelpCommand()] },
            { Commands.HELP, [new HelpCommand()] },
            { Commands.GET_WEATHER, [new WeatherCommand(), new GetWeather(weatherService)] }
        };
    }
}
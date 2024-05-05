namespace TelegramWeatherBotServer.Http;

public class RequestBuilder
{
    private readonly HttpRequestMessage _httpRequestMessage;
    private readonly List<Parameter> _parameters = [];

    public RequestBuilder(HttpMethod method, string baseAddress)
    {
        _httpRequestMessage = new HttpRequestMessage(method, new Uri(baseAddress, UriKind.RelativeOrAbsolute));
    }

    public RequestBuilder AddParameter(Parameter parameter)
    {
        _parameters.Add(parameter);
        return this;
    }

    public RequestBuilder AddParameters(IEnumerable<Parameter> parameters)
    {
        _parameters.AddRange(parameters);
        return this;
    }

    public HttpRequestMessage Build()
    {
        if (_parameters.Count <= 0)
        {
            return _httpRequestMessage;
        }
        
        var uriBuilder = new UriBuilder(_httpRequestMessage.RequestUri!)
        {
            Query = _parameters.ToQueryString()
        };
        
        _httpRequestMessage.RequestUri = uriBuilder.Uri;
        return _httpRequestMessage;
    }
}
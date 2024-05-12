namespace HttpHelpers;

public class RequestSender(IHttpClientFactory clientFactory)
{
    public async Task<HttpResponseMessage?> SendRequest(HttpRequestMessage request)
    {
        var httpClient = clientFactory.CreateClient();
        HttpResponseMessage? responseMessage = default;
        try
        {
            responseMessage = await httpClient.SendAsync(request);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }

        return responseMessage;
    }
}
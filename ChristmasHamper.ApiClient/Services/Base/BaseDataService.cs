namespace ChristmasHamper.ApiClient.Services.Base;

public class BaseDataService
{
    protected readonly IClient _client;

    public BaseDataService(IClient client)
    {
        _client = client;
    }
}

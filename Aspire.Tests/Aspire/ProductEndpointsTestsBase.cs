namespace Aspire.Tests;

public abstract class ProductEndpointsTestsBase : IAsyncLifetime
{
    protected HttpClient _client;
    private ApiFixture _fixture;

    protected ProductEndpointsTestsBase()
    {
        _client = _fixture.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _fixture.DisposeAsync();
        _client.Dispose();
    }

    public async Task InitializeAsync()
    {
        _fixture = new ApiFixture();
        await _fixture.InitializeAsync();
        _client = _fixture.CreateClient();

    }
}
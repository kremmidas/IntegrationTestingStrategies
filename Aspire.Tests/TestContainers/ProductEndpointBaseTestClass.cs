namespace Testcontainers.Tests
{
    public abstract class ProductEndpointBaseTestClass : IAsyncLifetime
    {
        private TestContainersFixture _fixture;
        public HttpClient _client;

        public async Task InitializeAsync()
        {
            _fixture = new TestContainersFixture();
            await _fixture.InitializeAsync();
            _client = _fixture.CreateClient();
        }

        public async Task DisposeAsync()
        {
            await _fixture.DisposeAsync();
            _client.Dispose();
        }
    }
}

using Api;
using Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Testcontainers.Tests;

public class TestContainersFixture : WebApplicationFactory<Program>
{
    private PostgreSqlContainer _sqlContainer;

    public async Task InitializeAsync()
    {
        _sqlContainer = new PostgreSqlBuilder()
        .WithImage(PostgreSqlBuilder.PostgreSqlImage)
        .Build();

        await _sqlContainer.StartAsync();
        await EnsureDatabaseCreated();
    }

    public async Task DisposeAsync()
    {
        await _sqlContainer.StopAsync();
        await _sqlContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_sqlContainer.GetConnectionString());
            });
        });
    }

    private async Task EnsureDatabaseCreated()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(_sqlContainer.GetConnectionString());

        using var context = new AppDbContext(optionsBuilder.Options);
        await context.Database.EnsureCreatedAsync();
    }
}

using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using IntegrationTests.Fakes;

namespace IntegrationTests.Factory;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public FakeGameRepository FakeRepo { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IGameRepository));

            if (descriptor != null) services.Remove(descriptor);

            services.AddSingleton<IGameRepository>(FakeRepo);
        });
    }
}

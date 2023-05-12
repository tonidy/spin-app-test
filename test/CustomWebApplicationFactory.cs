using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpinGameApp;

namespace SpinGameTest
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<Program> where TProgram : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(new ConfigurationBuilder()
                        .AddJsonFile("appsettings.Integration.json")
                        .Build());
            });

            builder.UseEnvironment("Integration");
        }

        public WebApplicationFactory<Program> InjectMongoDbConfigurationSettings(string connectionString, string databaseName)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<MongoDbSettings>(_ => new MongoDbSettings(connectionString, databaseName));
                });
            });
        }
    }

}
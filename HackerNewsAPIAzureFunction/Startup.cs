using HackerNewsAPI.Interfaces;
using HackerNewsAPI.Models;
using HackerNewsAPI.Services;
using HackerNewsAPIAzureFunction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(Startup))]
namespace HackerNewsAPIAzureFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {

        builder.Services.AddOptions<Settings>()
        .Configure<IConfiguration>((settings, configuration) =>
        {
            configuration.GetSection("Settings").Bind(settings);
        });

        builder.Services.AddScoped<IStoriesService, StoriesService>();
        builder.Services.AddMemoryCache();

    }
}
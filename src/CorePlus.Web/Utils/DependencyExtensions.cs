using CorePlus.Modules.Reporting.Perisistence;

namespace CorePlus.Web.Utils;

public static class DependencyExtensions
{
    public static IServiceCollection AddElasticSearchClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetSection("ElasticSearch");
        var client = ElasticClientBuilder.CreateClient()
            .WithCredentials(config["Server"], config["Username"], config["Password"])
            .Create();
        
        services.AddScoped(sp => client);
        return services;
    }
}
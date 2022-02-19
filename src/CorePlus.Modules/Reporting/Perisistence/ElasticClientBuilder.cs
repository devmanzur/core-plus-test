using Elasticsearch.Net;
using Nest;

namespace CorePlus.Modules.Reporting.Perisistence;

public class ElasticClientBuilder : IConfigurationProviderStage, IClientCreateStage
{
    private string _server;
    private string _username;
    private string _password;

    public IClientCreateStage WithCredentials(string server, string username, string password)
    {
        _server = server;
        _username = username;
        _password = password;
        return this;
    }
    
    public static IConfigurationProviderStage CreateClient()
    {
        return new ElasticClientBuilder();
    }

    public ElasticClient Create()
    {
        var nodes = new[]
        {
            new Uri(_server),                
        };

        var pool = new StaticConnectionPool(nodes);
        var settings = new ConnectionSettings(pool)
            .DefaultIndex("reports");
        
        if (!string.IsNullOrWhiteSpace(_username))
        {
            settings.BasicAuthentication(_username, _password);
        }
            
        return new ElasticClient(settings);
    }
}

public interface IConfigurationProviderStage
{
    public IClientCreateStage WithCredentials(string server, string username, string password);
}

public interface IClientCreateStage
{
    public ElasticClient Create();
}
using CorePlus.Modules.Reporting.Models;
using Nest;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportService
{
    private readonly ElasticClient _elasticClient;

    public AppointmentReportService(ElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    private static string ReportIndex() => $"reports_{DateTime.UtcNow:yyyy.MM.dd}";
    
    private async Task EnsureIndexExists()
    {
        var index = await _elasticClient.Indices.ExistsAsync(ReportIndex());
        if (!index.Exists)
        {
            CreateIndex(_elasticClient, ReportIndex());
        }
    }

    private static void CreateIndex(IElasticClient client, string indexName)
    {
        client.Indices.Create(indexName,
            index => index.Map<AppointmentRecord>(x => x.AutoMap()
            )
        );
    }
}
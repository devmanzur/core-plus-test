using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Reporting.Models;
using Nest;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportRepository
{
    private readonly ElasticClient _elasticClient;
    private readonly IUnitOfWork _unitOfWork;
    private const string defaultIndex = "appointments*";

    public AppointmentReportRepository(ElasticClient elasticClient,IUnitOfWork unitOfWork)
    {
        _elasticClient = elasticClient;
        _unitOfWork = unitOfWork;
    }

    private static string ReportIndex() => $"appointments_{DateTime.UtcNow:yyyy.MM.dd}";
    
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
using CorePlus.Modules.Reporting.Models;
using Nest;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportRepository
{
    public async Task<List<AppointmentCostRevenueSummaryDto>> GetAppointments(long practitionerId, DateTime startDate, DateTime endDate)
    {
        var filters = new List<Func<QueryContainerDescriptor<AppointmentRecord>, QueryContainer>>
        {
            fq =>
                fq.DateRange(c =>
                    c.Field(x => x.Date).GreaterThanOrEquals(startDate.Date)
                        .LessThanOrEquals(endDate.Date)),
            fq => fq.Terms(t => t
                .Field(f => f.Practitioner.Id)
                .Terms(practitionerId)
            )
        };
        
        var searchResponse = await _elasticClient.SearchAsync<AppointmentRecord>(x =>
            x.Query(q =>
                    q.Bool(b => b.Filter(filters)))
                .Size(10000)
                .TrackTotalHits(true)
                .Index(defaultIndex));
        
        if (!searchResponse.IsValid)
        {
            return new List<AppointmentCostRevenueSummaryDto>();
        }

        return searchResponse.Documents.Select(x => new AppointmentCostRevenueSummaryDto()
        {
            Cost = x.Cost,
            Date = x.Date.ToString("g"),
            Revenue = x.Revenue,
            AppointmentId = x.UniqueId,
            PractitionerId = x.Practitioner.Id,
            PractitionerName = x.Practitioner.Name

        }).ToList();
    }
}
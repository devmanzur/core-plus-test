﻿using CorePlus.Modules.Reporting.Models;
using Microsoft.Data.SqlClient;
using Nest;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportRepository
{
    public async Task<List<MonthlyCostRevenueBucketDto>> GetMonthlyProfitSummary(List<int> practitionerIds,
        DateTime startDate, DateTime endDate)
    {
        var filters = new List<Func<QueryContainerDescriptor<AppointmentRecord>, QueryContainer>>
        {
            fq =>
                fq.DateRange(c =>
                    c.Field(x => x.Date).GreaterThanOrEquals(startDate.Date)
                        .LessThanOrEquals(endDate.Date)),
            fq => fq.Terms(t => t
                .Field(f => f.Practitioner.Id)
                .Terms(practitionerIds)
            )
        };

        var searchResponse = await _elasticClient.SearchAsync<AppointmentRecord>(x =>
            x.Query(q =>
                    q.Bool(b => b.Filter(filters)))
                .Size(0)
                .Aggregations(a =>
                    a.DateHistogram("revenue_per_month", date => date
                        .Field(d => d.Date)
                        .CalendarInterval(DateInterval.Month)
                        .MinimumDocumentCount(2)
                        .Format("yyyy-MM-dd")
                        .Order(HistogramOrder.CountAscending)
                        .Aggregations(aggs => aggs
                            .Histogram("per_practitioner", p => p.Field(f => f.Practitioner.Id)
                                .Interval(1)
                                .MinimumDocumentCount(2)
                                .Order(HistogramOrder.CountAscending)
                                .Aggregations(childAggs => childAggs
                                    .Sum("total_revenue", tr => tr.Field(f => f.Revenue))
                                    .Sum("total_cost", tc => tc.Field(f => f.Cost))
                                )))
                    ))
                .TrackTotalHits(true)
                .Index(defaultIndex));

        var result = searchResponse.Aggregations.DateHistogram("revenue_per_month")
            .Buckets.SelectMany(x => x.Histogram("per_practitioner").Buckets.Select(v =>
            {
                if (x.DocCount == null)
                    return new MonthlyCostRevenueBucketDto
                    {
                        Date = x.KeyAsString,
                        PractitionerId = (int) v.Key,
                        TotalRevenue = 0,
                        TotalCost = 0,
                    };
                var revenue = v.ValueCount("total_revenue").Value;
                var cost = v.ValueCount("total_cost").Value;
                if (revenue != null && cost != null)
                    if (x.DocCount != null)
                        return new MonthlyCostRevenueBucketDto
                        {
                            Date = x.KeyAsString,
                            PractitionerId = (int) v.Key,
                            TotalRevenue = Math.Round(revenue.Value, 4),
                            TotalCost = Math.Round(cost.Value, 4),
                        };

                return new MonthlyCostRevenueBucketDto
                {
                    Date = x.KeyAsString,
                    TotalRevenue = 0,
                    TotalCost = 0,
                };
            })).ToList();

        return result;
    }

    public async Task<List<MonthlyCostRevenueSummaryDto>> GetMonthlyProfitSummaryBySql(List<int> practitionerIds,
        DateTime startDate, DateTime endDate)
    {
        var ids = $"({string.Join(",", practitionerIds.ToArray())})";

        var sqlQuery = $@"SELECT  GroupingYear = DATEPART(YEAR, Date),
                    GroupingMonth = DATEPART(MONTH, Date),
                    a.PractitionerId,
                    PractitionerName,
                    SUM(Cost) TotalCost,
                    SUM(Revenue) TotalRevenue
                    FROM    [appointments].Appointments a
                    LEFT JOIN (SELECT Id as PractitionerId, Name as PractitionerName from [appointments].Practitioners) p on p.PractitionerId = a.PractitionerId
                    WHERE   Date >= @startDate 
                    AND     Date <= @endDate
                    AND a.PractitionerId IN {ids}
                    GROUP BY DATEPART(YEAR, Date), DATEPART(MONTH, Date), a.PractitionerId, PractitionerName
                    ORDER BY GroupingYear, GroupingMonth;";


        var summary = await _unitOfWork.GetListAsync<MonthlyCostRevenueSummaryDto>(sqlQuery,
            new SqlParameter("@startDate", startDate), new SqlParameter("@endDate", endDate));

        return summary;
    }
}
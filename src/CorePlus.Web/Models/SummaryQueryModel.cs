namespace CorePlus.Web.Models;

public class SummaryQueryModel
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<long> Practitioners { get; set; } = new List<long>();
}

public class PractitionerSummaryQueryModel
{
    public DateTime Month { get; set; }

    public long PractitionerId { get; set; }
}
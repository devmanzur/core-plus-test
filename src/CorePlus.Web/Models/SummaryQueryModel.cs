namespace CorePlus.Web.Models;

public class SummaryQueryModel
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Practitioners { get; set; }
    
    public List<long> PractitionerIds => Practitioners.Split(',').ToList().ConvertAll(Convert.ToInt64);

}

public class PractitionerSummaryQueryModel
{
    public DateTime Month { get; set; }

    public long PractitionerId { get; set; }
}
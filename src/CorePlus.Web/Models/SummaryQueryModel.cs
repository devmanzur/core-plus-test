namespace CorePlus.Web.Models;

public class SummaryQueryModel
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<int> Practitioners { get; set; } = new List<int>();
    public string? Source { get; set; }
}
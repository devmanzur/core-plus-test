namespace CorePlus.Modules.Shared.Interfaces;

public abstract class BaseEntity
{
    //database generated id
    public long Id { get; set; }
    //application generated id shared across databases
    public Guid UniqueId { get; set; }
}
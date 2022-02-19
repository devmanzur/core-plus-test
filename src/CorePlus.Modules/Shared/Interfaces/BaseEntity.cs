namespace CorePlus.Modules.Shared.Interfaces;

public abstract class BaseEntity
{
    //database generated id
    public Guid Id { get; set; }
    //application generated id shared across databases
    public Guid UniqueId { get; set; }
}
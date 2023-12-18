namespace KashmirServices.Domain.Shared;

public abstract class BaseEntity
{
    public Guid Id { get; set; }


    public Guid? CreatedBy { get; set; }


    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;


    public Guid? UpdatedBy { get; set; }


    public DateTimeOffset? UpdatedOn { get; set; }
}
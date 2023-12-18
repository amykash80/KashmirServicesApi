using KashmirServices.Domain.Enums;
using KashmirServices.Domain.Shared;

namespace KashmirServices.Domain.Entities;

public sealed class AppFile : BaseEntity
{
    public Module Module { get; set; }


    public string FilePath { get; set; } = string.Empty;


    public Guid EntityId { get; set; }
}

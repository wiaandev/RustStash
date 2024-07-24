namespace RustStash.Core.Entities.Auth;

using System.ComponentModel.DataAnnotations.Schema;
using RustStash.Core.Entities.Shared;

public class SystemEntity : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int PartyId { get; set; }

    public Party Party { get; set; } = null!;
}

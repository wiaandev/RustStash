namespace RustStash.Core.Entities.Shared;

using System;
using RustStash.Core.Entities.Auth;

public interface IBaseEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedByPartyId { get; set; }

    public Party? CreatedByParty { get; set; }

    public int? UpdatedByPartyId { get; set; }

    public Party? UpdatedByParty { get; set; }
}

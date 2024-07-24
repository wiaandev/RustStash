namespace RustStash.Core.Entities.Shared;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;
using RustStash.Core.Entities.Auth;

public abstract class BaseEntity : IBaseEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [GraphQLIgnore]
    public int? CreatedByPartyId { get; set; }

    [GraphQLIgnore]
    [ForeignKey("CreatedByPartyId")]
    public Party? CreatedByParty { get; set; } = null!;

    [GraphQLIgnore]
    public int? UpdatedByPartyId { get; set; }

    [GraphQLIgnore]
    [ForeignKey("UpdatedByPartyId")]
    public Party? UpdatedByParty { get; set; }
}

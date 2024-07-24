namespace RustStash.Core.Entities.Auth;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using RustStash.Core.Entities.Shared;

public class Role : IdentityRole<int>, IBaseEntity
{
    public const string Admin = "Admin";

    public static readonly ImmutableList<string> AllRoles = new List<string> { Admin }.ToImmutableList();

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedByPartyId { get; set; }

    [ForeignKey("CreatedByPartyId")]
    public Party? CreatedByParty { get; set; } = null!;

    public int? UpdatedByPartyId { get; set; }

    [ForeignKey("UpdatedByPartyId")]
    public Party? UpdatedByParty { get; set; }
}

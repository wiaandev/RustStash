namespace RustStash.Core.Entities.Auth;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using RustStash.Core.Entities.Shared;

public class User : IdentityUser<int>, IBaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int PartyId { get; set; }

    public Party Party { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedByPartyId { get; set; }

    [ForeignKey("CreatedByPartyId")]
    public Party? CreatedByParty { get; set; } = null!;

    public int? UpdatedByPartyId { get; set; }

    [ForeignKey("UpdatedByPartyId")]
    public Party? UpdatedByParty { get; set; }
}

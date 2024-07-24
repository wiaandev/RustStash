namespace RustStash.Core;

using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RustStash.Core.Entities;
using RustStash.Core.Entities.Auth;
using RustStash.Core.Entities.Shared;
using RustStash.Core.Extensions;

public class AppDbContext : IdentityDbContext<
    User,
    Role,
    int,
    UserClaim,
    UserRole,
    UserLogin,
    RoleClaim,
    UserToken>
{
    public AppDbContext(
        DbContextOptions options)
        : base(options)
    {
        this.SavingChanges += (_, args) =>
        {
            var sessionContext = this.GetService<ISessionContext>();
            var addedEntities = this.ChangeTracker.Entries()
                .Where(e => e is { State: EntityState.Added, Entity: IBaseEntity })
                .ToList();

            addedEntities.ForEach(e =>
            {
                e.Property("CreatedAt").CurrentValue = DateTime.Now;
                e.Property("CreatedByPartyId").CurrentValue = sessionContext.PartyId;
            });

            var editedEntities = this.ChangeTracker.Entries()
                .Where(e => e is { State: EntityState.Modified, Entity: IBaseEntity })
                .ToList();

            editedEntities.ForEach(e =>
            {
                // Fields cannot be updated
                e.Property("CreatedAt").IsModified = false;
                e.Property("CreatedByPartyId").IsModified = false;

                e.Property("UpdatedAt").CurrentValue = DateTime.Now;
                e.Property("UpdatedByPartyId").CurrentValue = sessionContext.PartyId;
            });
        };
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties(typeof(Enum))
            .HaveConversion<string>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema.Core);

        // Midnight South Africa 2023
        var timestamp = new DateTime(2023, 1, 1, 2, 0, 0, DateTimeKind.Utc);

        base.OnModelCreating(builder);
        builder.RemovePluralizingTableNameConvention();
        builder.Entity<Party>().ToTable(nameof(Party), "auth").HasData(new Party
        {
            Id = 1,
            CreatedAt = timestamp,
        });

        builder.Entity<User>().ToTable(nameof(User), Schema.Auth);
        builder.Entity<Role>().ToTable(nameof(Role), Schema.Auth);
        builder.Entity<SystemEntity>().ToTable(nameof(SystemEntity), Schema.Auth).HasData(
            new SystemEntity
            {
                Id = 1,
                Name = "System",
                PartyId = 1,
                CreatedAt = timestamp,
            });

        builder.Entity<RoleClaim>().ToTable(nameof(RoleClaim), Schema.Auth);
        builder.Entity<UserClaim>().ToTable(nameof(UserClaim), Schema.Auth);
        builder.Entity<UserRole>().ToTable(nameof(UserRole), Schema.Auth);
        builder.Entity<UserToken>().ToTable(nameof(UserToken), Schema.Auth);
        builder.Entity<UserLogin>().ToTable(nameof(UserLogin), Schema.Auth);
    }
}

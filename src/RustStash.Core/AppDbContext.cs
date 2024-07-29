using RustStash.Core.Entities.Base;
using RustStash.Core.Entities.Inventory;

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
    public AppDbContext(DbContextOptions options)
        : base(options)
    {
        this.SavingChanges += (_, args) =>
        {
            var sessionContext = this.GetService<ISessionContext>();
            var addedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IBaseEntity && e.State == EntityState.Added)
                .ToList();

            addedEntities.ForEach(e =>
            {
                e.Property("CreatedAt").CurrentValue = DateTime.UtcNow; // Use UTC now
                e.Property("CreatedByPartyId").CurrentValue = sessionContext.PartyId;
            });

            var editedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IBaseEntity && e.State == EntityState.Modified)
                .ToList();

            editedEntities.ForEach(e =>
            {
                e.Property("CreatedAt").IsModified = false;
                e.Property("CreatedByPartyId").IsModified = false;

                e.Property("UpdatedAt").CurrentValue = DateTime.UtcNow; // Use UTC now
                e.Property("UpdatedByPartyId").CurrentValue = sessionContext.PartyId;
            });
        };
    }

    public DbSet<Base> Bases => this.Set<Base>();

    public DbSet<Inventory> Inventories => this.Set<Inventory>();

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties(typeof(Enum))
            .HaveConversion<string>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema.Core);

        // Midnight South Africa 2023, but converted to UTC
        var timestamp = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Correct UTC timestamp

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
        builder.Entity<Base>().ToTable(nameof(Base), Schema.Core);
    }
}

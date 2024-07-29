using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RustStash.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "Base",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    ItemImage = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByPartyId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByPartyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Party_Party_CreatedByPartyId",
                        column: x => x.CreatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Party_Party_UpdatedByPartyId",
                        column: x => x.UpdatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByPartyId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByPartyId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Party_CreatedByPartyId",
                        column: x => x.CreatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Role_Party_UpdatedByPartyId",
                        column: x => x.UpdatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemEntity",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PartyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByPartyId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByPartyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemEntity_Party_CreatedByPartyId",
                        column: x => x.CreatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SystemEntity_Party_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemEntity_Party_UpdatedByPartyId",
                        column: x => x.UpdatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PartyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByPartyId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByPartyId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Party_CreatedByPartyId",
                        column: x => x.CreatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Party_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Party_UpdatedByPartyId",
                        column: x => x.UpdatedByPartyId,
                        principalSchema: "auth",
                        principalTable: "Party",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "auth",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Party",
                columns: new[] { "Id", "CreatedAt", "CreatedByPartyId", "UpdatedAt", "UpdatedByPartyId" },
                values: new object[] { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "SystemEntity",
                columns: new[] { "Id", "CreatedAt", "CreatedByPartyId", "Name", "PartyId", "UpdatedAt", "UpdatedByPartyId" },
                values: new object[] { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "System", 1, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Party_CreatedByPartyId",
                schema: "auth",
                table: "Party",
                column: "CreatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_UpdatedByPartyId",
                schema: "auth",
                table: "Party",
                column: "UpdatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreatedByPartyId",
                schema: "auth",
                table: "Role",
                column: "CreatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdatedByPartyId",
                schema: "auth",
                table: "Role",
                column: "UpdatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "auth",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "auth",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemEntity_CreatedByPartyId",
                schema: "auth",
                table: "SystemEntity",
                column: "CreatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemEntity_PartyId",
                schema: "auth",
                table: "SystemEntity",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemEntity_UpdatedByPartyId",
                schema: "auth",
                table: "SystemEntity",
                column: "UpdatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "auth",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedByPartyId",
                schema: "auth",
                table: "User",
                column: "CreatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PartyId",
                schema: "auth",
                table: "User",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedByPartyId",
                schema: "auth",
                table: "User",
                column: "UpdatedByPartyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "auth",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "auth",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "auth",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "auth",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Base",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "core");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "SystemEntity",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "User",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Party",
                schema: "auth");
        }
    }
}

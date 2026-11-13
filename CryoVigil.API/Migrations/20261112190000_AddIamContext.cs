using System;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261112190000_AddIamContext")]
public class AddIamContext : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "users",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                username = table.Column<string>(type: "longtext", nullable: false),
                password_hash = table.Column<string>(type: "longtext", nullable: false),
                created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            })
            .Annotation("MySQL:Charset", "utf8mb4");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "users");
    }
}

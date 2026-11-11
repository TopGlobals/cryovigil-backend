using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261108171000_AddGeneralSettings")]
public class AddGeneralSettings : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "general_settings",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false),
                region = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                temperature_unit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_general_settings", settings => settings.id));

        migrationBuilder.Sql(
            "INSERT INTO `general_settings` (`id`, `region`, `temperature_unit`) " +
            "VALUES (1, 'Europe/Zurich', 'Celsius') " +
            "ON DUPLICATE KEY UPDATE `id` = `id`;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "general_settings");
    }
}

using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261110190000_SeparateProfilesFromSettings")]
public class SeparateProfilesFromSettings : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "profiles",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false),
                name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                email = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                role = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_profiles", profile => profile.id));

        migrationBuilder.Sql(
            "INSERT INTO `profiles` (`id`, `name`, `email`, `role`) " +
            "VALUES (1, 'Dr. Alex Vance', 'alex.vance@topglobals.com', 'Head Researcher');");
        migrationBuilder.Sql(
            "UPDATE `profiles` AS p " +
            "INNER JOIN `settings` AS s ON s.`id` = p.`id` " +
            "SET p.`name` = s.`profile_name`, p.`email` = s.`profile_email`, p.`role` = s.`profile_role` " +
            "WHERE p.`id` = 1;");

        migrationBuilder.DropColumn(name: "profile_name", table: "settings");
        migrationBuilder.DropColumn(name: "profile_email", table: "settings");
        migrationBuilder.DropColumn(name: "profile_role", table: "settings");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "profile_name",
            table: "settings",
            type: "varchar(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "Dr. Alex Vance");
        migrationBuilder.AddColumn<string>(
            name: "profile_email",
            table: "settings",
            type: "varchar(254)",
            maxLength: 254,
            nullable: false,
            defaultValue: "alex.vance@topglobals.com");
        migrationBuilder.AddColumn<string>(
            name: "profile_role",
            table: "settings",
            type: "varchar(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "Head Researcher");

        migrationBuilder.Sql(
            "UPDATE `settings` AS s " +
            "INNER JOIN `profiles` AS p ON p.`id` = 1 " +
            "SET s.`profile_name` = p.`name`, s.`profile_email` = p.`email`, s.`profile_role` = p.`role` " +
            "WHERE s.`id` = 1;");

        migrationBuilder.DropTable(name: "profiles");
    }
}

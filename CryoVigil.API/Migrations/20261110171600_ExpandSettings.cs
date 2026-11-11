using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261110171600_ExpandSettings")]
public class ExpandSettings : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameTable(
            name: "general_settings",
            newName: "settings");

        migrationBuilder.AddColumn<string>(
            name: "setting_key",
            table: "settings",
            type: "varchar(30)",
            maxLength: 30,
            nullable: false,
            defaultValue: "global");

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

        migrationBuilder.AddColumn<bool>(
            name: "notification_email",
            table: "settings",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: true);

        migrationBuilder.AddColumn<bool>(
            name: "notification_in_app",
            table: "settings",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "notification_daily_summary",
            table: "settings",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "notification_weekly_report",
            table: "settings",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "notification_instant_alerts",
            table: "settings",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: true);

        migrationBuilder.AddColumn<string>(
            name: "warning_threshold",
            table: "settings",
            type: "varchar(10)",
            maxLength: 10,
            nullable: false,
            defaultValue: "2");

        migrationBuilder.AddColumn<string>(
            name: "critical_threshold",
            table: "settings",
            type: "varchar(10)",
            maxLength: 10,
            nullable: false,
            defaultValue: "5");

        migrationBuilder.AddColumn<bool>(
            name: "two_factor_enabled",
            table: "settings",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateIndex(
            name: "ix_settings_key",
            table: "settings",
            column: "setting_key",
            unique: true);

        migrationBuilder.CreateTable(
            name: "settings_sensors",
            columns: table => new
            {
                settings_id = table.Column<int>(type: "int", nullable: false),
                id = table.Column<Guid>(type: "char(36)", nullable: false),
                name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                model = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                serial = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_settings_sensors", sensor => new { sensor.settings_id, sensor.id });
                table.ForeignKey(
                    name: "fk_settings_sensors_settings_settings_id",
                    column: sensor => sensor.settings_id,
                    principalTable: "settings",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "settings_sensors");

        migrationBuilder.DropIndex(
            name: "ix_settings_key",
            table: "settings");

        migrationBuilder.DropColumn(name: "setting_key", table: "settings");
        migrationBuilder.DropColumn(name: "profile_name", table: "settings");
        migrationBuilder.DropColumn(name: "profile_email", table: "settings");
        migrationBuilder.DropColumn(name: "profile_role", table: "settings");
        migrationBuilder.DropColumn(name: "notification_email", table: "settings");
        migrationBuilder.DropColumn(name: "notification_in_app", table: "settings");
        migrationBuilder.DropColumn(name: "notification_daily_summary", table: "settings");
        migrationBuilder.DropColumn(name: "notification_weekly_report", table: "settings");
        migrationBuilder.DropColumn(name: "notification_instant_alerts", table: "settings");
        migrationBuilder.DropColumn(name: "warning_threshold", table: "settings");
        migrationBuilder.DropColumn(name: "critical_threshold", table: "settings");
        migrationBuilder.DropColumn(name: "two_factor_enabled", table: "settings");

        migrationBuilder.RenameTable(
            name: "settings",
            newName: "general_settings");
    }
}

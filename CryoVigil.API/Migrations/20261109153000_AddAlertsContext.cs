using System;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261109153000_AddAlertsContext")]
public class AddAlertsContext : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "alerts",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy.IdentityColumn),
                code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                title = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                message = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                severity = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                source_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                laboratory_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                storage_unit_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                sensor_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                reading_celsius = table.Column<double>(type: "double", nullable: true),
                safe_min_celsius = table.Column<double>(type: "double", nullable: true),
                safe_max_celsius = table.Column<double>(type: "double", nullable: true),
                raised_at = table.Column<DateTime>(type: "datetime", nullable: false),
                acknowledged_at = table.Column<DateTime>(type: "datetime", nullable: true),
                acknowledged_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                resolved_at = table.Column<DateTime>(type: "datetime", nullable: true),
                resolved_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_alerts", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_alerts_code",
            table: "alerts",
            column: "code",
            unique: true);

        migrationBuilder.CreateTable(
            name: "corrective_actions",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy.IdentityColumn),
                alert_id = table.Column<int>(type: "int", nullable: false),
                description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                technician_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                taken_at = table.Column<DateTime>(type: "datetime", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_corrective_actions", x => x.id);
                table.ForeignKey(
                    name: "fk_corrective_actions_alerts_alert_id",
                    column: x => x.alert_id,
                    principalTable: "alerts",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_corrective_actions_alert_id",
            table: "corrective_actions",
            column: "alert_id");

        migrationBuilder.CreateTable(
            name: "alert_readings",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy.IdentityColumn),
                alert_id = table.Column<int>(type: "int", nullable: false),
                taken_at = table.Column<DateTime>(type: "datetime", nullable: false),
                value_celsius = table.Column<double>(type: "double", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_alert_readings", x => x.id);
                table.ForeignKey(
                    name: "fk_alert_readings_alerts_alert_id",
                    column: x => x.alert_id,
                    principalTable: "alerts",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_alert_readings_alert_id",
            table: "alert_readings",
            column: "alert_id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "alert_readings");
        migrationBuilder.DropTable(name: "corrective_actions");
        migrationBuilder.DropTable(name: "alerts");
    }
}

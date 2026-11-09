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
        migrationBuilder.InsertData(
            table: "alerts",
            columns: new[] { "id", "code", "title", "message", "severity", "status", "source_type", "laboratory_name", "storage_unit_name", "sensor_code", "reading_celsius", "safe_min_celsius", "safe_max_celsius", "raised_at", "acknowledged_at", "acknowledged_by", "resolved_at", "resolved_by" },
            values: new object[,] {
                { 1, "ALT-20261005-001", "Temperature above safe range", "Vaccine refrigerator R-02 is reading 9.6 °C, 1.6 °C above its 8.0 °C limit. The compressor has not restarted after the last defrost cycle.", "critical", "raised", "thermal-excursion", "Vaccine Storage Room", "Vaccine refrigerator R-02", "TS-204", 9.6, 2, 8, DateTime.Parse("2026-10-05T17:38:00Z"), null, null, null, null },
                { 2, "ALT-20261005-002", "Ultra-low freezer warming", "Freezer F-01 is at -64.2 °C, above its -70.0 °C limit. It stores plasma and DNA samples from the biobank collection.", "critical", "raised", "thermal-excursion", "Biobank B2", "Ultra-low freezer F-01", "TS-118", -64.2, -86, -70, DateTime.Parse("2026-10-05T17:22:00Z"), null, null, null, null },
                { 3, "ALT-20261005-003", "Temperature below safe range", "Reagent refrigerator R-07 dropped to 0.8 °C. Enzyme kits stored at 2 to 8 °C can lose activity if they freeze.", "critical", "acknowledged", "thermal-excursion", "Clinical Biochemistry Lab", "Reagent refrigerator R-07", "TS-311", 0.8, 2, 8, DateTime.Parse("2026-10-05T16:56:00Z"), DateTime.Parse("2026-10-05T17:03:00Z"), "Lucía Ramos", null, null },
                { 4, "ALT-20261005-004", "Temperature rising toward limit", "Blood bank refrigerator R-03 has risen 0.5 °C per hour and is projected to exceed 6.0 °C in less than 2 hours. Check the cooling system.", "warning", "raised", "preventive-trend", "Clinical Biochemistry Lab", "Blood bank refrigerator R-03", "TS-207", 5.4, 2, 6, DateTime.Parse("2026-10-05T15:54:00Z"), null, null, null, null },
                { 5, "ALT-20261005-005", "Freezer temperature drifting", "Freezer F-04 has warmed steadily for 3 hours and is 1.2 °C from its -15.0 °C limit. The door seal may be worn.", "warning", "acknowledged", "preventive-trend", "Microbiology Lab", "Freezer F-04", "TS-412", -16.2, -25, -15, DateTime.Parse("2026-10-05T14:29:00Z"), DateTime.Parse("2026-10-05T14:54:00Z"), "Diego Salas", null, null },
                { 6, "ALT-20261005-006", "Sensor calibration due", "The calibration certificate of sensor TS-305 expires in 3 days. Schedule a calibration to keep its readings valid for audits.", "info", "raised", "calibration-due", "Clinical Biochemistry Lab", "Sample refrigerator R-09", "TS-305", null, null, null, DateTime.Parse("2026-10-05T13:44:00Z"), null, null, null, null },
                { 7, "ALT-20261005-007", "Temperature back within range", "Vaccine refrigerator R-01 returned to 4.9 °C after the scheduled defrost cycle and has been stable for 30 minutes.", "info", "closed", "thermal-status-normalized", "Vaccine Storage Room", "Vaccine refrigerator R-01", "TS-201", 4.9, 2, 8, DateTime.Parse("2026-10-05T14:44:00Z"), DateTime.Parse("2026-10-05T15:44:00Z"), "Ana Torres", DateTime.Parse("2026-10-05T15:44:00Z"), "Ana Torres" },
                { 8, "ALT-20261005-008", "Cold room temperature excursion", "Cold room CR-1 reached 10.4 °C, above its 8.0 °C limit, while the door was held open during a vaccine delivery.", "critical", "closed", "thermal-excursion", "Vaccine Storage Room", "Cold room CR-1", "TS-220", 10.4, 2, 8, DateTime.Parse("2026-10-05T12:44:00Z"), DateTime.Parse("2026-10-05T12:52:00Z"), "Lucía Ramos", DateTime.Parse("2026-10-05T13:34:00Z"), "Lucía Ramos" },
                { 9, "ALT-20261005-009", "Temperature rising toward limit", "Plasma freezer F-02 was projected to exceed -25.0 °C within 2 hours.", "warning", "closed", "preventive-trend", "Biobank B2", "Plasma freezer F-02", "TS-131", -27.1, -40, -25, DateTime.Parse("2026-10-04T14:44:00Z"), DateTime.Parse("2026-10-04T15:04:00Z"), "Diego Salas", DateTime.Parse("2026-10-04T16:44:00Z"), "Diego Salas" },
                { 10, "ALT-20261005-010", "Sensor calibration due", "The calibration certificate of sensor TS-118 was about to expire.", "info", "closed", "calibration-due", "Biobank B2", "Ultra-low freezer F-01", "TS-118", null, null, null, DateTime.Parse("2026-09-29T17:44:00Z"), DateTime.Parse("2026-09-30T17:44:00Z"), "Ana Torres", DateTime.Parse("2026-09-30T17:44:00Z"), "Ana Torres" },
            });
        migrationBuilder.InsertData(
            table: "corrective_actions",
            columns: new[] { "id", "alert_id", "description", "technician_name", "taken_at" },
            values: new object[,] {
                { 1, 3, "Raised the thermostat set point from 2 °C to 5 °C and moved the enzyme kits to the upper shelf.", "Lucía Ramos", DateTime.Parse("2026-10-05T17:09:00Z") },
                { 2, 8, "Moved vaccine lots 2291 and 2292 to backup refrigerator R-05 and replaced the door gasket of CR-1.", "Lucía Ramos", DateTime.Parse("2026-10-05T13:32:00Z") },
                { 3, 9, "Defrosted the evaporator coil. Temperature stable at -32 °C.", "Diego Salas", DateTime.Parse("2026-10-04T16:39:00Z") },
                { 4, 10, "Sensor recalibrated by the vendor. Certificate valid until next October.", "Ana Torres", DateTime.Parse("2026-09-30T17:44:00Z") },
            });
        migrationBuilder.InsertData(
            table: "alert_readings",
            columns: new[] { "id", "alert_id", "taken_at", "value_celsius" },
            values: new object[,] {
                { 1, 1, DateTime.Parse("2026-10-05T17:14:00Z"), 6.8 },
                { 2, 1, DateTime.Parse("2026-10-05T17:19:00Z"), 7.1 },
                { 3, 1, DateTime.Parse("2026-10-05T17:24:00Z"), 7.5 },
                { 4, 1, DateTime.Parse("2026-10-05T17:29:00Z"), 7.9 },
                { 5, 1, DateTime.Parse("2026-10-05T17:34:00Z"), 8.4 },
                { 6, 1, DateTime.Parse("2026-10-05T17:39:00Z"), 9 },
                { 7, 1, DateTime.Parse("2026-10-05T17:44:00Z"), 9.6 },
                { 8, 2, DateTime.Parse("2026-10-05T17:14:00Z"), -72.4 },
                { 9, 2, DateTime.Parse("2026-10-05T17:19:00Z"), -71.1 },
                { 10, 2, DateTime.Parse("2026-10-05T17:24:00Z"), -69.6 },
                { 11, 2, DateTime.Parse("2026-10-05T17:29:00Z"), -68.3 },
                { 12, 2, DateTime.Parse("2026-10-05T17:34:00Z"), -66.9 },
                { 13, 2, DateTime.Parse("2026-10-05T17:39:00Z"), -65.5 },
                { 14, 2, DateTime.Parse("2026-10-05T17:44:00Z"), -64.2 },
                { 15, 3, DateTime.Parse("2026-10-05T17:14:00Z"), 1.2 },
                { 16, 3, DateTime.Parse("2026-10-05T17:19:00Z"), 0.9 },
                { 17, 3, DateTime.Parse("2026-10-05T17:24:00Z"), 0.8 },
                { 18, 3, DateTime.Parse("2026-10-05T17:29:00Z"), 1 },
                { 19, 3, DateTime.Parse("2026-10-05T17:34:00Z"), 1.3 },
                { 20, 3, DateTime.Parse("2026-10-05T17:39:00Z"), 1.7 },
                { 21, 3, DateTime.Parse("2026-10-05T17:44:00Z"), 2.1 },
                { 22, 4, DateTime.Parse("2026-10-05T17:14:00Z"), 5.15 },
                { 23, 4, DateTime.Parse("2026-10-05T17:19:00Z"), 5.2 },
                { 24, 4, DateTime.Parse("2026-10-05T17:24:00Z"), 5.24 },
                { 25, 4, DateTime.Parse("2026-10-05T17:29:00Z"), 5.28 },
                { 26, 4, DateTime.Parse("2026-10-05T17:34:00Z"), 5.32 },
                { 27, 4, DateTime.Parse("2026-10-05T17:39:00Z"), 5.36 },
                { 28, 4, DateTime.Parse("2026-10-05T17:44:00Z"), 5.4 },
                { 29, 5, DateTime.Parse("2026-10-05T17:14:00Z"), -16.8 },
                { 30, 5, DateTime.Parse("2026-10-05T17:19:00Z"), -16.7 },
                { 31, 5, DateTime.Parse("2026-10-05T17:24:00Z"), -16.6 },
                { 32, 5, DateTime.Parse("2026-10-05T17:29:00Z"), -16.5 },
                { 33, 5, DateTime.Parse("2026-10-05T17:34:00Z"), -16.4 },
                { 34, 5, DateTime.Parse("2026-10-05T17:39:00Z"), -16.3 },
                { 35, 5, DateTime.Parse("2026-10-05T17:44:00Z"), -16.2 },
                { 36, 7, DateTime.Parse("2026-10-05T14:14:00Z"), 7.6 },
                { 37, 7, DateTime.Parse("2026-10-05T14:19:00Z"), 7 },
                { 38, 7, DateTime.Parse("2026-10-05T14:24:00Z"), 6.3 },
                { 39, 7, DateTime.Parse("2026-10-05T14:29:00Z"), 5.8 },
                { 40, 7, DateTime.Parse("2026-10-05T14:34:00Z"), 5.4 },
                { 41, 7, DateTime.Parse("2026-10-05T14:39:00Z"), 5.1 },
                { 42, 7, DateTime.Parse("2026-10-05T14:44:00Z"), 4.9 },
                { 43, 8, DateTime.Parse("2026-10-05T13:04:00Z"), 10.4 },
                { 44, 8, DateTime.Parse("2026-10-05T13:09:00Z"), 9.8 },
                { 45, 8, DateTime.Parse("2026-10-05T13:14:00Z"), 9.1 },
                { 46, 8, DateTime.Parse("2026-10-05T13:19:00Z"), 8.5 },
                { 47, 8, DateTime.Parse("2026-10-05T13:24:00Z"), 7.9 },
                { 48, 8, DateTime.Parse("2026-10-05T13:29:00Z"), 7.2 },
                { 49, 8, DateTime.Parse("2026-10-05T13:34:00Z"), 6.6 },
                { 50, 9, DateTime.Parse("2026-10-04T16:14:00Z"), -27.1 },
                { 51, 9, DateTime.Parse("2026-10-04T16:19:00Z"), -28.3 },
                { 52, 9, DateTime.Parse("2026-10-04T16:24:00Z"), -29.5 },
                { 53, 9, DateTime.Parse("2026-10-04T16:29:00Z"), -30.6 },
                { 54, 9, DateTime.Parse("2026-10-04T16:34:00Z"), -31.4 },
                { 55, 9, DateTime.Parse("2026-10-04T16:39:00Z"), -31.9 },
                { 56, 9, DateTime.Parse("2026-10-04T16:44:00Z"), -32 },
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "alert_readings");
        migrationBuilder.DropTable(name: "corrective_actions");
        migrationBuilder.DropTable(name: "alerts");
    }
}

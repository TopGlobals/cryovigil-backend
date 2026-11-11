using System;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261111120000_AddHistoryContext")]
public class AddHistoryContext : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "history_events",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy.IdentityColumn),
                title_key = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                message_key = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                severity = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                sensor = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                is_resolved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                actions_taken_keys = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                timestamp = table.Column<DateTime>(type: "datetime", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_history_events", x => x.id);
            });

        migrationBuilder.Sql(
            "INSERT INTO `history_events` (`id`, `title_key`, `type`, `message_key`, `severity`, `location`, `sensor`, `is_resolved`, `actions_taken_keys`, `timestamp`) VALUES " +
            "(1, 'history.events.event1.title', 'Resolved', 'history.events.event1.message', 'Success', 'Lab A - Biochemistry Wing', 'TH-04 (Cabinet C3)', 1, '[\"history.events.event1.action1\",\"history.events.event1.action2\",\"history.events.event1.action3\",\"history.events.event1.action4\"]', '2026-11-11 09:47:00'), " +
            "(2, 'history.events.event2.title', 'Alert', 'history.events.event2.message', 'Critical', 'Lab A - Biochemistry Wing', 'TH-04 (Cabinet C3)', 0, '[\"history.events.event2.action1\",\"history.events.event2.action2\"]', '2026-11-11 03:52:00'), " +
            "(3, 'history.events.event3.title', 'Automation', 'history.events.event3.message', 'Info', 'Lab A - Biochemistry Wing', 'TH-04 (Cabinet C3)', 1, '[]', '2026-11-11 03:53:00'), " +
            "(4, 'history.events.event4.title', 'User Action', 'history.events.event4.message', 'Warning', 'Lab B - Molecular Research', 'CO2-01', 1, '[]', '2026-11-11 02:30:00'), " +
            "(5, 'history.events.event5.title', 'Automation', 'history.events.event5.message', 'Info', 'Lab C - Clean Room', 'HVAC-02', 1, '[]', '2026-11-10 22:15:00') " +
            "ON DUPLICATE KEY UPDATE `id` = `id`;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "history_events");
    }
}

using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261108190000_AddLaboratories")]
public class AddLaboratories : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "laboratories",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", 1),
                name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                building = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                floor = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                room = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                temperature_min = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                temperature_max = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                co2_max = table.Column<int>(type: "int", nullable: false),
                gas_sensitivity = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                vibration_max = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                escalation = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                sensor_temperature = table.Column<bool>(type: "tinyint(1)", nullable: false),
                sensor_air_quality = table.Column<bool>(type: "tinyint(1)", nullable: false),
                sensor_ai_detection = table.Column<bool>(type: "tinyint(1)", nullable: false),
                sensor_air_conditioning = table.Column<bool>(type: "tinyint(1)", nullable: false),
                notification_email = table.Column<bool>(type: "tinyint(1)", nullable: false),
                notification_sms = table.Column<bool>(type: "tinyint(1)", nullable: false),
                notification_push = table.Column<bool>(type: "tinyint(1)", nullable: false),
                notification_critical_only = table.Column<bool>(type: "tinyint(1)", nullable: false),
                status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                temperature = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                air_quality = table.Column<int>(type: "int", nullable: true),
                unknown = table.Column<int>(type: "int", nullable: true),
                updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
            },
            constraints: table => table.PrimaryKey("pk_laboratories", laboratory => laboratory.id));

        migrationBuilder.CreateIndex(
            name: "ix_laboratories_code",
            table: "laboratories",
            column: "code",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "laboratories");
    }
}

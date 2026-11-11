using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryoVigil.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261108193000_AddAlertsWithLaboratoryCascade")]
public class AddAlertsWithLaboratoryCascade : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "alerts",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", 1),
                code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                severity = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                laboratory_id = table.Column<int>(type: "int", nullable: false),
                sensor_id = table.Column<int>(type: "int", nullable: false),
                reading = table.Column<float>(type: "float", nullable: false),
                safe_min = table.Column<float>(type: "float", nullable: false),
                safe_max = table.Column<float>(type: "float", nullable: false),
                raised_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_alerts", alert => alert.id);
                table.ForeignKey(
                    name: "fk_alerts_laboratories_laboratory_id",
                    column: alert => alert.laboratory_id,
                    principalTable: "laboratories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_alerts_laboratory_id",
            table: "alerts",
            column: "laboratory_id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "alerts");
    }
}

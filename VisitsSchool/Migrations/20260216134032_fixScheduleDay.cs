using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitSchool.Migrations
{
    /// <inheritdoc />
    public partial class fixScheduleDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_ScheduleDay_ScheduleDayId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Schedule_ScheduleId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "ScheduleDay");

            //migrationBuilder.DropTable(
            //    name: "Schedule");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleName = table.Column<string>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    CostSubscriptions = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Days_ScheduleId",
                table: "Days",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Days_ScheduleDayId",
                table: "Visits",
                column: "ScheduleDayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Schedules_ScheduleId",
                table: "Visits",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Days_ScheduleDayId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Schedules_ScheduleId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostSubscriptions = table.Column<decimal>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleName = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDay_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDay_ScheduleId",
                table: "ScheduleDay",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_ScheduleDay_ScheduleDayId",
                table: "Visits",
                column: "ScheduleDayId",
                principalTable: "ScheduleDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Schedule_ScheduleId",
                table: "Visits",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

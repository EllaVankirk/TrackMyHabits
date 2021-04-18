using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackMyHabit.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitsDates",
                columns: table => new
                {
                    HabitsID = table.Column<int>(nullable: false),
                    AllDatesID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitsDates", x => new { x.AllDatesID, x.HabitsID });
                    table.ForeignKey(
                        name: "FK_HabitsDates_AllDates_AllDatesID",
                        column: x => x.AllDatesID,
                        principalTable: "AllDates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitsDates_Habits_HabitsID",
                        column: x => x.HabitsID,
                        principalTable: "Habits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitsDates_HabitsID",
                table: "HabitsDates",
                column: "HabitsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitsDates");
        }
    }
}

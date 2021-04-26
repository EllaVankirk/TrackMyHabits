using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackMyHabit.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HabitInitial",
                table: "Habits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HabitInitial",
                table: "Habits",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

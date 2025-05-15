using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T217_Capstone_Project_API.Migrations
{
    /// <inheritdoc />
    public partial class updated_env_risks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Culture",
                table: "ProjectRisks");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "PersonalRisks");

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "EnvironmentalRisks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "EnvironmentalRisks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Culture",
                table: "EnvironmentalRisks");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "EnvironmentalRisks");

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "ProjectRisks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "PersonalRisks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

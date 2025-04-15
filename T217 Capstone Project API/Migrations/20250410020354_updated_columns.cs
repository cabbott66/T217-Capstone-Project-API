using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T217_Capstone_Project_API.Migrations
{
    /// <inheritdoc />
    public partial class updated_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlobID",
                table: "StakeholderGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProjectDescription",
                table: "Projects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobID",
                table: "StakeholderGroups");

            migrationBuilder.DropColumn(
                name: "ProjectDescription",
                table: "Projects");
        }
    }
}

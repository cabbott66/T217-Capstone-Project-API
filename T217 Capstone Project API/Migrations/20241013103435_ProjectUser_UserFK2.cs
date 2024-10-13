using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T217_Capstone_Project_API.Migrations
{
    /// <inheritdoc />
    public partial class ProjectUser_UserFK2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserID",
                table: "ProjectUsers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Users_UserID",
                table: "ProjectUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Users_UserID",
                table: "ProjectUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectUsers_UserID",
                table: "ProjectUsers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T217_Capstone_Project_API.Migrations
{
    /// <inheritdoc />
    public partial class projectUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectID",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "PermissionLevel",
                table: "ProjectUsers");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "ProjectUsers",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUsers_ProjectID",
                table: "ProjectUsers",
                newName: "IX_ProjectUsers_ProjectId");

            migrationBuilder.AddColumn<bool>(
                name: "CanEdit",
                table: "ProjectUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRead",
                table: "ProjectUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanWrite",
                table: "ProjectUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "CanEdit",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "CanRead",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "CanWrite",
                table: "ProjectUsers");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "ProjectUsers",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUsers",
                newName: "IX_ProjectUsers_ProjectID");

            migrationBuilder.AddColumn<string>(
                name: "PermissionLevel",
                table: "ProjectUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectID",
                table: "ProjectUsers",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

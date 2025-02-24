using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T217_Capstone_Project_API.Migrations
{
    /// <inheritdoc />
    public partial class initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserEmail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StakeholderGroups",
                columns: table => new
                {
                    StakeholderGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StakeholderGroupName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StakeholderGroups", x => x.StakeholderGroupID);
                    table.ForeignKey(
                        name: "FK_StakeholderGroups_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    ProjectUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    CanRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanWrite = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanEdit = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => x.ProjectUserID);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EnvironmentalRisks",
                columns: table => new
                {
                    EnvironmentalRiskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StakeholderGroupID = table.Column<int>(type: "int", nullable: false),
                    ChangeVolume = table.Column<int>(type: "int", nullable: false),
                    Infrastructure = table.Column<int>(type: "int", nullable: false),
                    Industry = table.Column<int>(type: "int", nullable: false),
                    OfficePolitics = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalRisks", x => x.EnvironmentalRiskID);
                    table.ForeignKey(
                        name: "FK_EnvironmentalRisks_StakeholderGroups_StakeholderGroupID",
                        column: x => x.StakeholderGroupID,
                        principalTable: "StakeholderGroups",
                        principalColumn: "StakeholderGroupID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InterpersonalRisks",
                columns: table => new
                {
                    InterpersonalRiskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StakeholderGroupID = table.Column<int>(type: "int", nullable: false),
                    Support = table.Column<int>(type: "int", nullable: false),
                    SupportiveManagement = table.Column<int>(type: "int", nullable: false),
                    Trust = table.Column<int>(type: "int", nullable: false),
                    Respect = table.Column<int>(type: "int", nullable: false),
                    Communication = table.Column<int>(type: "int", nullable: false),
                    SharingSuccess = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterpersonalRisks", x => x.InterpersonalRiskID);
                    table.ForeignKey(
                        name: "FK_InterpersonalRisks_StakeholderGroups_StakeholderGroupID",
                        column: x => x.StakeholderGroupID,
                        principalTable: "StakeholderGroups",
                        principalColumn: "StakeholderGroupID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PersonalRisks",
                columns: table => new
                {
                    PersonalRiskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StakeholderGroupID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Workload = table.Column<int>(type: "int", nullable: false),
                    Involvement = table.Column<int>(type: "int", nullable: false),
                    EducationTraining = table.Column<int>(type: "int", nullable: false),
                    Kpi = table.Column<int>(type: "int", nullable: false),
                    Impact = table.Column<int>(type: "int", nullable: false),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    ServiceLength = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Interest = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<int>(type: "int", nullable: false),
                    Personalities = table.Column<int>(type: "int", nullable: false),
                    PriorRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRisks", x => x.PersonalRiskID);
                    table.ForeignKey(
                        name: "FK_PersonalRisks_StakeholderGroups_StakeholderGroupID",
                        column: x => x.StakeholderGroupID,
                        principalTable: "StakeholderGroups",
                        principalColumn: "StakeholderGroupID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectRisks",
                columns: table => new
                {
                    ProjectRiskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StakeholderGroupID = table.Column<int>(type: "int", nullable: false),
                    TypeOfChange = table.Column<int>(type: "int", nullable: false),
                    ProjectLength = table.Column<int>(type: "int", nullable: false),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    CulturalAlignment = table.Column<int>(type: "int", nullable: false),
                    Resourcing = table.Column<int>(type: "int", nullable: false),
                    ProjectGoals = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRisks", x => x.ProjectRiskID);
                    table.ForeignKey(
                        name: "FK_ProjectRisks_StakeholderGroups_StakeholderGroupID",
                        column: x => x.StakeholderGroupID,
                        principalTable: "StakeholderGroups",
                        principalColumn: "StakeholderGroupID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stakeholders",
                columns: table => new
                {
                    StakeholderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StakeholderGroupID = table.Column<int>(type: "int", nullable: false),
                    StakeholderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CaFI = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TestData = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stakeholders", x => x.StakeholderID);
                    table.ForeignKey(
                        name: "FK_Stakeholders_StakeholderGroups_StakeholderGroupID",
                        column: x => x.StakeholderGroupID,
                        principalTable: "StakeholderGroups",
                        principalColumn: "StakeholderGroupID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalRisks_StakeholderGroupID",
                table: "EnvironmentalRisks",
                column: "StakeholderGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_InterpersonalRisks_StakeholderGroupID",
                table: "InterpersonalRisks",
                column: "StakeholderGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRisks_StakeholderGroupID",
                table: "PersonalRisks",
                column: "StakeholderGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRisks_StakeholderGroupID",
                table: "ProjectRisks",
                column: "StakeholderGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectID",
                table: "ProjectUsers",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserID",
                table: "ProjectUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_StakeholderGroups_ProjectID",
                table: "StakeholderGroups",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Stakeholders_StakeholderGroupID",
                table: "Stakeholders",
                column: "StakeholderGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentalRisks");

            migrationBuilder.DropTable(
                name: "InterpersonalRisks");

            migrationBuilder.DropTable(
                name: "PersonalRisks");

            migrationBuilder.DropTable(
                name: "ProjectRisks");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "Stakeholders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StakeholderGroups");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}

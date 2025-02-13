using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T217_Capstone_Project_API.Migrations
{
    /// <inheritdoc />
    public partial class initalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: false),
                    UserFirstName = table.Column<string>(type: "TEXT", nullable: false),
                    UserLastName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "StakeholderGroups",
                columns: table => new
                {
                    StakeholderGroupID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StakeholderGroupName = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    ProjectUserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    CanRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanWrite = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanEdit = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalRisks",
                columns: table => new
                {
                    EnvironmentalRiskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StakeholderGroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeVolume = table.Column<int>(type: "INTEGER", nullable: false),
                    Infrastructure = table.Column<int>(type: "INTEGER", nullable: false),
                    Industry = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficePolitics = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "InterpersonalRisks",
                columns: table => new
                {
                    InterpersonalRiskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StakeholderGroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    Support = table.Column<int>(type: "INTEGER", nullable: false),
                    SupportiveManagement = table.Column<int>(type: "INTEGER", nullable: false),
                    Trust = table.Column<int>(type: "INTEGER", nullable: false),
                    Respect = table.Column<int>(type: "INTEGER", nullable: false),
                    Communication = table.Column<int>(type: "INTEGER", nullable: false),
                    SharingSuccess = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "PersonalRisks",
                columns: table => new
                {
                    PersonalRiskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StakeholderGroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<int>(type: "INTEGER", nullable: false),
                    Workload = table.Column<int>(type: "INTEGER", nullable: false),
                    Involvement = table.Column<int>(type: "INTEGER", nullable: false),
                    EducationTraining = table.Column<int>(type: "INTEGER", nullable: false),
                    Kpi = table.Column<int>(type: "INTEGER", nullable: false),
                    Impact = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleType = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceLength = table.Column<int>(type: "INTEGER", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    Interest = table.Column<int>(type: "INTEGER", nullable: false),
                    History = table.Column<int>(type: "INTEGER", nullable: false),
                    Personalities = table.Column<int>(type: "INTEGER", nullable: false),
                    PriorRole = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "ProjectRisks",
                columns: table => new
                {
                    ProjectRiskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StakeholderGroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    TypeOfChange = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectLength = table.Column<int>(type: "INTEGER", nullable: false),
                    Culture = table.Column<int>(type: "INTEGER", nullable: false),
                    CulturalAlignment = table.Column<int>(type: "INTEGER", nullable: false),
                    Resourcing = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectGoals = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Stakeholders",
                columns: table => new
                {
                    StakeholderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StakeholderGroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    StakeholderName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CaFI = table.Column<string>(type: "TEXT", nullable: false),
                    TestData = table.Column<string>(type: "TEXT", nullable: false)
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
                });

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

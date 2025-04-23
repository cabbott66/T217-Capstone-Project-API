using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Models.Risks;

namespace T217_Capstone_Project_API
{
    public class StakeholderRisksContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        // Project DbSets
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }

        // Stakeholder DbSets
        public DbSet<Stakeholder> Stakeholders { get; set; }

        // Risks DbSets
        public DbSet<EnvironmentalRisk> EnvironmentalRisks { get; set; }
        public DbSet<InterpersonalRisk> InterpersonalRisks { get; set; }
        public DbSet<PersonalRisk> PersonalRisks { get; set; }
        public DbSet<ProjectRisk> ProjectRisks { get; set; }

        public StakeholderRisksContext(DbContextOptions options) : base (options) { }

        // Dalek (very important, do not delete!)
        //        (\. -- ./)
        //        O-0)))--|     \
        //          |____________|
        //           -|--|--|--|-
        //           _T ~_T~_T~_T_
        //          |____________|
        //          |_o_|____|_o_|
        //       .-~/  :  |   %  \
        //.-..-~   /  :   |  %:   \
        //`-'     /   :   | %  :   \
        //       /   :    |#   :    \
        //      /    :    |     :    \
        //     /    :     |     :     \
        // . -/     :     |      :     \- .
        //|\  ~-.  :      |      :   .-~  /|
        //\ ~-.   ~ - .  _|_. - ~   .-~ /
        //  ~-.  ~ -  . _ _ _.  - ~  .-~
        //       ~ -  . _ _ _.  - ~
    }
}

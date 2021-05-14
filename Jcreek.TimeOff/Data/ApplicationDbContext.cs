using Jcreek.TimeOff.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Jcreek.TimeOff.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedCompany(ref builder);
            SeedTeams(ref builder);
            SeedUsersAndManagers(ref builder);
            SeedTeamManagers(ref builder);
            SeedHolidays(ref builder);
            SeedHolidayAllocations(ref builder);

            base.OnModelCreating(builder);
        }

        protected void SeedCompany(ref ModelBuilder builder)
        {
            var company = new Company
            {
                CompanyId = 1,
                CompanyName = "The Test Co",
                MaximumYearlyUnusedRolloverDays = 5,
            };

            // Add to the database
            List<Company> companies = new List<Company>()
            {
                company,
            };

            builder.Entity<Company>().HasData(companies);
        }

        protected void SeedTeams(ref ModelBuilder builder)
        {
            var team1 = new Team
            {
                TeamId = 1,
                CompanyId = 1,
                TeamName = "Test Team 1",
            };

            var team2 = new Team
            {
                TeamId = 2,
                CompanyId = 1,
                TeamName = "Test Team 2",
            };

            var team3 = new Team
            {
                TeamId = 3,
                CompanyId = 1,
                TeamName = "Test Team 3",
            };

            // Add to the database
            List<Team> teams = new List<Team>()
            {
                team1, team2, team3,
            };

            builder.Entity<Team>().HasData(teams);

            builder.Entity<Team>()
                .HasOne<Company>(t => t.Company)
                .WithMany(c => c.Teams)
                .HasForeignKey(g => g.CompanyId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade); // Setup delete behaviour here
        }

        protected void SeedUsersAndManagers(ref ModelBuilder builder)
        {
            var user1 = new TeamMember
            {
                TeamMemberId = 1,
                TeamId = 1,
                Name = "Test Person A",
            };

            var user2 = new TeamMember
            {
                TeamMemberId = 2,
                TeamId = 1,
                Name = "Test Person B",
            };

            var user3 = new TeamMember
            {
                TeamMemberId = 3,
                TeamId = 2,
                Name = "Test Person C",
            };

            var user4 = new TeamMember
            {
                TeamMemberId = 4,
                TeamId = 2,
                Name = "Test Person D",
            };

            var manager1 = new TeamMember
            {
                TeamMemberId = 5,
                TeamId = 2,
                Name = "Test Manager A",
            };

            var manager2 = new TeamMember
            {
                TeamMemberId = 6,
                TeamId = 3,
                Name = "Test Manager B",
            };

            // Add to the database
            List<TeamMember> users = new List<TeamMember>()
            {
                user1, user2, user3, user4, manager1, manager2,
            };

            builder.Entity<TeamMember>().HasData(users);

            builder.Entity<TeamMember>()
                .HasOne<Team>(u => u.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(g => g.TeamId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.SetNull); // Setup delete behaviour here
        }

        protected void SeedTeamManagers(ref ModelBuilder builder)
        {
            // Create an extra table in the database to represent the possible many-to-many relationship between Teams and Managers
            builder.Entity<TeamMember>()
                .HasMany(left => left.ManagedTeams)
                .WithMany(right => right.TeamManagers)
                .UsingEntity(
                    "TeamManagers",
                    typeof(Dictionary<string, object>),
                    right => right.HasOne(typeof(Team)).WithMany().HasForeignKey("TeamId"),
                    left => left.HasOne(typeof(TeamMember)).WithMany().HasForeignKey("TeamMemberId"),
                    join => join.ToTable("TeamManagers"));

            // Allocate many to many relationships
            builder.Entity("TeamManagers").HasData(
              new { TeamId = 1, TeamMemberId = 5 },
              new { TeamId = 2, TeamMemberId = 6 },
              new { TeamId = 3, TeamMemberId = 5 });
        }

        protected void SeedHolidays(ref ModelBuilder builder)
        {
            var holiday1 = new Holiday
            {
                HolidayId = 1,
                UserId = 1,
                StartDate = new System.DateTime(2021, 01, 01, 0, 0, 0),
                EndDate = new System.DateTime(2021, 01, 03, 0, 0, 0),
                IsApproved = true,
            };

            var holiday2 = new Holiday
            {
                HolidayId = 2,
                UserId = 1,
                StartDate = new System.DateTime(2021, 02, 01, 12, 0, 0),
                EndDate = new System.DateTime(2021, 02, 03, 0, 0, 0),
                IsApproved = true,
            };

            // Add to the database
            List<Holiday> holidays = new List<Holiday>()
            {
                holiday1, holiday2,
            };

            builder.Entity<Holiday>().HasData(holidays);

            builder.Entity<Holiday>()
                .HasOne<TeamMember>(h => h.User)
                .WithMany(u => u.Holidays)
                .HasForeignKey(g => g.UserId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade); // Setup delete behaviour here
        }

        protected void SeedHolidayAllocations(ref ModelBuilder builder)
        {
            var userHolidayAllocation1 = new AnnualHolidayAllocation()
            {
                AnnualHolidayAllocationId = 1,
                UserId = 1,
                Year = "2021",
                Days = 25,
            };

            var userHolidayAllocation2 = new AnnualHolidayAllocation()
            {
                AnnualHolidayAllocationId = 2,
                UserId = 2,
                Year = "2021",
                Days = 25,
            };

            var userHolidayAllocation3 = new AnnualHolidayAllocation()
            {
                AnnualHolidayAllocationId = 3,
                UserId = 3,
                Year = "2021",
                Days = 25,
            };

            var userHolidayAllocation4 = new AnnualHolidayAllocation()
            {
                AnnualHolidayAllocationId = 4,
                UserId = 4,
                Year = "2021",
                Days = 25,
            };

            var managerHolidayAllocation1 = new AnnualHolidayAllocation()
            {
                AnnualHolidayAllocationId = 5,
                UserId = 5,
                Year = "2021",
                Days = 30,
            };

            var managerHolidayAllocation2 = new AnnualHolidayAllocation()
            {
                AnnualHolidayAllocationId = 6,
                UserId = 6,
                Year = "2021",
                Days = 30,
            };

            // Add to the database
            List<AnnualHolidayAllocation> annualHolidayAllocations = new List<AnnualHolidayAllocation>()
            {
                userHolidayAllocation1, userHolidayAllocation2, userHolidayAllocation3, userHolidayAllocation4, managerHolidayAllocation1, managerHolidayAllocation2,
            };

            builder.Entity<AnnualHolidayAllocation>().HasData(annualHolidayAllocations);

            builder.Entity<AnnualHolidayAllocation>()
                .HasOne<TeamMember>(h => h.User)
                .WithMany(u => u.AnnualHolidayAllocations)
                .HasForeignKey(g => g.UserId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade); // Setup delete behaviour here
        }
    }
}

using ApiGreenway.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<BadgeLevel> BadgeLevels { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyRepresentative> CompanyRepresentatives { get; set; }
        public DbSet<ImprovementMeasurement> ImprovementMeasurements { get; set; }
        public DbSet<MeasurementProcessStep> MeasurementProcessSteps { get; set; }
        public DbSet<Measurement> Measurements  { get; set; }
        public DbSet<MeasurementType> MeasurementTypes { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessBadge> ProcessBadges { get; set; }
        public DbSet<ProcessResource> ProcessResources { get; set; }
        public DbSet<ProcessStep> ProcessSteps { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<SustainableGoal> SustainableGoals { get; set; }
        public DbSet<SustainableImprovementActions> SustainableImprovementActions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
    }
}

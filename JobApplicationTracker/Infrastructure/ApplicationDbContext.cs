using Domain.JobApplications;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure {
    public class ApplicationDbContext : DbContext {

        public DbSet<JobApplication> JobApplications { get; set; }

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationConfiguration).Assembly);
        }
    }
}

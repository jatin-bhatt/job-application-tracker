using Domain.JobApplications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations {
    public class ApplicationConfiguration : IEntityTypeConfiguration<JobApplication> {
        public void Configure(EntityTypeBuilder<JobApplication> builder) {
            builder.HasKey(b => b.Id);

           
        }
    }
}

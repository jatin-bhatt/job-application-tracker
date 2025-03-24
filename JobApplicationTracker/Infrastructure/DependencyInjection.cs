using Domain.JobApplications;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure {
    public static class DependencyInjection {

        public static IServiceCollection AddInfrastructure(this IServiceCollection service) {

            service.AddDbContext<ApplicationDbContext>(o => o.UseSqlite($"Data Source=JobApplicationTracker.db"));
            service.AddScoped<IJobApplicationRepository, JobApplicationRepository>();

            return service;
        }
    }
}

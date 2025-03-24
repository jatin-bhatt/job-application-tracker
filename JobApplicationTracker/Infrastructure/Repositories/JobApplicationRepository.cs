using Domain.JobApplications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories {
    public class JobApplicationRepository : IJobApplicationRepository {
        private readonly ApplicationDbContext _context;

        public JobApplicationRepository(ApplicationDbContext dbContext) {
            _context = dbContext;
        }

        public async Task<IList<JobApplication>> GetAsync(CancellationToken cancellationToken) {
            return await _context.JobApplications.ToListAsync(cancellationToken);
        }

        public async Task<JobApplication> GetAsync(int id, CancellationToken cancellationToken) {
            return await _context.JobApplications?.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task AddAsync(JobApplication application, CancellationToken cancellationToken) {
            await _context.JobApplications.AddAsync(application, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobApplication application, CancellationToken cancellationToken) {
            _context.Entry(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

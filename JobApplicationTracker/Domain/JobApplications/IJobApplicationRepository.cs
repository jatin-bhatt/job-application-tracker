namespace Domain.JobApplications {
    public interface IJobApplicationRepository {
        Task<IList<JobApplication>> GetAsync(CancellationToken cancellationToken);
        Task<JobApplication> GetAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(JobApplication product, CancellationToken cancellationToken);
        Task UpdateAsync(JobApplication product, CancellationToken cancellationToken);

    }
}

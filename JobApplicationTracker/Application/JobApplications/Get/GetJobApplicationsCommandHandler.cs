using Domain.JobApplications;
using MediatR;

namespace Application.JobApplications.Get {
    public class GetJobApplicationsCommandHandler : IRequestHandler<GetJobApplicationsCommand, IList<JobApplication>> {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public GetJobApplicationsCommandHandler(IJobApplicationRepository jobApplicationRepository) {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<IList<JobApplication>> Handle(GetJobApplicationsCommand request, CancellationToken cancellationToken) {
            return await _jobApplicationRepository.GetAsync(cancellationToken);
        }
    }
}

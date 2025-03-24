using Domain.JobApplications;
using MediatR;

namespace Application.JobApplications.Get {
    public class GetJobApplicationByIdCommandHandler : IRequestHandler<GetJobApplicationByIdCommand, JobApplication> {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public GetJobApplicationByIdCommandHandler(IJobApplicationRepository jobApplicationRepository) {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<JobApplication> Handle(GetJobApplicationByIdCommand request, CancellationToken cancellationToken) {
            return await _jobApplicationRepository.GetAsync(request.id, cancellationToken);
        }
    }
}

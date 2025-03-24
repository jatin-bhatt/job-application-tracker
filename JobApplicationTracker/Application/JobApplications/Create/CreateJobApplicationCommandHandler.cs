using Domain.JobApplications;
using MediatR;

namespace Application.JobApplications.Create {
    public class CreateJobApplicationCommandHandler : IRequestHandler<CreateJobApplicationCommand> {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public CreateJobApplicationCommandHandler(IJobApplicationRepository jobApplicationRepository) {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken) {

            if (!Enum.TryParse(request.Status.ToString(), true, out Status status)) {
                throw new Exception("Job Application Status not acceptable");
            }

            var jobApplication = new JobApplication {
                CompanyName = request.CompanyName,
                Position =  request.Position, 
                Status = status, 
                ApplicationDate = request.ApplicationDate 
            };

            await _jobApplicationRepository.AddAsync(jobApplication, cancellationToken);

        }
    }
}

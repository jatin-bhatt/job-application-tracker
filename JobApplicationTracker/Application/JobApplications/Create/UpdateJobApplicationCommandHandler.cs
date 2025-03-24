using Domain.JobApplications;
using MediatR;

namespace Application.JobApplications.Create {
    public class UpdateJobApplicationCommandHandler : IRequestHandler<UpdateJobApplicationCommand> {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public UpdateJobApplicationCommandHandler(IJobApplicationRepository jobApplicationRepository) {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task Handle(UpdateJobApplicationCommand request, CancellationToken cancellationToken) {

            if (!Enum.TryParse(request.Status.ToString(), true, out Status status)) {
                throw new Exception("Job Application Status not acceptable");
            }

            var entity = await _jobApplicationRepository.GetAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new ArgumentException($"Entity  {request.Id} not found.");

            entity.CompanyName = request.CompanyName;
            entity.Position = request.Position;
            entity.Status = status;
            entity.ApplicationDate = request.ApplicationDate;

            await _jobApplicationRepository.UpdateAsync(entity, cancellationToken);

        }
    }
}

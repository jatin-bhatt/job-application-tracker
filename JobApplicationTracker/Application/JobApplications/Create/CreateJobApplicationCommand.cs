using MediatR;

namespace Application.JobApplications.Create {
    public record CreateJobApplicationCommand(string CompanyName, string Position, string Status, DateTime ApplicationDate) : IRequest;
}

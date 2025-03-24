using MediatR;

namespace Application.JobApplications.Create {
    public record UpdateJobApplicationCommand(int Id, string CompanyName, string Position, string Status, DateTime ApplicationDate) : IRequest;
}

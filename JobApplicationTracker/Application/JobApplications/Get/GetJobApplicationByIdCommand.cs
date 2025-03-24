using Domain.JobApplications;
using MediatR;

namespace Application.JobApplications.Get {
    public record GetJobApplicationByIdCommand(int id) : IRequest<JobApplication>;
}

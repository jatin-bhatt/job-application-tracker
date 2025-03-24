using Domain.JobApplications;
using MediatR;

namespace Application.JobApplications.Get {
    public record GetJobApplicationsCommand () : IRequest<IList<JobApplication>>;
}

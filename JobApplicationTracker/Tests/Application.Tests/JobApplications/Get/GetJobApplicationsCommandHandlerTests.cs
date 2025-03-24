using Application.JobApplications.Get;
using Domain.JobApplications;
using Moq;

namespace Application.Tests.JobApplications.Get {
    public class GetJobApplicationsCommandHandlerTests {
        private readonly Mock<IJobApplicationRepository> _jobApplicationRepositoryMock;
        private readonly GetJobApplicationsCommandHandler _handler;

        public GetJobApplicationsCommandHandlerTests() {
            _jobApplicationRepositoryMock = new Mock<IJobApplicationRepository>();
            _handler = new GetJobApplicationsCommandHandler(_jobApplicationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsListOfJobApplications() {
            // Arrange
            var expectedJobApplications = new List<JobApplication>
            {
                new JobApplication { Id = 1, CompanyName = "Company A", Position = "Developer", Status = Status.Submitted, ApplicationDate = DateTime.UtcNow },
                new JobApplication { Id = 2, CompanyName = "Company B", Position = "Tester", Status = Status.InterviewCompleted, ApplicationDate = DateTime.UtcNow }
            };

            _jobApplicationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
                                         .ReturnsAsync(expectedJobApplications);

            // Act
            var result = await _handler.Handle(new GetJobApplicationsCommand(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoJobApplicationsExist() {
            // Arrange
            _jobApplicationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
                                         .ReturnsAsync(new List<JobApplication>());

            // Act
            var result = await _handler.Handle(new GetJobApplicationsCommand(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}

using Application.JobApplications.Get;
using Domain.JobApplications;
using Moq;

namespace Application.Tests.JobApplications.Get {
    public class GetJobApplicationByIdCommandHandlerTests {
        private readonly Mock<IJobApplicationRepository> _jobApplicationRepositoryMock;
        private readonly GetJobApplicationByIdCommandHandler _handler;

        public GetJobApplicationByIdCommandHandlerTests() {
            _jobApplicationRepositoryMock = new Mock<IJobApplicationRepository>();
            _handler = new GetJobApplicationByIdCommandHandler(_jobApplicationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsJobApplication() {
            // Arrange
            var command = new GetJobApplicationByIdCommand(1);
            var expectedJobApplication = new JobApplication { Id = 1, CompanyName = "Company A", Position = "Developer", Status = Status.Submitted, ApplicationDate = DateTime.UtcNow };

            _jobApplicationRepositoryMock.Setup(repo => repo.GetAsync(1, It.IsAny<CancellationToken>()))
                                         .ReturnsAsync(expectedJobApplication);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedJobApplication.Id, result.Id);
        }

        [Fact]
        public async Task Handle_NonExistentJobApplication_ReturnsNull() {
            // Arrange
            var command = new GetJobApplicationByIdCommand(1);
            _jobApplicationRepositoryMock.Setup(repo => repo.GetAsync(1, It.IsAny<CancellationToken>()))
                                         .ReturnsAsync((JobApplication)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}

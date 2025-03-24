using Application.JobApplications.Create;
using Domain.JobApplications;
using Moq;

namespace Application.Tests.JobApplications.Create {
    public class CreateJobApplicationCommandHandlerTests {
        private readonly Mock<IJobApplicationRepository> _jobApplicationRepositoryMock;
        private readonly CreateJobApplicationCommandHandler _handler;

        public CreateJobApplicationCommandHandlerTests() {
            _jobApplicationRepositoryMock = new Mock<IJobApplicationRepository>();
            _handler = new CreateJobApplicationCommandHandler(_jobApplicationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_CallsRepositoryAddAsync() {
            // Arrange
            var command = new CreateJobApplicationCommand("Company A", "Developer", "Hired", DateTime.UtcNow);
            _jobApplicationRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<JobApplication>(), It.IsAny<CancellationToken>()))
                                         .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _jobApplicationRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<JobApplication>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidStatus_ThrowsException() {
            // Arrange
            var command = new CreateJobApplicationCommand("Company B", "Engineer", "InvalidStatus", DateTime.UtcNow);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

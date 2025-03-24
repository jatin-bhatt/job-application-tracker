using Application.JobApplications.Create;
using Domain.JobApplications;
using Moq;

namespace Application.Tests.JobApplications.Create {
    public class UpdateJobApplicationCommandHandlerTests {
        private readonly Mock<IJobApplicationRepository> _jobApplicationRepositoryMock;
        private readonly UpdateJobApplicationCommandHandler _handler;

        public UpdateJobApplicationCommandHandlerTests() {
            _jobApplicationRepositoryMock = new Mock<IJobApplicationRepository>();
            _handler = new UpdateJobApplicationCommandHandler(_jobApplicationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_CallsRepositoryUpdateAsync() {
            // Arrange
            var command = new UpdateJobApplicationCommand(1, "Company A", "Developer", "Submitted", DateTime.UtcNow);
            var existingJobApplication = new JobApplication { Id = 1, CompanyName = "Old Company", Position = "Old Position", Status = Status.Submitted, ApplicationDate = DateTime.UtcNow };

            _jobApplicationRepositoryMock.Setup(repo => repo.GetAsync(1, It.IsAny<CancellationToken>()))
                                         .ReturnsAsync(existingJobApplication);
            _jobApplicationRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<JobApplication>(), It.IsAny<CancellationToken>()))
                                         .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _jobApplicationRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<JobApplication>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidStatus_ThrowsException() {
            // Arrange
            var command = new UpdateJobApplicationCommand(1, "Company B", "Engineer", "InvalidStatus", DateTime.UtcNow);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NonExistentJobApplication_ThrowsArgumentException() {
            // Arrange
            var command = new UpdateJobApplicationCommand(1, "Company C", "Manager", "Hired", DateTime.UtcNow);
            _jobApplicationRepositoryMock.Setup(repo => repo.GetAsync(1, It.IsAny<CancellationToken>()))
                                         .ReturnsAsync((JobApplication)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

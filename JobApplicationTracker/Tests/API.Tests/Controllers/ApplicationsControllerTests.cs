using API.Controllers;
using API.DTOs;
using Application.JobApplications.Create;
using Application.JobApplications.Get;
using Domain.JobApplications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests.Controllers {
    public class ApplicationsControllerTests {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ApplicationsController _controller;

        public ApplicationsControllerTests() {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ApplicationsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithJobApplications() {
            // Arrange
            var jobApplications = new List<JobApplication>
            {
                new JobApplication { Id = 1, CompanyName = "Company A", Position = "Developer", Status = Status.Submitted, ApplicationDate = DateTime.UtcNow }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetJobApplicationsCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(jobApplications);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<JobApplicationDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_WhenApplicationDoesNotExist() {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetJobApplicationByIdCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((JobApplication)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Post_CreatesJobApplication_ReturnsOk() {
            // Arrange
            var createDto = new JobApplicationCreateDto { CompanyName = "Company B", Position = "Engineer", Status = "Submitted", ApplicationDate = DateTime.UtcNow };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateJobApplicationCommand>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(createDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenApplicationDoesNotExist() {
            // Arrange
            var updateDto = new JobApplicationUpdateDto { Id = 1, CompanyName = "Company C", Position = "Manager", Status = "Hired", ApplicationDate = DateTime.UtcNow };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetJobApplicationByIdCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((JobApplication)null);

            // Act
            var result = await _controller.Put(updateDto, 1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}

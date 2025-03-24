using API.DTOs;
using Application.JobApplications.Create;
using Application.JobApplications.Get;
using Domain.JobApplications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api")]
    [ApiController]
    public class ApplicationsController : ControllerBase {

        private readonly IMediator _mediator;

        public ApplicationsController(IMediator mediator) {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Get All Job Applications
        /// </summary>
        /// <returns></returns>
        [HttpGet("applications")]
        [ProducesResponseType<JobApplicationDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get() {
            IList<JobApplication> jobApplications = await _mediator.Send(new GetJobApplicationsCommand());
            var result = jobApplications.Select(j => new JobApplicationDto {
                Id = j.Id,
                ApplicationDate = j.ApplicationDate,
                CompanyName = j.CompanyName,
                Position = j.Position,
                Status = j.Status.ToString(),
            }).ToList();
            return Ok(result);
        }
        
        /// <summary>
        /// Get Job Application for specified id.
        /// </summary>
        /// <param name="id">Job Application Unique Identifier</param>
        /// <returns></returns>
        [HttpGet("application/{id}")]
        [ProducesResponseType<JobApplicationDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id) {
            var jobApplication = await _mediator.Send(new GetJobApplicationByIdCommand(id));
            if (jobApplication == null) {
                return NotFound($"Job Application {id} Not Found");
            }
            var result = new JobApplicationDto {
                Id = jobApplication.Id,
                ApplicationDate = jobApplication.ApplicationDate,
                CompanyName = jobApplication.CompanyName,
                Position = jobApplication.Position,
                Status = jobApplication.Status.ToString(),
            };
            return Ok(result);
        }
        /// <summary>
        /// Create Job Application
        /// </summary>
        /// <param name="jobApplicationCreateDto"></param>
        /// <returns></returns>
        [HttpPost("application")]
        [ProducesResponseType<JobApplicationCreateDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] JobApplicationCreateDto jobApplicationCreateDto) {
            await _mediator.Send(new CreateJobApplicationCommand (
                jobApplicationCreateDto.CompanyName,
                jobApplicationCreateDto.Position,
                jobApplicationCreateDto.Status,
                jobApplicationCreateDto.ApplicationDate));

            return Ok();
        }

        /// <summary>
        /// Update an existing Job Application
        /// </summary>
        /// <param name="jobApplicationUpdateDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("application/{id}")]
        [ProducesResponseType<JobApplicationUpdateDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] JobApplicationUpdateDto jobApplicationUpdateDto, int id) {
            if (jobApplicationUpdateDto == null || id != jobApplicationUpdateDto.Id) {
                return BadRequest("Invalid request data.");
            }
            var jobApplication = await _mediator.Send(new GetJobApplicationByIdCommand(id));
            if (jobApplication == null) {
                return NotFound($"Job Application {id} Not Found");
            }
            await _mediator.Send(new UpdateJobApplicationCommand(
                jobApplicationUpdateDto.Id,
                jobApplicationUpdateDto.CompanyName,
                jobApplicationUpdateDto.Position,
                jobApplicationUpdateDto.Status,
                jobApplicationUpdateDto.ApplicationDate));

            return Ok();
        }

    }
}

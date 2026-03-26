using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Application.DTOs.Job;
using RecruitmentSystem.Application.Interfaces;
using System.Security.Claims;

namespace RecruitmentSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetJobs()
    {
        return Ok(await _jobService.GetAllJobsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob(int id)
    {
        var job = await _jobService.GetJobByIdAsync(id);
        if (job == null) return NotFound();
        return Ok(job);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Recruiter")]
    public async Task<IActionResult> CreateJob(CreateJobRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var job = await _jobService.CreateJobAsync(request, userId);
        return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Recruiter")]
    public async Task<IActionResult> UpdateJob(int id, CreateJobRequest request)
    {
        await _jobService.UpdateJobAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        await _jobService.DeleteJobAsync(id);
        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Application.DTOs.JobApplication;
using RecruitmentSystem.Application.Interfaces;
using RecruitmentSystem.Domain.Enums;
using System.Security.Claims;

namespace RecruitmentSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("my-applications")]
    [Authorize(Roles = "Candidate")]
    public async Task<IActionResult> GetMyApplications()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _applicationService.GetApplicationsByCandidateAsync(userId));
    }

    [HttpGet("job/{jobId}")]
    [Authorize(Roles = "Admin,Recruiter")]
    public async Task<IActionResult> GetJobApplications(int jobId)
    {
        return Ok(await _applicationService.GetApplicationsByJobAsync(jobId));
    }

    [HttpPost("apply/{jobId}")]
    [Authorize(Roles = "Candidate")]
    public async Task<IActionResult> Apply(int jobId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _applicationService.ApplyAsync(jobId, userId);
        if (!result) return BadRequest("Already applied or candidate record not found");
        return Ok("Application submitted");
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin,Recruiter")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateApplicationStatusRequest request)
    {
        await _applicationService.UpdateStatusAsync(id, request.Status);
        return NoContent();
    }
}

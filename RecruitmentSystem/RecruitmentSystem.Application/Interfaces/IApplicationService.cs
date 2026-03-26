using RecruitmentSystem.Application.DTOs.JobApplication;
using RecruitmentSystem.Domain.Enums;

namespace RecruitmentSystem.Application.Interfaces;

public interface IApplicationService
{
    Task<IEnumerable<ApplicationDto>> GetApplicationsByCandidateAsync(int userId);
    Task<IEnumerable<ApplicationDto>> GetApplicationsByJobAsync(int jobId);
    Task<bool> ApplyAsync(int jobId, int userId);
    Task UpdateStatusAsync(int applicationId, ApplicationStatus status);
}

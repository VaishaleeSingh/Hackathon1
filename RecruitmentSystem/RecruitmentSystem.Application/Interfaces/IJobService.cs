using RecruitmentSystem.Application.DTOs.Job;

namespace RecruitmentSystem.Application.Interfaces;

public interface IJobService
{
    Task<IEnumerable<JobDto>> GetAllJobsAsync();
    Task<JobDto?> GetJobByIdAsync(int id);
    Task<JobDto> CreateJobAsync(CreateJobRequest request, int userId);
    Task UpdateJobAsync(int id, CreateJobRequest request);
    Task DeleteJobAsync(int id);
}

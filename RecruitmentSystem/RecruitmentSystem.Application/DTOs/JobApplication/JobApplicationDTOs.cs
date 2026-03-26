using RecruitmentSystem.Domain.Enums;

namespace RecruitmentSystem.Application.DTOs.JobApplication;

public class ApplicationDto
{
    public int Id { get; set; }
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    public int JobId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public int CandidateId { get; set; }
    public string CandidateName { get; set; } = string.Empty;
}

public class CreateApplicationRequest
{
    public int JobId { get; set; }
}

public class UpdateApplicationStatusRequest
{
    public ApplicationStatus Status { get; set; }
}

using RecruitmentSystem.Domain.Enums;

namespace RecruitmentSystem.Domain.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    
    public int JobId { get; set; }
    public Job Job { get; set; } = null!;
    
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;
}

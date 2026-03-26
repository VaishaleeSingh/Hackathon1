namespace RecruitmentSystem.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    
    public Candidate? CandidateProfile { get; set; }
    public ICollection<Job> CreatedJobs { get; set; } = new List<Job>();
}

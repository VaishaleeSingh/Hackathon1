namespace RecruitmentSystem.Domain.Entities;

public class Candidate
{
    public int Id { get; set; }
    public string ResumeUrl { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Education { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
}

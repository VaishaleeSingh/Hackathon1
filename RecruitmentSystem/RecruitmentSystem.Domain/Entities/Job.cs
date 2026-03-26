namespace RecruitmentSystem.Domain.Entities;

public class Job
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public decimal SalaryRange { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    
    public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
}

using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Domain.Entities;

namespace RecruitmentSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Job> Jobs { get; set; } = null!;
    public DbSet<Candidate> Candidates { get; set; } = null!;
    public DbSet<JobApplication> Applications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
            
        modelBuilder.Entity<Candidate>()
            .HasOne(c => c.User)
            .WithOne(u => u.CandidateProfile)
            .HasForeignKey<Candidate>(c => c.UserId);
            
        modelBuilder.Entity<Job>()
            .HasOne(j => j.CreatedBy)
            .WithMany(u => u.CreatedJobs)
            .HasForeignKey(j => j.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
            
        modelBuilder.Entity<JobApplication>()
            .HasOne(a => a.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<JobApplication>()
            .HasOne(a => a.Candidate)
            .WithMany(c => c.Applications)
            .HasForeignKey(a => a.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using System;
using RecruitmentSystem.Domain.Entities;

namespace RecruitmentSystem.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<Job> Jobs { get; }
    IRepository<Candidate> Candidates { get; }
    IRepository<JobApplication> Applications { get; }
    
    Task<int> CompleteAsync();
}

using RecruitmentSystem.Application.Interfaces;
using RecruitmentSystem.Domain.Entities;
using RecruitmentSystem.Infrastructure.Data;

namespace RecruitmentSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IRepository<User> Users { get; private set; }
    public IRepository<Role> Roles { get; private set; }
    public IRepository<Job> Jobs { get; private set; }
    public IRepository<Candidate> Candidates { get; private set; }
    public IRepository<JobApplication> Applications { get; private set; }
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new Repository<User>(_context);
        Roles = new Repository<Role>(_context);
        Jobs = new Repository<Job>(_context);
        Candidates = new Repository<Candidate>(_context);
        Applications = new Repository<JobApplication>(_context);
    }
    
    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    
    public void Dispose() => _context.Dispose();
}

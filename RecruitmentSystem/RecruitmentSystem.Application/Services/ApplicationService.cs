using System;
using System.Linq;
using AutoMapper;
using RecruitmentSystem.Application.DTOs.JobApplication;
using RecruitmentSystem.Application.Interfaces;
using RecruitmentSystem.Domain.Entities;
using RecruitmentSystem.Domain.Enums;

namespace RecruitmentSystem.Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ApplicationDto>> GetApplicationsByCandidateAsync(int userId)
    {
        var candidates = await _unitOfWork.Candidates.FindAsync(c => c.UserId == userId);
        var candidate = candidates.FirstOrDefault();
        if (candidate == null) return Enumerable.Empty<ApplicationDto>();

        var apps = await _unitOfWork.Applications.FindAsync(a => a.CandidateId == candidate.Id);
        return _mapper.Map<IEnumerable<ApplicationDto>>(apps);
    }

    public async Task<IEnumerable<ApplicationDto>> GetApplicationsByJobAsync(int jobId)
    {
        var apps = await _unitOfWork.Applications.FindAsync(a => a.JobId == jobId);
        return _mapper.Map<IEnumerable<ApplicationDto>>(apps);
    }

    public async Task<bool> ApplyAsync(int jobId, int userId)
    {
        var candidates = await _unitOfWork.Candidates.FindAsync(c => c.UserId == userId);
        var candidate = candidates.FirstOrDefault();
        if (candidate == null) return false;

        var existing = await _unitOfWork.Applications.FindAsync(a => a.JobId == jobId && a.CandidateId == candidate.Id);
        if (existing.Any()) return false;

        var app = new JobApplication
        {
            JobId = jobId,
            CandidateId = candidate.Id,
            ApplicationDate = DateTime.UtcNow,
            Status = ApplicationStatus.Pending
        };

        await _unitOfWork.Applications.AddAsync(app);
        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task UpdateStatusAsync(int applicationId, ApplicationStatus status)
    {
        var app = await _unitOfWork.Applications.GetByIdAsync(applicationId);
        if (app != null)
        {
            app.Status = status;
            await _unitOfWork.CompleteAsync();
        }
    }
}

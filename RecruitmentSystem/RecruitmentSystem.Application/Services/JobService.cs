using AutoMapper;
using RecruitmentSystem.Application.DTOs.Job;
using RecruitmentSystem.Application.Interfaces;
using RecruitmentSystem.Domain.Entities;

namespace RecruitmentSystem.Application.Services;

public class JobService : IJobService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public JobService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<JobDto>> GetAllJobsAsync()
    {
        var jobs = await _unitOfWork.Jobs.GetAllAsync();
        return _mapper.Map<IEnumerable<JobDto>>(jobs);
    }

    public async Task<JobDto?> GetJobByIdAsync(int id)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);
        return _mapper.Map<JobDto>(job);
    }

    public async Task<JobDto> CreateJobAsync(CreateJobRequest request, int userId)
    {
        var job = _mapper.Map<Job>(request);
        job.CreatedById = userId;
        job.CreatedAt = DateTime.UtcNow;
        job.IsActive = true;

        await _unitOfWork.Jobs.AddAsync(job);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<JobDto>(job);
    }

    public async Task UpdateJobAsync(int id, CreateJobRequest request)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);
        if (job != null)
        {
            _mapper.Map(request, job);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task DeleteJobAsync(int id)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);
        if (job != null)
        {
            await _unitOfWork.Jobs.DeleteAsync(job);
            await _unitOfWork.CompleteAsync();
        }
    }
}

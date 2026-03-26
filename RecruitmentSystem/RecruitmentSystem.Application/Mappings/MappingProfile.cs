using AutoMapper;
using RecruitmentSystem.Application.DTOs.Auth;
using RecruitmentSystem.Application.DTOs.Job;
using RecruitmentSystem.Application.DTOs.JobApplication;
using RecruitmentSystem.Domain.Entities;

namespace RecruitmentSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, User>();
        
        CreateMap<Job, JobDto>()
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedBy.Username));
        CreateMap<CreateJobRequest, Job>();

        CreateMap<JobApplication, ApplicationDto>()
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title))
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate.User.Username));
    }
}

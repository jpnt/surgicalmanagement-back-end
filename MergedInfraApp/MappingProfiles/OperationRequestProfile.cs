using AutoMapper;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.ValueObjects;
using surgicalmanagement_back_end.MergedInfraApp.DTOs;
using surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;

namespace surgicalmanagement_back_end.MergedInfraApp.MappingProfiles;

public class OperationRequestProfile : Profile
{
    public OperationRequestProfile()
    {
        // Mapping from CreateOperationRequestDto to OperationRequest
        CreateMap<CreateOperationRequestDto, OperationRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is generated server-side
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OperationRequestStatus.Pending)) // Default status
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow)) // Set creation date
            .ForMember(dest => dest.LastUpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow)); // Set last updated date

        // Mapping from OperationRequest to OperationRequestDto
        CreateMap<OperationRequest, OperationRequestDto>();
    }
}
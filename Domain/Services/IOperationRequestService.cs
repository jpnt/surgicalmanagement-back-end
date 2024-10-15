using surgicalmanagement_back_end.MergedInfraApp.DTOs;
using surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;

namespace surgicalmanagement_back_end.Domain.Services;

public interface IOperationRequestService
{
    Task<OperationRequestDto> CreateOperationRequest(CreateOperationRequestDto dto);
    Task<OperationRequestDto> GetOperationRequest(Guid id);
    Task<List<OperationRequestDto>> GetAllOperationRequests();
    Task<OperationRequestDto> UpdateOperationRequest(OperationRequestDto dto);
    Task DeleteOperationRequest(Guid id);
}

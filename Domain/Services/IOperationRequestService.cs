using surgicalmanagement_back_end.MergedInfraApp.Common;
using surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;

namespace surgicalmanagement_back_end.Domain.Services;

public interface IOperationRequestService
{
    Task<Result<OperationRequestDto>> CreateOperationRequest(CreateOperationRequestDto dto);
    Task<Result<OperationRequestDto>> GetOperationRequest(Guid id);
    Task<Result<List<OperationRequestDto>>> GetAllOperationRequests();
    Task<Result<OperationRequestDto>> UpdateOperationRequest(OperationRequestDto dto);
    Task<Result<Guid>> DeleteOperationRequest(Guid id);
}
using AutoMapper;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;
using surgicalmanagement_back_end.Domain.Services;
using surgicalmanagement_back_end.Domain.Shared;
using surgicalmanagement_back_end.MergedInfraApp.DTOs;
using surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;
using surgicalmanagement_back_end.MergedInfraApp.Exceptions;

namespace surgicalmanagement_back_end.MergedInfraApp.Services;

public class OperationRequestService : IOperationRequestService
{
    private readonly IOperationRequestRepository _operationRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OperationRequestService(
        IOperationRequestRepository operationRequestRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _operationRequestRepository = operationRequestRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationRequestDto> CreateOperationRequest(CreateOperationRequestDto dto)
    {
        var operationRequest = _mapper.Map<OperationRequest>(dto);
        operationRequest.Id = Guid.NewGuid();

        await _operationRequestRepository.AddAsync(operationRequest);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<OperationRequestDto>(operationRequest);
    }

    public async Task<OperationRequestDto> GetOperationRequest(Guid id)
    {
        var operationRequest = await _operationRequestRepository.GetByIdAsync(id);

        if (operationRequest == null)
            throw new NotFoundException("The operation request was not found");

        return _mapper.Map<OperationRequestDto>(operationRequest);
    }

    public async Task<List<OperationRequestDto>> GetAllOperationRequests()
    {
        var operationRequests = await _operationRequestRepository.GetAllAsync();
        return _mapper.Map<List<OperationRequestDto>>(operationRequests);
    }

    public async Task<OperationRequestDto> UpdateOperationRequest(OperationRequestDto dto)
    {
        var existingRequest = await _operationRequestRepository.GetByIdAsync(dto.Id);

        if (existingRequest == null)
        {
            throw new NotFoundException("Operation request not found.");
        }

        // TODO
        if (existingRequest.DoctorId != dto.DoctorId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this operation request.");
        }

        // Update properties
        existingRequest.PatientId = dto.PatientId;
        existingRequest.OperationTypeId = dto.OperationTypeId;
        existingRequest.RequestedDate = dto.RequestedDate;
        existingRequest.Priority = dto.Priority;

        await _unitOfWork.CommitAsync();

        return _mapper.Map<OperationRequestDto>(existingRequest);
    }

    public async Task DeleteOperationRequest(Guid id)
    {
        var existingRequest = await _operationRequestRepository.GetByIdAsync(id);

        if (existingRequest == null)
        {
            throw new NotFoundException("Operation request not found.");
        }

        _operationRequestRepository.Remove(existingRequest);

        await _unitOfWork.CommitAsync();
    }
}
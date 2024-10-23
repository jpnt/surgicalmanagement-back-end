using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;
using surgicalmanagement_back_end.Domain.Services;
using surgicalmanagement_back_end.Domain.Shared;
using surgicalmanagement_back_end.Domain.ValueObjects;
using surgicalmanagement_back_end.MergedInfraApp.Common;
using surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;

namespace surgicalmanagement_back_end.MergedInfraApp.Services;

public class OperationRequestService : IOperationRequestService
{
    private readonly IMedicalHistoryService _medicalHistoryService;
    private readonly IStaffRepository _staffRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IOperationRequestRepository _operationRequestRepository;
    private readonly IOperationTypeRepository _operationTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<OperationRequestService> _logger;

    public OperationRequestService(
        IMedicalHistoryService medicalHistoryService,
        IStaffRepository staffRepository,
        IPatientRepository patientRepository,
        IOperationRequestRepository operationRequestRepository,
        IOperationTypeRepository operationTypeRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<OperationRequestService> logger)
    {
        _medicalHistoryService = medicalHistoryService;
        _staffRepository = staffRepository;
        _patientRepository = patientRepository;
        _operationRequestRepository = operationRequestRepository;
        _operationTypeRepository = operationTypeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    // -----------------------------------------------------------------------------------------------------------------
    public async Task<Result<OperationRequestDto>> CreateOperationRequest(CreateOperationRequestDto dto)
    {
        var doctor = await _staffRepository.GetByIdAsync(dto.DoctorId);
        if (doctor is null || doctor.Role != StaffRole.Doctor)
        {
            var error = $"Staff with id: {dto.DoctorId} not found or is not a valid doctor.";
            _logger.LogError(error);
            return error;
        }

        var patient = await _patientRepository.GetByIdAsync(dto.PatientId);
        if (patient is null)
        {
            var error = $"Patient with id: {dto.PatientId} not found.";
            _logger.LogError(error);
            return error;
        }

        var operationType = await _operationTypeRepository.GetByIdAsync(dto.OperationTypeId);
        if (operationType is null)
        {
            var error = "Operation type not found.";
            _logger.LogError(error);
            return error;
        }
        
        if (!doctor.HasSpecializationForOperationType(operationType))
        {
            var error = "Doctor does not have a specialization for this operation type.";
            _logger.LogError(error);
            return error;
        }
        
        var operationRequest = _mapper.Map<OperationRequest>(dto);
        operationRequest.Id = Guid.NewGuid(); // Business rule
        
        await _operationRequestRepository.AddAsync(operationRequest);
        await _unitOfWork.CommitAsync();

        await _medicalHistoryService.LogOperationRequestInMedicalHistory(patient, operationRequest);
        
        _logger.LogInformation($"Operation request with id: {operationRequest.Id} created.");
        return _mapper.Map<OperationRequestDto>(operationRequest);
    }

    // -----------------------------------------------------------------------------------------------------------------
    public async Task<Result<OperationRequestDto>> GetOperationRequest(Guid id)
    {
        var operationRequest = await _operationRequestRepository.GetByIdAsync(id);
        if (operationRequest == null)
            return "The operation request was not found";

        return _mapper.Map<OperationRequestDto>(operationRequest);
    }

    // -----------------------------------------------------------------------------------------------------------------
    public async Task<Result<List<OperationRequestDto>>> GetAllOperationRequests()
    {
        var operationRequests = await _operationRequestRepository.GetAllAsync();
        return _mapper.Map<List<OperationRequestDto>>(operationRequests);
    }

    // -----------------------------------------------------------------------------------------------------------------
    public async Task<Result<OperationRequestDto>> UpdateOperationRequest(OperationRequestDto dto)
    {
        var existingRequest = await _operationRequestRepository.GetByIdAsync(dto.Id);
        if (existingRequest == null)
        {
            return "Operation request not found.";
        }

        // AC: The system checks that only the requesting doctor can update the operation request.
        if (existingRequest.DoctorId != dto.DoctorId)
        {
            return "You are not authorized to update this operation request.";
        }

        // Update properties
        existingRequest.UpdateDeadline(dto.DeadlineDate);
        existingRequest.UpdatePriority(dto.Priority);
        
        // TODO: AC: The system logs all updates to the operation request (e.g., changes to priority or deadline).
        
        await _unitOfWork.CommitAsync();
        
        // TODO: Notify Planning Module
        //await _notificationService.NotifyPlanningModuleAsync($"Operation request {dto.Id} has been updated.");

        // TODO: AC: - Updated requests are reflected immediately in the system and notify the Planning Module of any changes.
        
        return _mapper.Map<OperationRequestDto>(existingRequest);
    }

    // -----------------------------------------------------------------------------------------------------------------
    public async Task<Result<Guid>> DeleteOperationRequest(Guid id)
    {
        var existingRequest = await _operationRequestRepository.GetByIdAsync(id);
        if (existingRequest == null)
            return $"Operation request with id: {id} was not found.";
        
        // TODO: Doctors can delete operation requests they created if the operation has not yet been scheduled.
        
        // TODO: A confirmation prompt is displayed before deletion
        
        // TODO: Once deleted, the operation request is removed from the patient’s medical record and cannot be recovered.

        _operationRequestRepository.Remove(existingRequest);

        await _unitOfWork.CommitAsync();

        // TODO: The system notifies the Planning Module and updates any schedules that were relying on this request.
        // TODO: Notify the planning module and update any schedules that were using this request
        //await _notificationService.NotifyPlanningModuleAsync($"Operation request {id} has been deleted.");

        return id;
    }
    
    // TODO
    // 5.1.19 As a Doctor, I want to list/search operation requisitions, so that I see the details,
    // edit, and remove operation requisitions
    // Acceptance Criteria:
    // - Doctors can search operation requests by patient name, operation type, priority, and status.
    // - The system displays a list of operation requests in a searchable and filterable view.
    // - Each entry in the list includes operation request details (e.g., patient name, operation type,
    // status).
    // 10
    // - Doctors can select an operation request to view, update, or delete it.
}
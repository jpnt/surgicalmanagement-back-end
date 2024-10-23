using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;
using surgicalmanagement_back_end.Domain.Services;
using surgicalmanagement_back_end.Domain.Shared;

namespace surgicalmanagement_back_end.MergedInfraApp.Services;

public class MedicalHistoryService : IMedicalHistoryService
{
    private readonly IMedicalHistoryEntryRepository _medicalHistoryEntryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MedicalHistoryService> _logger;
    
    public MedicalHistoryService(IMedicalHistoryEntryRepository medicalHistoryEntryRepository, IUnitOfWork unitOfWork, ILogger<MedicalHistoryService> logger)
    {
        _medicalHistoryEntryRepository = medicalHistoryEntryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task LogOperationRequestInMedicalHistory(Patient patient, OperationRequest operationRequest)
    {
        var entry = new MedicalHistoryEntry(patient.Id, operationRequest.Id);
        
        await _medicalHistoryEntryRepository.AddAsync(entry);
        await _unitOfWork.CommitAsync();
        
        _logger.LogInformation($"Logged operation request with id: {operationRequest.Id} in patient medical history.");
    }
}
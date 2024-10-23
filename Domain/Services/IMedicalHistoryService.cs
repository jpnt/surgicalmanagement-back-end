using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.MergedInfraApp.Common;

namespace surgicalmanagement_back_end.Domain.Services;

public interface IMedicalHistoryService
{
    Task LogOperationRequestInMedicalHistory(Patient patient, OperationRequest operationRequest);
}
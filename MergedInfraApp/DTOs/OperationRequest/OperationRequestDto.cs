using surgicalmanagement_back_end.Domain.ValueObjects;

namespace surgicalmanagement_back_end.MergedInfraApp.DTOs.OperationRequest;

public class OperationRequestDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid OperationTypeId { get; set; }
    public DateTime RequestedDate { get; set; }
    public OperationRequestPriority Priority { get; set; }
    public OperationRequestStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
}
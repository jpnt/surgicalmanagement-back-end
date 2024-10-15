using surgicalmanagement_back_end.Domain.ValueObjects;

namespace surgicalmanagement_back_end.Domain.Entities;

public class OperationRequest
{
    public Guid Id { get; internal set; }
    public Guid PatientId { get; internal set; }
    public Guid DoctorId { get; private set; }
    public Guid OperationTypeId { get; internal set; }
    public DateTime RequestedDate { get; internal set; }
    public OperationRequestPriority Priority { get; internal set; }
    public OperationRequestStatus Status { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }

    // Private constructor for EF
    private OperationRequest()
    {
    }

    // Factory method to create an OperationRequest
    public static OperationRequest Create(Guid patientId, Guid doctorId, Guid operationTypeId, DateTime requestedDate,
        OperationRequestPriority priority)
    {
        var operationRequest = new OperationRequest
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            DoctorId = doctorId,
            OperationTypeId = operationTypeId,
            RequestedDate = requestedDate,
            Priority = priority,
            Status = OperationRequestStatus.Pending,
            CreatedDate = DateTime.UtcNow,
            LastUpdatedDate = DateTime.UtcNow
        };

        // Add domain-specific validations here if needed

        return operationRequest;
    }

    // Methods to change the status
    public void Approve()
    {
        if (Status != OperationRequestStatus.Pending)
            throw new InvalidOperationException("Only pending requests can be approved.");

        Status = OperationRequestStatus.Approved;
        LastUpdatedDate = DateTime.UtcNow;
    }

    public void Reject()
    {
        if (Status != OperationRequestStatus.Pending)
            throw new InvalidOperationException("Only pending requests can be rejected.");

        Status = OperationRequestStatus.Rejected;
        LastUpdatedDate = DateTime.UtcNow;
    }
}
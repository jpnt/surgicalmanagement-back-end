using surgicalmanagement_back_end.Domain.Shared;
using surgicalmanagement_back_end.Domain.ValueObjects;

namespace surgicalmanagement_back_end.Domain.Entities;

public class OperationRequest : Entity<Guid>
{
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; } // Staff.Role = Doctor
    public Guid OperationTypeId { get; private set; }
    public DateTime DeadlineDate { get; private set; }
    public OperationRequestPriority Priority { get; private set; }
    public string Description { get; private set; }
    public OperationRequestStatus Status { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }

    // Private constructor for EF
    private OperationRequest() : base(Guid.NewGuid())
    {
        Status = OperationRequestStatus.Pending; // Default status
        CreatedDate = DateTime.UtcNow; // Set created date
        LastUpdatedDate = DateTime.UtcNow; // Set last updated date
    }

    // Factory method to create an OperationRequest
    public static OperationRequest Create(Guid patientId, Guid doctorId, Guid operationTypeId, DateTime deadlineDate,
        OperationRequestPriority priority, Specialization doctorSpecialization, Specialization operationSpecialization)
    {
        if (doctorSpecialization != operationSpecialization)
            throw new InvalidOperationException("Doctor specialization does not match this operation type.");

        if (deadlineDate < DateTime.UtcNow)
            throw new ArgumentException("Deadline date cannot be in the past.");

        var operationRequest = new OperationRequest
        {
            PatientId = patientId,
            DoctorId = doctorId,
            OperationTypeId = operationTypeId,
            DeadlineDate = deadlineDate,
            Priority = priority,
        };

        return operationRequest;
    }

    // AC: Doctors can update operation requests they created (e.g., change the deadline or priority).
    public void UpdateDeadline(DateTime deadlineDate)
    {
        DeadlineDate = deadlineDate;
        LastUpdatedDate = DateTime.UtcNow;
    }

    public void UpdatePriority(OperationRequestPriority priority)
    {
        Priority = priority;
        LastUpdatedDate = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
        Description = description;
        LastUpdatedDate = DateTime.UtcNow;
    }

    // Business behavior to change the status with validation
    public void Approve()
    {
        ChangeStatus(OperationRequestStatus.Approved);
    }

    public void Reject()
    {
        ChangeStatus(OperationRequestStatus.Rejected);
    }

    // Private helper to ensure status change rules
    private void ChangeStatus(OperationRequestStatus newStatus)
    {
        if (Status != OperationRequestStatus.Pending)
            throw new InvalidOperationException("Only pending requests can be approved or rejected.");

        Status = newStatus;
        LastUpdatedDate = DateTime.UtcNow;
    }
}
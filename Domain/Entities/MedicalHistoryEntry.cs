using surgicalmanagement_back_end.Domain.Shared;

namespace surgicalmanagement_back_end.Domain.Entities;

public class MedicalHistoryEntry : Entity<Guid>
{
    public Guid PatientId { get; private set; }
    public Guid OperationRequestId { get; private set; }
    public DateTime EntryDate { get; private set; }
    
    public MedicalHistoryEntry(Guid patientId, Guid operationRequestId) : base(Guid.NewGuid())
    {
        PatientId = patientId;
        OperationRequestId = operationRequestId;
        EntryDate = DateTime.UtcNow;
    }
}
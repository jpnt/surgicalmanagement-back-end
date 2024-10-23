using surgicalmanagement_back_end.Domain.Repositories;
using surgicalmanagement_back_end.Domain.Shared;

namespace surgicalmanagement_back_end.Domain.Entities;

public class Patient : Entity<Guid>
{
    // TODO: no other attributes for simplicity
    public string MedicalRecordNumber { get; private set; } // TODO: string for simplicity

    protected Patient() : base(Guid.NewGuid()) // For EF Core
    {
    }

    public Patient(string medicalRecordNumber) : base(Guid.NewGuid())
    {
        MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentNullException(nameof(medicalRecordNumber));
    }

    public void UpdateMedicalRecordNumber(string medicalRecordNumber)
    {
        MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentNullException(nameof(medicalRecordNumber));
    }
}
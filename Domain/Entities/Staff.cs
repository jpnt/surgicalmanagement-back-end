using surgicalmanagement_back_end.Domain.Shared;
using surgicalmanagement_back_end.Domain.ValueObjects;

namespace surgicalmanagement_back_end.Domain.Entities;

public class Staff : Entity<Guid>
{
    public Specialization Specialization { get; private set; }
    public StaffRole Role { get; private set; } // TODO :Added this

    protected Staff() : base(Guid.NewGuid()) // For EF Core
    {
    }

    public Staff(Specialization specialization, StaffRole role) : base(Guid.NewGuid())
    {
        Specialization = specialization;
        Role = role;
    }

    public void UpdateSpecialization(Specialization specialization)
    {
        Specialization = specialization;
    }

    public bool HasSpecializationForOperationType(OperationType operationType)
    {
        return Specialization == operationType.SpecializationRequired;
    }
}
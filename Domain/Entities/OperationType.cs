using surgicalmanagement_back_end.Domain.Shared;
using surgicalmanagement_back_end.Domain.ValueObjects;

namespace surgicalmanagement_back_end.Domain.Entities;

public class OperationType : Entity<Guid>
{
    public string Name { get; set; }
    public Specialization SpecializationRequired { get; set; }
    
    public OperationType(Guid id, string name) : base(id)
    {
        Name = name;
    }
    
}
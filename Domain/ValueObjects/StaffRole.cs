using System.ComponentModel;

namespace surgicalmanagement_back_end.Domain.ValueObjects;

public enum StaffRole
{
    [Description("Administrator")]
    Administrator,
    
    [Description("Doctor")]
    Doctor,
    
    [Description("Nurse")]
    Nurse
}
using System.ComponentModel;

namespace surgicalmanagement_back_end.Domain.ValueObjects;

public enum OperationRequestStatus
{
    [Description("Pending")]
    Pending,
    
    [Description("Approved")]
    Approved,
    
    [Description("Rejected")]
    Rejected,
    
    [Description("Scheduled")]
    Scheduled,
    
    [Description("Completed")]
    Completed,
    
    [Description("Cancelled")]
    Canceled
}
namespace surgicalmanagement_back_end.Domain.ValueObjects;

public enum OperationRequestPriority
{
    Emergency = 0,
    VeryUrgent = 1,
    Urgent = 2,
    Scheduled = 3,
    Elective = 4
}
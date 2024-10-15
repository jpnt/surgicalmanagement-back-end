namespace surgicalmanagement_back_end.Domain.ValueObjects;

public enum OperationRequestStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Scheduled = 3,
    Completed = 4,
    Canceled = 5
}
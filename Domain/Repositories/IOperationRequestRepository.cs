using surgicalmanagement_back_end.Domain.Entities;

namespace surgicalmanagement_back_end.Domain.Repositories;

public interface IOperationRequestRepository : IRepository<OperationRequest, Guid>
{
}
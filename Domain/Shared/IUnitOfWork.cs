namespace surgicalmanagement_back_end.Domain.Shared;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
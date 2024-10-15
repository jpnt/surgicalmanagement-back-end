using surgicalmanagement_back_end.Domain.Shared;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await this._context.SaveChangesAsync();
    }
}
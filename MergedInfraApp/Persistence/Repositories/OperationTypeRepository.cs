using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence.Repositories;

public class OperationTypeRepository : IOperationTypeRepository
{
    private readonly ApplicationDbContext _context;
    
    public OperationTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<OperationType>> GetAllAsync()
    {
        return await _context.OperationTypes.ToListAsync();
    }

    public async Task<OperationType> GetByIdAsync(Guid id)
    {
        return await _context.OperationTypes.FindAsync(id);
    }

    public async Task<List<OperationType>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.OperationTypes
            .Where(o => ids.Contains(o.Id))
            .ToListAsync();
    }

    public async Task<OperationType> AddAsync(OperationType obj)
    {
        await _context.OperationTypes.AddAsync(obj);
        await _context.SaveChangesAsync();
        return obj;
    }

    public void Remove(OperationType obj)
    {
        _context.OperationTypes.Remove(obj);
    }
}
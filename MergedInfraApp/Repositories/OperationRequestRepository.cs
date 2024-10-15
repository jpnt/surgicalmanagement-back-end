using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;
using surgicalmanagement_back_end.MergedInfraApp.Persistence;

namespace surgicalmanagement_back_end.MergedInfraApp.Repositories;

public class OperationRequestRepository : IOperationRequestRepository
{
    private readonly ApplicationDbContext _context;

    public OperationRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<OperationRequest>> GetAllAsync()
    {
        return await _context.OperationRequests.ToListAsync();
    }

    public async Task<OperationRequest> GetByIdAsync(Guid id)
    {
        return await _context.OperationRequests.FindAsync(id);
    }

    public async Task<List<OperationRequest>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.OperationRequests
            .Where(or => ids.Contains(or.Id))
            .ToListAsync();
    }

    public async Task<OperationRequest> AddAsync(OperationRequest obj)
    {
        await _context.OperationRequests.AddAsync(obj);
        await _context.SaveChangesAsync(); // Commit the transaction
        return obj; // Return the added entity
    }

    public void Remove(OperationRequest obj)
    {
        _context.OperationRequests.Remove(obj);
    }
}
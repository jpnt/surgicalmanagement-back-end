using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly ApplicationDbContext _context;

    public StaffRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Staff>> GetAllAsync()
    {
        return await _context.Staffs.ToListAsync();
    }

    public async Task<Staff> GetByIdAsync(Guid id)
    {
        return await _context.Staffs.FindAsync(id);
    }

    public async Task<List<Staff>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.Staffs
            .Where(or => ids.Contains(or.Id))
            .ToListAsync();    }

    public async Task<Staff> AddAsync(Staff obj)
    {
        await _context.Staffs.AddAsync(obj);
        await _context.SaveChangesAsync(); // Commit the transaction
        return obj; // Return the added entity
    }

    public void Remove(Staff obj)
    {
        _context.Staffs.Remove(obj);
    }
}
using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence.Repositories;

public class MedicalHistoryEntryRepository : IMedicalHistoryEntryRepository
{
    private readonly ApplicationDbContext _context;

    public MedicalHistoryEntryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MedicalHistoryEntry>> GetAllAsync()
    {
        return await _context.MedicalHistoryEntries.ToListAsync();
    }

    public async Task<MedicalHistoryEntry> GetByIdAsync(Guid id)
    {
        return await _context.MedicalHistoryEntries.FindAsync(id);
    }

    public async Task<List<MedicalHistoryEntry>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.MedicalHistoryEntries
            .Where(or => ids.Contains(or.Id))
            .ToListAsync();
    }

    public async Task<MedicalHistoryEntry> AddAsync(MedicalHistoryEntry obj)
    {
        await _context.MedicalHistoryEntries.AddAsync(obj);
        await _context.SaveChangesAsync(); // Commit the transaction
        return obj; // Return the added entity
    }

    public void Remove(MedicalHistoryEntry obj)
    {
        _context.MedicalHistoryEntries.Remove(obj);
    }
}
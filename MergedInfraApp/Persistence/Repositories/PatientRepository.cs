using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Repositories;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> GetByIdAsync(Guid id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<List<Patient>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.Patients
            .Where(or => ids.Contains(or.Id))
            .ToListAsync();
    }

    public async Task<Patient> AddAsync(Patient obj)
    {
        await _context.Patients.AddAsync(obj);
        await _context.SaveChangesAsync(); // Commit the transaction
        return obj; // Return the added entity
    }

    public void Remove(Patient obj)
    {
        _context.Patients.Remove(obj);
    }
}
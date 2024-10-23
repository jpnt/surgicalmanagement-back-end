using surgicalmanagement_back_end.Domain.Entities;
using surgicalmanagement_back_end.Domain.Shared;

namespace surgicalmanagement_back_end.Domain.Repositories;

public interface IMedicalHistoryEntryRepository : IRepository<MedicalHistoryEntry, Guid>
{
}
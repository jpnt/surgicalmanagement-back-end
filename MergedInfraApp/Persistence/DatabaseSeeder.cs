using Bogus;
using surgicalmanagement_back_end.Domain.Entities;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;

    public DatabaseSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        /*if (!_context.Staffs.Any()) // Add condition to avoid reseeding
        {
            var faker = new Faker<Staff>()
                .RuleFor(s => s.Id, f => Guid.NewGuid())
                .RuleFor(s => s.Name, f => f.Name.FullName())
                .RuleFor(s => s.Position, f => f.Name.JobTitle())
                .RuleFor(s => s.Email, f => f.Internet.Email());

            _context.Staffs.AddRange(faker.Generate(50)); // Generates 50 staff records
        }

        if (!_context.OperationRequests.Any())
        {
            var operationFaker = new Faker<OperationRequest>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.RequestDate, f => f.Date.Past());

            _context.OperationRequests.AddRange(operationFaker.Generate(30)); // 30 random operations
        }*/

        await _context.SaveChangesAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Entities;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<OperationRequest> OperationRequests { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        
        // TODO: Put this in a separate class for all entities model building stuff
        modelBuilder.Entity<OperationRequest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PatientId).IsRequired();
            entity.Property(e => e.DoctorId).IsRequired();
            entity.Property(e => e.OperationTypeId).IsRequired();
            entity.Property(e => e.RequestedDate).IsRequired();
            entity.Property(e => e.Priority).IsRequired().HasConversion<string>();
            entity.Property(e => e.Status).IsRequired().HasConversion<string>();
            entity.Property(e => e.CreatedDate).IsRequired();
            entity.Property(e => e.LastUpdatedDate).IsRequired();
        });
    }
    
}
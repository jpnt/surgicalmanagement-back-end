using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Entities;

namespace surgicalmanagement_back_end.MergedInfraApp.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<OperationRequest> OperationRequests { get; set; }
    public DbSet<OperationType> OperationTypes { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<MedicalHistoryEntry> MedicalHistoryEntries { get; set; } //TODO

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: Put this in a separate class for all entities model building stuff
        //modelBuilder.ApplyConfiguration(new OperationRequestTypeConfiguration());
        modelBuilder.Entity<OperationRequest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PatientId).IsRequired();
            entity.Property(e => e.DoctorId).IsRequired();
            entity.Property(e => e.OperationTypeId).IsRequired();
            entity.Property(e => e.DeadlineDate).IsRequired();
            entity.Property(e => e.Priority).IsRequired().HasConversion<string>();
            entity.Property(e => e.Status).IsRequired().HasConversion<string>();
            entity.Property(e => e.CreatedDate).IsRequired();
            entity.Property(e => e.LastUpdatedDate).IsRequired();
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Specialization).IsRequired().HasConversion<string>();
            entity.Property(e => e.Role).IsRequired().HasConversion<string>();
        });

        modelBuilder.Entity<OperationType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasConversion<string>();
            entity.Property(e => e.SpecializationRequired).IsRequired().HasConversion<string>();
        });


        base.OnModelCreating(modelBuilder);
    }
}
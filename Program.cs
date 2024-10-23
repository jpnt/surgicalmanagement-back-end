using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Repositories;
using surgicalmanagement_back_end.Domain.Services;
using surgicalmanagement_back_end.Domain.Shared;
using surgicalmanagement_back_end.MergedInfraApp.MappingProfiles;
using surgicalmanagement_back_end.MergedInfraApp.Persistence;
using surgicalmanagement_back_end.MergedInfraApp.Persistence.Repositories;
using surgicalmanagement_back_end.MergedInfraApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// DEBUG: Print the Connection String
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

// Add seeder to services
//builder.Services.AddScoped<DatabaseSeeder>();

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<IOperationRequestService, OperationRequestService>();
builder.Services.AddScoped<IMedicalHistoryService, MedicalHistoryService>();

// Register Repositories
builder.Services.AddScoped<IOperationRequestRepository, OperationRequestRepository>();
builder.Services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IMedicalHistoryEntryRepository, MedicalHistoryEntryRepository>();

// Register AutoMapper and scan for profiles
builder.Services.AddAutoMapper(typeof(OperationRequestProfile));

// Add Swagger for API documentation (optional but recommended)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seed the database with random data
// using (var scope = app.Services.CreateScope())
// {
//     var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
//     await seeder.SeedAsync();
// }

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using surgicalmanagement_back_end.Domain.Repositories;
using surgicalmanagement_back_end.Domain.Services;
using surgicalmanagement_back_end.Domain.Shared;
using surgicalmanagement_back_end.MergedInfraApp.MappingProfiles;
using surgicalmanagement_back_end.MergedInfraApp.Persistence;
using surgicalmanagement_back_end.MergedInfraApp.Repositories;
using surgicalmanagement_back_end.MergedInfraApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// DEBUG
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<IOperationRequestService, OperationRequestService>();

// Register Repositories
builder.Services.AddScoped<IOperationRequestRepository, OperationRequestRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
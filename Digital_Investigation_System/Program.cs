using Microsoft.EntityFrameworkCore;
using DigitalInvestigationAPI.Data;
using DigitalInvestigationAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// SERVICES
// =====================================================

// Add Controllers
builder.Services.AddControllers();

// Allow React frontend to access the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add API Explorer
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<InvestigationRepository>();

// Add Swagger
builder.Services.AddSwaggerGen();

// Register Entity Framework Core DbContext
builder.Services.AddDbContext<DigitalInvestigationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// =====================================================
// BUILD APPLICATION
// =====================================================

var app = builder.Build();

// =====================================================
// HTTP REQUEST PIPELINE
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS for React
app.UseCors("ReactPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
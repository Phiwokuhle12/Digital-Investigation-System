
using Microsoft.EntityFrameworkCore;
using DigitalInvestigationAPI.Data;
using DigitalInvestigationAPI.Repositories;
using DigitalInvestigationAPI.Services;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// =====================================================
// SERVICES
// =====================================================

// Add Controllers
builder.Services.AddControllers();

// Configure CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://phiwokuhle12.github.io"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add API Explorer
builder.Services.AddEndpointsApiExplorer();

// Register repository
builder.Services.AddScoped<InvestigationRepository>();

// Register Investigation Docket Service
builder.Services.AddScoped<InvestigationDocketService>();

// Add Swagger
builder.Services.AddSwaggerGen();

// Register PostgreSQL DbContext
builder.Services.AddDbContext<DigitalInvestigationContext>(options =>
    options.UseNpgsql(
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

// HTTPS redirection
app.UseHttpsRedirection();

// Enable CORS
app.UseCors("ReactPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

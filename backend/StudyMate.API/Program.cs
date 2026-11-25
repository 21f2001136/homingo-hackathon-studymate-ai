using StudyMate.Application.Interfaces;
using StudyMate.Application.Services;
using StudyMate.Infrastructure.AI;
using StudyMate.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

builder.Services.Configure<GeminiOptions>(
    builder.Configuration.GetSection(GeminiOptions.SectionName));

builder.Services.AddHttpClient<GeminiAIService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<IStudyService, StudyService>();
builder.Services.AddScoped<IAIService, GeminiAIService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
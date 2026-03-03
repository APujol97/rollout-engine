

using RolloutEngine.Api.Contracts;
using RolloutEngine.Application.Abstractions;
using RolloutEngine.Domain.Entities;
using RolloutEngine.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFeatureFlagRepository, InMemoryFeatureFlagRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/flags", async (
    CreateFeatureFlagRequest request,
    IFeatureFlagRepository repository,
    CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(request.Key))
        return Results.BadRequest(new { error = "Key is required." });

    if (string.IsNullOrWhiteSpace(request.Name))
        return Results.BadRequest(new { error = "Name is required." });

    var existing = await repository.GetByKeyAsync(request.Key, cancellationToken);
    if (existing != null)
        return Results.Conflict(new { error = $"A flag with key '{request.Key}' already exists." });

    var flag = new FeatureFlag(request.Key.Trim(), request.Name.Trim(), request.Description?.Trim());

    await repository.AddAsync(flag, cancellationToken);

    var response = new FeatureFlagResponse
    {
        Id = flag.Id,
        Key = flag.Key,
        Name = flag.Name,
        Description = flag.Description,
        Enabled = flag.Enabled,
        CreatedAtUtc = flag.CreatedAtUtc
    };

    return Results.Created($"/api/flags/{flag.Key}", response);
})
.WithName("CreateFeatureFlag")
.WithTags("Flags");

app.MapGet("/api/flags/{key}", async (
    string key,
    IFeatureFlagRepository repository,
    CancellationToken cancellationToken) =>
{
    var flag = await repository.GetByKeyAsync(key, cancellationToken);

    if (flag is null)
        return Results.NotFound(new { error = $"Falg '{key}' was not found" });

    var response = new FeatureFlagResponse
    {
        Id = flag.Id,
        Key = flag.Key,
        Name = flag.Name,
        Description = flag.Description,
        Enabled = flag.Enabled,
        CreatedAtUtc = flag.CreatedAtUtc
    };

    return Results.Ok(response);
})
.WithName("GetFeatureFlagByKey")
.WithTags("Flags");

app.Run();

public partial class Program { }

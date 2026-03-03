namespace RolloutEngine.Api.Contracts
{
    public sealed class FeatureFlagResponse
    {
        public Guid Id { get; init; }
        public string Key { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool Enabled { get; init; }
        public DateTime CreatedAtUtc { get; init; }
    }
}

namespace RolloutEngine.Api.Contracts
{
    public sealed class CreateFeatureFlagRequest
    {
        public string Key { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
    }
}

namespace RolloutEngine.Domain.Entities
{
    public sealed class FeatureFlag
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Key { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool Enabled { get; private set; }
        public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;

        public FeatureFlag(string key, string name, string? description)
        {
            Key = key;
            Name = name;
            Description = description;
            Enabled = false;
        }

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;
    }
}

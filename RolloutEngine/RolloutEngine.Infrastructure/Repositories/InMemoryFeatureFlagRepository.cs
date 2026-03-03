using RolloutEngine.Application.Abstractions;
using RolloutEngine.Domain.Entities;

namespace RolloutEngine.Infrastructure.Repositories
{
    public sealed class InMemoryFeatureFlagRepository : IFeatureFlagRepository
    {
        private readonly List<FeatureFlag> _items = [];

        public Task<FeatureFlag?> GetByKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            var item = _items.FirstOrDefault(x =>
                x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(item);
        }

        public Task AddAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default)
        {
            _items.Add(featureFlag);
            return Task.CompletedTask;
        }
    }
}

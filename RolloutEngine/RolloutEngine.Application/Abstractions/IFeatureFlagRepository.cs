using RolloutEngine.Domain.Entities;

namespace RolloutEngine.Application.Abstractions
{

    public interface IFeatureFlagRepository
    {
        Task<FeatureFlag?> GetByKeyAsync(string key, CancellationToken cancellationToken = default);
        Task AddAsync(FeatureFlag featureFlag, CancellationToken cancellationToken = default);
    }

}

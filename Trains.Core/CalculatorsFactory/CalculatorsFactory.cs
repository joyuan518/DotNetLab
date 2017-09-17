namespace Trains.Core.CalculatorsFactory
{
    using CacheProvider;
    using CalculatorsInterfaces;
    using CalculatorsImplementations;

    /// <summary>
    /// the route calculator factory class, which will create the calculators with memory cache provider
    /// </summary>
    internal sealed class CalculatorsFactory : BaseCalculatorsFactory
    {
        private readonly ICacheProvider _cacheProvider;

        public CalculatorsFactory()
        {
            _cacheProvider =
                (new CacheProviderFactory().CreateCacheProvider(CacheProviderFactory.CacheProviderType.Simple));
        }

        public override IDistanceCalculator CreateDistanceCalculator()
        {
            return new DefaultCalculatorsImplementation(_cacheProvider);
        }

        public override IExactlyStopsRoutesCalculator CreateExactlyStopsRoutesCalculator()
        {
            return new DefaultCalculatorsImplementation(_cacheProvider);
        }

        public override IMaxDistanceRoutesCalculator CreateMaxDistanceRoutesCalculator()
        {
            return new DefaultCalculatorsImplementation(_cacheProvider);
        }

        public override IMaxStopsRoutesCalculator CreateMaxStopsRoutesCalculator()
        {
            return new DefaultCalculatorsImplementation(_cacheProvider);
        }

        public override IShortestDistanceCalculator CreateShortestDistanceCalculator()
        {
            return new DefaultCalculatorsImplementation(_cacheProvider);
        }
    }
}

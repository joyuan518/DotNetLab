namespace Trains.Core.CacheProvider
{
    /// <summary>
    /// the factory class to create the cache provider instances
    /// </summary>
    internal class CacheProviderFactory
    {
        public enum CacheProviderType
        {
            Simple
        }

        public ICacheProvider CreateCacheProvider(CacheProviderType cacheProviderType)
        {
            ICacheProvider cacheProvider;

            switch (cacheProviderType)
            {
                case CacheProviderType.Simple:
                    {
                        cacheProvider = new SimpleCacheProvider();
                        break;
                    }

                default:
                    {
                        cacheProvider = new SimpleCacheProvider();
                        break;
                    }
            }

            return cacheProvider;
        }

    }
}

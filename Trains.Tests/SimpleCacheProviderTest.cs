namespace Trains.Tests
{
    using NUnit.Framework;
    using Core.CacheProvider;
    using System.Collections.Generic;

    [TestFixture]
    public class SimpleCacheProviderTest
    {
        private readonly ICacheProvider _cacheProvider = new CacheProviderFactory().CreateCacheProvider(CacheProviderFactory.CacheProviderType.Simple);

        [Test]
        public void TestPutAndGetRoutesForExactlyStops()
        {
            _cacheProvider.PutRoutesForExactlyStops(1, 2, 10, 5);
            Assert.AreEqual(5, _cacheProvider.GetRoutesForExactlyStops(1, 2, 10));
        }

        [Test]
        public void TestPutAndGetRoutesForMaxStops()
        {
            _cacheProvider.PutRoutesForMaxStops(1, 2, 10, 5);
            Assert.AreEqual(5, _cacheProvider.GetRoutesForMaxStops(1, 2, 10));
        }

        [Test]
        public void TestPutAndRoutesForMaxDistance()
        {
            _cacheProvider.PutRoutesForMaxDistance(1, 2, 10, 5);
            Assert.AreEqual(5, _cacheProvider.GetRoutesForMaxDistance(1, 2, 10));
        }

        [Test]
        public void TestPutAndGetShortestDistance()
        {
            _cacheProvider.PutShortestDistance(1, new Dictionary<int, int>() { {2, 20}, {3, 40} });
            Assert.AreEqual(40, _cacheProvider.GetShortestDistance(1, 3));
        }
    }
}

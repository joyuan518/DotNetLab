namespace Trains.Tests
{
    using NUnit.Framework;
    using Core.CalculatorsFactory;

    [TestFixture]
    public class DefaultCalculatorsImplementationTest
    {
        private readonly BaseCalculatorsFactory _calculatorsFactory = new CalculatorsFactory();

        private readonly int[,] _graphData = new int[5, 5] { {int.MaxValue, 5, int.MaxValue, 5, 7},
                                                 {int.MaxValue, int.MaxValue, 4, int.MaxValue, int.MaxValue},
                                                 {int.MaxValue, int.MaxValue, int.MaxValue, 8, 2},
                                                 {int.MaxValue, int.MaxValue, 8, int.MaxValue, 6},
                                                 {int.MaxValue, 3, int.MaxValue, int.MaxValue, int.MaxValue} };

        [Test]
        public void TestCalculateDistance()
        {
            var distanceCalculator = _calculatorsFactory.CreateDistanceCalculator();
            var route = new [] {0, 1, 2};

            var actualResult = distanceCalculator.CalculateDistance(_graphData, route);

            Assert.AreEqual(9, actualResult);

        }

        [Test]
        public void TestGetRoutesForExactlyStops()
        {
            var exactlyStopsRoutesCalculator = _calculatorsFactory.CreateExactlyStopsRoutesCalculator();
            var actualResult = exactlyStopsRoutesCalculator.GetRoutesForExactlyStops(_graphData, 0, 2, 4);

            Assert.AreEqual(3, actualResult);
        }

        [Test]
        public void TestGetRoutesForMaxDistance()
        {
            var maxDistanceRoutesCalculator = _calculatorsFactory.CreateMaxDistanceRoutesCalculator();
            var actualResult = maxDistanceRoutesCalculator.GetRoutesForMaxDistance(_graphData, 2, 2, 30);

            Assert.AreEqual(7, actualResult);
        }

        [Test]
        public void TestGetRoutesForMaxStops()
        {
            var maxStopsRoutesCalculator = _calculatorsFactory.CreateMaxStopsRoutesCalculator();
            var actualResult = maxStopsRoutesCalculator.GetRoutesForMaxStops(_graphData, 2, 2, 3);

            Assert.AreEqual(2, actualResult);
        }

        [Test]
        public void TestGetShortestRouteDistance()
        {
            var shortestDistanceCalculator = _calculatorsFactory.CreateShortestDistanceCalculator();
            var actualResult = shortestDistanceCalculator.GetShortestRouteDistance(_graphData, 1, 1);

            Assert.AreEqual(9, actualResult);
        }
    }
}

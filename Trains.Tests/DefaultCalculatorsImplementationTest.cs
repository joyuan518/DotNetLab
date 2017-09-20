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

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void Cleanup()
        {

        }

        [TestCase(new[] { 0, 1, 2 }, 9)]
        public void TestCalculateDistance(int[] route, int expectedResult) 
        {
            var distanceCalculator = _calculatorsFactory.CreateDistanceCalculator();

            var actualResult = distanceCalculator.CalculateDistance(_graphData, route);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(0, 2, 4, 3)]
        public void TestGetRoutesForExactlyStops(int startingPoint, int endPoint, int exactlyStops, int expectedResult)
        {
            var exactlyStopsRoutesCalculator = _calculatorsFactory.CreateExactlyStopsRoutesCalculator();
            var actualResult = exactlyStopsRoutesCalculator.GetRoutesForExactlyStops(_graphData, startingPoint, endPoint, exactlyStops);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(2, 2, 30, 7)]
        public void TestGetRoutesForMaxDistance(int startingPoint, int endPoint, int maxDistance, int expectedResult)
        {
            var maxDistanceRoutesCalculator = _calculatorsFactory.CreateMaxDistanceRoutesCalculator();
            var actualResult = maxDistanceRoutesCalculator.GetRoutesForMaxDistance(_graphData, startingPoint, endPoint, maxDistance);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(2, 2, 3, 2)]
        public void TestGetRoutesForMaxStops(int startingPoint, int endPoint, int maxStops, int expectedResult)
        {
            var maxStopsRoutesCalculator = _calculatorsFactory.CreateMaxStopsRoutesCalculator();
            var actualResult = maxStopsRoutesCalculator.GetRoutesForMaxStops(_graphData, startingPoint, endPoint, maxStops);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(1, 1, 9)]
        public void TestGetShortestRouteDistance(int startingPoint, int endPoint, int expectedResult)
        {
            var shortestDistanceCalculator = _calculatorsFactory.CreateShortestDistanceCalculator();
            var actualResult = shortestDistanceCalculator.GetShortestRouteDistance(_graphData, startingPoint, endPoint);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

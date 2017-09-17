namespace Trains.Tests
{
    using NUnit.Framework;
    using Core.CalculatorsFactory;
    using Core.TripsInfoProvider;

    [TestFixture]
    public class DefaultTripsInfoProviderTest
    {
        private readonly DefaultTripsInfoProvider _tripsInfoProvider = new DefaultTripsInfoProvider("AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7", new CalculatorsFactory());

        [Test]
        public void TestGetAdjacentMatrix()
        {
            var actualResult = _tripsInfoProvider.GetAdjacentMatrix("AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7");

            var expectedResult = new int[5, 5] { {int.MaxValue, 5, int.MaxValue, 5, 7}, 
                                                 {int.MaxValue, int.MaxValue, 4, int.MaxValue, int.MaxValue}, 
                                                 {int.MaxValue, int.MaxValue, int.MaxValue, 8, 2}, 
                                                 {int.MaxValue, int.MaxValue, 8, int.MaxValue, 6}, 
                                                 {int.MaxValue, 3, int.MaxValue, int.MaxValue, int.MaxValue} };

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestGetVertexArray()
        {
            _tripsInfoProvider.GetAdjacentMatrix("AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7");

            var actualResult = _tripsInfoProvider.GetVertexArray("A-B-E-D");
            var expectedResult = new int[] {0, 1, 4, 3};

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

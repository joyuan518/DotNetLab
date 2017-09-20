namespace Trains.Tests
{
    using NUnit.Framework;
    using Core.CalculatorsFactory;
    using Core.TripsInfoProvider;
    using System.Collections;

    [TestFixture]
    public class DefaultTripsInfoProviderTest
    {
        private readonly DefaultTripsInfoProvider _tripsInfoProvider = new DefaultTripsInfoProvider("AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7", new CalculatorsFactory());

        public static IEnumerable TestGetAdjacentMatrix_Cases
        {
            get
            {
                yield return new TestCaseData("AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7",
                                              new int[5, 5] { {int.MaxValue, 5, int.MaxValue, 5, 7},
                                                              {int.MaxValue, int.MaxValue, 4, int.MaxValue, int.MaxValue},
                                                              {int.MaxValue, int.MaxValue, int.MaxValue, 8, 2},
                                                              {int.MaxValue, int.MaxValue, 8, int.MaxValue, 6},
                                                              {int.MaxValue, 3, int.MaxValue, int.MaxValue, int.MaxValue} });
            }
        }

        public static IEnumerable TestGetVertexArray_Cases
        {
            get
            {
                yield return new TestCaseData("AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7", "A-B-E-D", new int[] { 0, 1, 4, 3 });
            }
        }

        [Test, TestCaseSource(nameof(TestGetAdjacentMatrix_Cases))]
        public void TestGetAdjacentMatrix(string graph, int[,] expectedResult)
        {
            var actualResult = _tripsInfoProvider.GetAdjacentMatrix(graph);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test, TestCaseSource(nameof(TestGetVertexArray_Cases))]
        public void TestGetVertexArray(string graph, string route, int[] expectedResult)
        {
            _tripsInfoProvider.GetAdjacentMatrix(graph);
            var actualResult = _tripsInfoProvider.GetVertexArray(route);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

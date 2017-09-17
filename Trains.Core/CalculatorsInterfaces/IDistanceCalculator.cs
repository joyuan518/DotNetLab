namespace Trains.Core.CalculatorsInterfaces
{
    internal interface IDistanceCalculator
    {
        /// <summary>
        /// to calculate the distance value of a given route
        /// </summary>
        /// <param name="graph">
        /// the graph, represented by adjacent matrix
        /// </param>
        /// <param name="route">
        /// the route, represented by an integer arrary.
        /// </param>
        /// <returns></returns>
        int? CalculateDistance(int[,] graph, int[] route);
    }
}

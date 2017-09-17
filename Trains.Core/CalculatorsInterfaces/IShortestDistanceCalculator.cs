namespace Trains.Core.CalculatorsInterfaces
{
    internal interface IShortestDistanceCalculator
    {
        /// <summary>
        /// get the distance value of the shortest route from starting point to end point
        /// </summary>
        /// <param name="graph">
        /// the graph, represented by adjacent matrix
        /// </param>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        int GetShortestRouteDistance(int[,] graph, int startingPoint, int endPoint);
    }
}

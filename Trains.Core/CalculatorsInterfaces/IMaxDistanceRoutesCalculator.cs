namespace Trains.Core.CalculatorsInterfaces
{
    internal interface IMaxDistanceRoutesCalculator
    {
        /// <summary>
        /// get the number of routes from starting point to end point, the distance value for each route should 
        /// be smaller than the given max distance value
        /// </summary>
        /// <param name="graph">
        /// the graph, represented by adjacent matrix
        /// </param>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        int GetRoutesForMaxDistance(int[,] graph, int startingPoint, int endPoint, int maxDistance);
    }
}

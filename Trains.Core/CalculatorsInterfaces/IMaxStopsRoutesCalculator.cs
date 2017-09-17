namespace Trains.Core.CalculatorsInterfaces
{
    internal interface IMaxStopsRoutesCalculator
    {
        /// <summary>
        /// get the number of routes from starting point to end point, with the stops less than the given max stops
        /// </summary>
        /// <param name="graph">
        /// the graph, represented by adjacent matrix
        /// </param>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxStops"></param>
        /// <returns></returns>
        int GetRoutesForMaxStops(int[,] graph, int startingPoint, int endPoint, int maxStops);
    }
}

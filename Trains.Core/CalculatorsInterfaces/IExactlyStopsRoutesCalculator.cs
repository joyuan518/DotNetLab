namespace Trains.Core.CalculatorsInterfaces
{
    internal interface IExactlyStopsRoutesCalculator
    {
        /// <summary>
        /// get the number of routes from starting point to end point, the number of the stops for each route should 
        /// be equal to the given exactly stops 
        /// </summary>
        /// <param name="graph">
        /// the graph, represented by adjacent matrix
        /// </param>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="exactlyStops"></param>
        /// <returns></returns>
        int GetRoutesForExactlyStops(int[,] graph, int startingPoint, int endPoint, int exactlyStops);
    }
}

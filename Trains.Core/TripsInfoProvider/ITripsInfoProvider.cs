namespace Trains.Core.TripsInfoProvider
{
    public interface ITripsInfoProvider
    {
        /// <summary>
        /// to calculate the distance value of a given route
        /// </summary>
        /// <param name="route">
        /// the route, represented by a string.
        /// e.g. "A-B-C" represents a route from vertex A to vertex C via vertex B.  
        /// </param>
        /// <returns></returns>
        int? CalculateDistance(string route);

        /// <summary>
        /// get the number of routes from starting point to end point, the number of the stops for each route should 
        /// be equal to the given exactly stops 
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="exactlyStops"></param>
        /// <returns></returns>
        int GetRoutesForExactlyStops(string startingPoint, string endPoint, int exactlyStops);

        /// <summary>
        /// get the number of routes from starting point to end point, the distance value for each route should 
        /// be smaller than the given max distance value
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        int GetRoutesForMaxDistance(string startingPoint, string endPoint, int maxDistance);

        /// <summary>
        /// get the number of routes from starting point to end point, with the stops less than the given max stops
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxStops"></param>
        /// <returns></returns>
        int GetRoutesForMaxStops(string startingPoint, string endPoint, int maxStops);

        /// <summary>
        /// get the distance value of the shortest route from starting point to end point
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        int GetShortestRouteDistance(string startingPoint, string endPoint);
    }
}

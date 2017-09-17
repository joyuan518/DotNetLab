namespace Trains.Core.CacheProvider
{
    using System.Collections.Generic;

    /// <summary>
    /// the interface of cache provider, which will be used to cache the calculation result
    /// of the methods of RouteCalculator class, to avoid doing the same calculation for multiple times
    /// </summary>
    internal interface ICacheProvider
    {
        /// <summary>
        /// put route quantity in the cache
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxStops"></param>
        /// <param name="routesQuantity"></param>
        void PutRoutesForMaxStops(int startingPoint, int endPoint, int maxStops, int routesQuantity);

        /// <summary>
        /// get route quantity from the cache
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxStops"></param>
        /// <returns></returns>
        int? GetRoutesForMaxStops(int startingPoint, int endPoint, int maxStops);

        /// <summary>
        /// put route quantity in the cache 
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="exactlyStops"></param>
        /// <param name="routesQuantity"></param>
        void PutRoutesForExactlyStops(int startingPoint, int endPoint, int exactlyStops, int routesQuantity);

        /// <summary>
        /// get route quantity from the cache
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="exactlyStops"></param>
        /// <returns></returns>
        int? GetRoutesForExactlyStops(int startingPoint, int endPoint, int exactlyStops);

        /// <summary>
        /// put route quantity in the cache 
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxDistance"></param>
        /// <param name="routesQuantity"></param>
        void PutRoutesForMaxDistance(int startingPoint, int endPoint, int maxDistance, int routesQuantity);

        /// <summary>
        /// get route quantity from the cache
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        int? GetRoutesForMaxDistance(int startingPoint, int endPoint, int maxDistance);

        /// <summary>
        /// put shortest distance in the cache 
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="shortestDistance"></param>
        void PutShortestDistance(int startingPoint, Dictionary<int, int> shortestDistance);

        /// <summary>
        /// get shortest distance from the cache
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        int? GetShortestDistance(int startingPoint, int endPoint);
    }
}

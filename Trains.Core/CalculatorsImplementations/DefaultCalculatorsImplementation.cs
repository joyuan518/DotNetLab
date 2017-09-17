namespace Trains.Core.CalculatorsImplementations
{
    using CalculatorsInterfaces;
    using System.Collections.Generic;
    using CacheProvider;
    using System.Linq;

    /// <summary>
    /// the default implementation of those calculators
    /// </summary>
    internal sealed class DefaultCalculatorsImplementation : IDistanceCalculator, IExactlyStopsRoutesCalculator, 
                                                             IMaxDistanceRoutesCalculator, IMaxStopsRoutesCalculator, IShortestDistanceCalculator
    {
        private class VertexQueueItem
        {
            public VertexQueueItem(int index, int? stops, int? distance)
            {
                Index = index;
                Stops = stops;
                Distance = distance;
            }

            public int Index { get; }
            public int? Stops { get; }
            public int? Distance { get; }
        }

        private class VertexShortestRoute
        {
            public VertexShortestRoute(int index, int shortestRoute)
            {
                Index = index;
                ShortestRoute = shortestRoute;
            }

            public int Index { get; }
            public int ShortestRoute { get; set; }
        }

        private readonly ICacheProvider _cacheProvider;

        public DefaultCalculatorsImplementation(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        int? IDistanceCalculator.CalculateDistance(int[,] graph, int[] route)
        {
            var distance = 0;

            for (var i = 0; i < route.Length - 1; i++)
            {
                var edgeDistance = graph[route[i], route[i + 1]];

                if (edgeDistance == int.MaxValue)
                {
                    return null;
                }

                distance += edgeDistance;
            }

            return distance;
        }

        int IExactlyStopsRoutesCalculator.GetRoutesForExactlyStops(int[,] graph, int startingPoint, int endPoint, int exactlyStops)
        {
            //firstly, try to grab the value from the cache
            var routes = _cacheProvider?.GetRoutesForExactlyStops(startingPoint, endPoint, exactlyStops);
            if (routes != null)
            {
                return routes.Value;
            }

            //not exists in the cache, then do the calculation using the BFS graph traversing algorithm
            var routesNumber = 0;
            var vertexQueue = new Queue<VertexQueueItem>();
            var numberOfVertex = graph.GetLength(0);

            vertexQueue.Enqueue(new VertexQueueItem(startingPoint, 0, null));

            while (vertexQueue.Count > 0)
            {
                var vertex = vertexQueue.Dequeue();

                if (vertex.Stops >= exactlyStops)
                {
                    //all routes with the stops number that equals to or is less than exactly stops have been visited, so quit
                    break;    
                }

                for (var i = 0; i < numberOfVertex; i++)
                {
                    if (graph[vertex.Index, i] != int.MaxValue)
                    {
                        vertexQueue.Enqueue(new VertexQueueItem(i, vertex.Stops + 1, null));

                        if (i == endPoint && vertex.Stops + 1 == exactlyStops)
                        {
                            routesNumber++;
                        }
                    }
                }
            }

            //put the value in the cache
            _cacheProvider?.PutRoutesForExactlyStops(startingPoint, endPoint, exactlyStops, routesNumber);

            return routesNumber;
        }

        int IMaxDistanceRoutesCalculator.GetRoutesForMaxDistance(int[,] graph, int startingPoint, int endPoint, int maxDistance)
        {
            //firstly, try to grab the value from the cache
            var routes = _cacheProvider?.GetRoutesForExactlyStops(startingPoint, endPoint, maxDistance);
            if (routes != null)
            {
                return routes.Value;
            }

            //not exists in the cache, then do the calculation using the BFS graph traversing algorithm
            var routesNumber = 0;
            var vertexQueue = new Queue<VertexQueueItem>();
            var numberOfVertex = graph.GetLength(0);

            vertexQueue.Enqueue(new VertexQueueItem(startingPoint, null, 0));

            while (vertexQueue.Count > 0)
            {
                var vertex = vertexQueue.Dequeue();

                for (var i = 0; i < numberOfVertex; i++)
                {
                    var edgeDistance = graph[vertex.Index, i];

                    if (edgeDistance != int.MaxValue)
                    {
                        if (i == endPoint && vertex.Distance + edgeDistance < maxDistance)
                        {
                            routesNumber++;
                        }

                        if (vertex.Distance + edgeDistance < maxDistance)
                        {
                            vertexQueue.Enqueue(new VertexQueueItem(i, null, vertex.Distance + edgeDistance));
                        }
                    }
                }
            }

            //put the value in the cache
            _cacheProvider?.PutRoutesForMaxDistance(startingPoint, endPoint, maxDistance, routesNumber);

            return routesNumber;
        }

        int IMaxStopsRoutesCalculator.GetRoutesForMaxStops(int[,] graph, int startingPoint, int endPoint, int maxStops)
        {
            //firstly, try to grab the value from the cache
            var routes = _cacheProvider?.GetRoutesForExactlyStops(startingPoint, endPoint, maxStops);
            if (routes != null)
            {
                return routes.Value;
            }

            //not exists in the cache, then do the calculation using the BFS graph traversing algorithm
            var routesNumber = 0;
            var vertexQueue = new Queue<VertexQueueItem>();
            var numberOfVertex = graph.GetLength(0);

            vertexQueue.Enqueue(new VertexQueueItem(startingPoint, 0, null));

            while (vertexQueue.Count > 0)
            {
                var vertex = vertexQueue.Dequeue();

                if (vertex.Stops >= maxStops)
                {
                    //all routes with the stops number that equals to or is less than max stops have been visited, so quit
                    break;
                }

                for (var i = 0; i < numberOfVertex; i++)
                {
                    if (graph[vertex.Index, i] != int.MaxValue)
                    {
                        vertexQueue.Enqueue(new VertexQueueItem(i, vertex.Stops + 1, null));

                        if (i == endPoint)
                        {
                            routesNumber++;
                        }
                    }
                }
            }

            //put the value in the cache
            _cacheProvider?.PutRoutesForMaxStops(startingPoint, endPoint, maxStops, routesNumber);

            return routesNumber;
        }

        int IShortestDistanceCalculator.GetShortestRouteDistance(int[,] graph, int startingPoint, int endPoint)
        {
            //firstly, try to grab the value from the cache
            var shortestDistance = _cacheProvider?.GetShortestDistance(startingPoint, endPoint);

            if (shortestDistance != null)
            {
                return shortestDistance.Value;
            }

            //not exists in the cache, then do the calculation the single source shortest routes using the Dijkstra algorithm
            var vertextList = new List<VertexShortestRoute>();
            var shortestRoutes = new Dictionary<int, int>();

            for (var i = 0; i < graph.GetLength(0); i++)
            {
                vertextList.Add(new VertexShortestRoute(i, graph[startingPoint, i]));
            }

            while (vertextList.Count > 0)
            {
                var shortestRoute = vertextList.OrderBy(v => v.ShortestRoute).First();
                shortestRoutes.Add(shortestRoute.Index, shortestRoute.ShortestRoute);
                vertextList.Remove(shortestRoute);

                foreach (var vertex in vertextList)
                {
                    if (graph[shortestRoute.Index, vertex.Index] != int.MaxValue)
                    {
                        if (shortestRoute.ShortestRoute + graph[shortestRoute.Index, vertex.Index] <
                            vertex.ShortestRoute)
                        {
                            vertex.ShortestRoute = shortestRoute.ShortestRoute +
                                                   graph[shortestRoute.Index, vertex.Index];
                        }    
                    }    
                }
            }

            //put the value in the cache
            _cacheProvider?.PutShortestDistance(startingPoint, shortestRoutes);

            return shortestRoutes[endPoint];
        }
    }
}

namespace Trains.Core.TripsInfoProvider
{
    using System;
    using CalculatorsFactory;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Linq;

    /// <summary>
    /// the TripsInfoProvider class includes all the relevant methods to provide the routes information of the train
    /// </summary>
    internal sealed class DefaultTripsInfoProvider : ITripsInfoProvider
    {
        #region private members

        private readonly string _graphString;
        private string _vertexLetters;
        private readonly BaseCalculatorsFactory _calculatorsFactory;

        private int[,] _graphData;
        private int[,] GraphData => _graphData ?? (_graphData = GetAdjacentMatrix(_graphString));

        #endregion

        #region public members

        /// <summary>
        /// constructor with a graph string as the parameter
        /// </summary>
        /// <param name="graph">
        /// the graph, represented by a string, with all of its edges their weight.
        /// the edges are separated by common. e.g. "AB5,BC2,AC3".
        /// </param>
        /// <param name="calculatorsFactory">calculators factory</param>
        public DefaultTripsInfoProvider(string graph, BaseCalculatorsFactory calculatorsFactory)
        {
            _graphString = graph;
            _calculatorsFactory = calculatorsFactory;
        }

        /// <summary>
        /// convert a graph represented by a string into its adjacent matrix representation
        /// </summary>
        /// <param name="graph">
        /// the graph, represented by a string, with all of its edges their weight.
        /// the edges are separated by commas. e.g. "AB5,BC2,AC3".
        /// </param>
        /// <returns></returns>
        public int[,] GetAdjacentMatrix(string graph)
        {
            graph = graph.Replace(" ", string.Empty).ToUpper();

            if (!(graph.EndsWith(",")))
            {
                graph += ",";
            }

            var regGraph = new Regex(@"^([A-Z][A-Z][1-9]\d*,)+$", RegexOptions.Compiled);

            if (!(regGraph.IsMatch(graph)))
            {
                throw new ArgumentException("The input graph is invalid.", nameof(graph));
            }

            var regEdge = new Regex(@"[A-Z][A-Z][1-9]\d*", RegexOptions.Compiled);
            var edges = regEdge.Matches(graph);

            var regVertex = new Regex(@"[A-Z][A-Z]", RegexOptions.Compiled);
            var vertexes = regVertex.Matches(graph);

            var vertexesSb = new StringBuilder();

            foreach (var vertex in vertexes)
            {
                vertexesSb.Append(vertex);
            }

            _vertexLetters = string.Join(string.Empty, vertexesSb.ToString().Distinct().OrderBy(c => c));
            var graphData = new int[_vertexLetters.Length, _vertexLetters.Length];

            //Initialize the graph data
            for (var i = 0; i < graphData.GetLength(0); i++)
            {
                for (var j = 0; j < graphData.GetLength(1); j++)
                {
                    graphData[i, j] = int.MaxValue;
                }
            }

            foreach (var edge in edges)
            {
                var edgeStr = edge.ToString();

                var vertexIndex1 = _vertexLetters.IndexOf(edgeStr[0]);
                var vertexIndex2 = _vertexLetters.IndexOf(edgeStr[1]);
                var weight = int.Parse(edgeStr[2].ToString());

                graphData[vertexIndex1, vertexIndex2] = weight;                
            }

            return graphData;
        }

        /// <summary>
        /// convert a route represented by a string into an integer arrary contains all of its vertexes
        /// </summary>
        /// <param name="route">
        /// the route, represented by a string.
        /// e.g. "A-B-C" is a route start from vertex A and end at vertex C via vertex B.
        /// </param>
        /// <returns></returns>
        public int[] GetVertexArray(string route)
        {
            route = route.Replace(" ", string.Empty).ToUpper();

            var regRoute = new Regex(@"^([A-Z]-)+[A-Z]$", RegexOptions.Compiled);

            if (!(regRoute.IsMatch(route)))
            {
                throw new ArgumentException("The input route is invalid.", nameof(route));
            }

            var regVertex = new Regex(@"[A-Z]", RegexOptions.Compiled);

            var routeLetters = regVertex.Matches(route);
            var vertexIndexes = new int[routeLetters.Count];

            for (int i = 0; i < routeLetters.Count; i++)
            {
                vertexIndexes[i] = _vertexLetters.IndexOf(Convert.ToChar(routeLetters[i].Value));

                if (vertexIndexes[i] == -1)
                {
                    throw new IndexOutOfRangeException($"Undefined vertex letter '{routeLetters[i]}'.");
                }
            }

            return vertexIndexes;
        }

        #endregion

        #region ITripsInfoProvider members

        int? ITripsInfoProvider.CalculateDistance(string route)
        {
            return _calculatorsFactory.CreateDistanceCalculator().CalculateDistance(GraphData, GetVertexArray(route));
        }

        int ITripsInfoProvider.GetRoutesForExactlyStops(string startingPoint, string endPoint, int exactlyStops)
        {
            var startingPointIndex = _vertexLetters.IndexOf(startingPoint);

            if (startingPointIndex == -1)
            {
                throw new ArgumentException("Invalid starting point.", nameof(startingPoint));    
            }

            var endPointIndex = _vertexLetters.IndexOf(endPoint);

            if (endPointIndex == -1)
            {
                throw new ArgumentException("Invalid end point.", nameof(endPoint));
            }

            return _calculatorsFactory.CreateExactlyStopsRoutesCalculator().GetRoutesForExactlyStops(GraphData, startingPointIndex, endPointIndex, exactlyStops);
        }

        int ITripsInfoProvider.GetRoutesForMaxDistance(string startingPoint, string endPoint, int maxDistance)
        {
            var startingPointIndex = _vertexLetters.IndexOf(startingPoint);

            if (startingPointIndex == -1)
            {
                throw new ArgumentException("Invalid starting point.", nameof(startingPoint));
            }

            var endPointIndex = _vertexLetters.IndexOf(endPoint);

            if (endPointIndex == -1)
            {
                throw new ArgumentException("Invalid end point.", nameof(endPoint));
            }

            return _calculatorsFactory.CreateMaxDistanceRoutesCalculator()
                .GetRoutesForMaxDistance(GraphData, startingPointIndex, endPointIndex, maxDistance);
        }

        int ITripsInfoProvider.GetRoutesForMaxStops(string startingPoint, string endPoint, int maxStops)
        {
            var startingPointIndex = _vertexLetters.IndexOf(startingPoint);

            if (startingPointIndex == -1)
            {
                throw new ArgumentException("Invalid starting point.", nameof(startingPoint));
            }

            var endPointIndex = _vertexLetters.IndexOf(endPoint);

            if (endPointIndex == -1)
            {
                throw new ArgumentException("Invalid end point.", nameof(endPoint));
            }

            return _calculatorsFactory.CreateMaxStopsRoutesCalculator()
                .GetRoutesForMaxStops(GraphData, startingPointIndex, endPointIndex, maxStops);
        }

        int ITripsInfoProvider.GetShortestRouteDistance(string startingPoint, string endPoint)
        {
            var startingPointIndex = _vertexLetters.IndexOf(startingPoint);

            if (startingPointIndex == -1)
            {
                throw new ArgumentException("Invalid starting point.", nameof(startingPoint));
            }

            var endPointIndex = _vertexLetters.IndexOf(endPoint);

            if (endPointIndex == -1)
            {
                throw new ArgumentException("Invalid end point.", nameof(endPoint));
            }

            return _calculatorsFactory.CreateShortestDistanceCalculator()
                .GetShortestRouteDistance(GraphData, startingPointIndex, endPointIndex);
        }

        #endregion

    }
}

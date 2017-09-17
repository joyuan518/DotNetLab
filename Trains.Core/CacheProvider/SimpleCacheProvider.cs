namespace Trains.Core.CacheProvider
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The simple implemenation of the cache provider interface 
    /// </summary>
    internal sealed class SimpleCacheProvider : ICacheProvider
    {
        private readonly Dictionary<int, Dictionary<int, Dictionary<int, int>>> _routesForExactlyStops = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
        private readonly Dictionary<int, Dictionary<int, Dictionary<int, int>>> _routesForMaxDistance = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
        private readonly Dictionary<int, Dictionary<int, Dictionary<int, int>>> _routesForMaxStops = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
        private readonly Dictionary<int, Dictionary<int, int>> _shortestDistance = new Dictionary<int, Dictionary<int, int>>();

        private int? GetDictionaryValue(Dictionary<int, Dictionary<int, Dictionary<int, int>>> dictionary, int key1, int key2, int key3)
        {
            if (dictionary.ContainsKey(key1)
                && dictionary[key1].ContainsKey(key2)
                && dictionary[key1][key2].ContainsKey(key3))
            {
                return dictionary[key1][key2][key3];
            }

            return null;
        }

        private void PutDictionaryValue(Dictionary<int, Dictionary<int, Dictionary<int, int>>> dictionary, int key1, int key2, int key3, int value)
        {
            if (dictionary.ContainsKey(key1)
                && dictionary[key1].ContainsKey(key2)
                && dictionary[key1][key2].ContainsKey(key3))
            {
                dictionary[key1][key2][key3] = value;
            }
            else
            {
                if (!(dictionary.ContainsKey(key1)))
                {
                    var exactlyStopsDic = new Dictionary<int, int> { { key3, value } };
                    var endPointDic = new Dictionary<int, Dictionary<int, int>> { { key2, exactlyStopsDic } };
                    dictionary.Add(key1, endPointDic);
                }
                else
                {
                    if (!(dictionary[key1].ContainsKey(key2)))
                    {
                        var exactlyStopsDic = new Dictionary<int, int> { { key3, value } };
                        dictionary[key1].Add(key2, exactlyStopsDic);
                    }
                    else
                    {
                        dictionary[key1][key2].Add(key3, value);
                    }
                }
            }
        }

        #region ICacheProvider members
        
        int? ICacheProvider.GetRoutesForExactlyStops(int startingPoint, int endPoint, int exactlyStops)
        {
            return GetDictionaryValue(_routesForExactlyStops, startingPoint, endPoint, exactlyStops);
        }

        int? ICacheProvider.GetRoutesForMaxDistance(int startingPoint, int endPoint, int maxDistance)
        {
            return GetDictionaryValue(_routesForMaxDistance, startingPoint, endPoint, maxDistance);
        }

        int? ICacheProvider.GetRoutesForMaxStops(int startingPoint, int endPoint, int maxStops)
        {
            return GetDictionaryValue(_routesForMaxStops, startingPoint, endPoint, maxStops);
        }

        int? ICacheProvider.GetShortestDistance(int startingPoint, int endPoint)
        {
            if (_shortestDistance.ContainsKey(startingPoint))
            {
                return _shortestDistance[startingPoint][endPoint];
            }

            return null;
        }

        void ICacheProvider.PutRoutesForExactlyStops(int startingPoint, int endPoint, int exactlyStops, int routesQuantity)
        {
            PutDictionaryValue(_routesForExactlyStops, startingPoint, endPoint, exactlyStops, routesQuantity);
        }

        void ICacheProvider.PutRoutesForMaxDistance(int startingPoint, int endPoint, int maxDistance, int routesQuantity)
        {
            PutDictionaryValue(_routesForMaxDistance, startingPoint, endPoint, maxDistance, routesQuantity);
        }

        void ICacheProvider.PutRoutesForMaxStops(int startingPoint, int endPoint, int maxStops, int routesQuantity)
        {
            PutDictionaryValue(_routesForMaxStops, startingPoint, endPoint, maxStops, routesQuantity);
        }

        void ICacheProvider.PutShortestDistance(int startingPoint, Dictionary<int, int> shortestDistance)
        {
            if (_shortestDistance.ContainsKey(startingPoint))
            {
                _shortestDistance[startingPoint] = shortestDistance;
            }
            else
            {
                _shortestDistance.Add(startingPoint, shortestDistance);
            }
        }

        #endregion
    }
}

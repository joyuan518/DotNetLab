namespace Trains.Main
{
    using System;
    using Core.TripsInfoProvider;

    class Program
    {
        static void Main(string[] args)
        {
            var testInput = "AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7";

            ITripsInfoProvider tripsInfoProvider = new TripsInfoProviderFactory().GetTripsInfoProvider(testInput);

            foreach (var route in new string[] {"A-B-C", "A-D", "A-D-C", "A-E-B-C-D", "A-E-D"})
            {
                var distance = tripsInfoProvider.CalculateDistance(route);
                Console.WriteLine(distance == null ? "NO SUCH ROUTE" : $"The distance of the route {route} is: {distance}");
            }
      
            Console.WriteLine($"The number of trips starting at C and ending at C with a maximum of 3 stops is: {tripsInfoProvider.GetRoutesForMaxStops("C", "C", 3)}");
            Console.WriteLine($"The number of trips starting at A and ending at C with exactly 4 stops is: {tripsInfoProvider.GetRoutesForExactlyStops("A", "C",  4)}");
            Console.WriteLine($"The length of the shortest route from A to C is: {tripsInfoProvider.GetShortestRouteDistance("A", "C")}");
            Console.WriteLine($"The length of the shortest route from B to B is: {tripsInfoProvider.GetShortestRouteDistance("B", "B")}");
            Console.WriteLine($"The number of trips starting at C and ending at C with a distance of less than 30 is: {tripsInfoProvider.GetRoutesForMaxDistance("C", "C", 30)}");

            Console.ReadLine();
        }
    }
}

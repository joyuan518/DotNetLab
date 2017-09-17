namespace Trains.Core.TripsInfoProvider
{
    public class TripsInfoProviderFactory
    {
        public ITripsInfoProvider GetTripsInfoProvider(string graph)
        {
            return new DefaultTripsInfoProvider(graph, new CalculatorsFactory.CalculatorsFactory());
        }
    }
}

namespace Trains.Core.CalculatorsFactory
{
    using CalculatorsInterfaces;

    /// <summary>
    /// the abstract factory class to create the instances of the implementation of route calculator interfaces
    /// </summary>
    internal abstract class BaseCalculatorsFactory
    {
        public abstract IDistanceCalculator CreateDistanceCalculator();

        public abstract IExactlyStopsRoutesCalculator CreateExactlyStopsRoutesCalculator();

        public abstract IMaxDistanceRoutesCalculator CreateMaxDistanceRoutesCalculator();

        public abstract IMaxStopsRoutesCalculator CreateMaxStopsRoutesCalculator();

        public abstract IShortestDistanceCalculator CreateShortestDistanceCalculator();
    }
}

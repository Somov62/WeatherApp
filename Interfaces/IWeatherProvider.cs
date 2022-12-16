namespace Interfaces
{
    public interface IWeatherProvider
    {
        public string ProviderDomain { get; }

        public void GetWeather(double longitude, double latitude);
    }
}
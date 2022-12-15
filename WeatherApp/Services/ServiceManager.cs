namespace WeatherApp.Services
{
	public static class ServiceManager
    {
		private readonly static InternetConnectionService _connectionService = new InternetConnectionService("open-meteo.com");

		public static InternetConnectionService InternetConnectionService => _connectionService;



	}
}

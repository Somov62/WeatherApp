namespace WeatherApp.Services
{
	public static class ServiceManager
    {
		private readonly static InternetConnectionService _connectionService = new InternetConnectionService();

		public static InternetConnectionService InternetConnectionService => _connectionService;



	}
}

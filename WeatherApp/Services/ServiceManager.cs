namespace WeatherApp.Services
{
	internal static class ServiceManager
    {
		private readonly static InternetConnectionService _connectionService = new InternetConnectionService();

		public static InternetConnectionService InternetConnectionService => _connectionService;


        private readonly static LocalizationService _localizationService = new LocalizationService();

        public static LocalizationService LocalizationService => _localizationService;
    }
}

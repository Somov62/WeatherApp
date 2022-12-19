using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Timers;

namespace WeatherApp.Services
{
    public class InternetConnectionService
    {
        private bool _connection;

        public bool IsConnectionExists
        {
            get => _connection;
            set
            {
                _connection = value;
                ConnectionChanged?.Invoke(value);
            }
        }

        public string HostName { get; set; } = null!;


        public bool AutoCheck
        {
            get => _timer.Enabled;
            set => _timer.Enabled = value;
        }


        private System.Timers.Timer _timer = new System.Timers.Timer();

        public InternetConnectionService(string hostname = null!)
        {
            HostName = hostname;
            _timer.Interval = 3000;
            _timer.AutoReset = true;
            _timer.Elapsed += Tick;
            _timer.Enabled = true;
        }

        private async void Tick(object? sender, ElapsedEventArgs e)
        {
            await CheckInternetConnection();
        }

        private async Task<bool> CheckInternetConnection()
        {
            if (string.IsNullOrEmpty(HostName)) return false;
            Ping ping = new Ping();
            var response = await ping.SendPingAsync(HostName, 3000);
            bool connection = response.Status == IPStatus.Success;
            if (connection != _connection) IsConnectionExists = connection;
            return connection;
        }

        public delegate void InternetConnactionChangedHandler(bool actualConnction);

        public event InternetConnactionChangedHandler ConnectionChanged = null!;




    }
}

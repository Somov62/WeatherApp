using System.Runtime.CompilerServices;
using WeatherApp.Mvvm;
using WeatherApp.Services;

namespace WeatherApp.ViewModels.Base
{
    internal class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
            ServiceManager.InternetConnectionService.ConnectionChanged += ConnectionChanged;
        }

        private void ConnectionChanged(bool actualConnction)
        {
            OnPropertyChanged(nameof(IsConnectionExists));
        }

        public bool IsConnectionExists => ServiceManager.InternetConnectionService.IsConnectionExists;

        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        public virtual void OnAppearing()
        {
                this.OnPropertyChanged();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels.Base;
using static WeatherApp.Services.NavigationService;

namespace WeatherApp.Services
{
    internal static class NavigationService
    {
        private static BaseViewModel _currentView = null!;
        private static List<BaseViewModel> _viewsStack = new List<BaseViewModel>();

        public static BaseViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                Notify();
            }
        }

        public static void SetView(BaseViewModel viewModel)
        {
            CurrentView = viewModel;
            if (!_viewsStack.Contains(viewModel))
                _viewsStack.Add(viewModel);
            
            CurrentView?.OnAppearing();
        }

        public static void GoBack(bool removeView = true)
        {
            if (_viewsStack.Count < 2)
            {
                throw new Exception("You cannot go back when less 2 views in stack");
            }
            int viewIndex = _viewsStack.IndexOf(CurrentView);
            if (viewIndex == 0)
            {
                throw new Exception("You cannot go back, this view is first in stack");
            }
            if (removeView) _viewsStack.RemoveAt(viewIndex);

            SetView(_viewsStack[viewIndex - 1]);
        }


        public delegate void ViewChangedHandler(BaseViewModel actualView);

        public static event ViewChangedHandler? ViewChanged;

        private static void Notify()
        {
            ViewChanged?.Invoke(CurrentView);
        }
    }
}

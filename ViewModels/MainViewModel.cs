using DazFileManager.Infrastructure;
using DazFileManager.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DazFileManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand ShowExtractViewCommand { get; }

        public MainViewModel()
        {
            ShowHomeViewCommand = new RelayCommand(ShowHomeView);
            ShowSettingsViewCommand = new RelayCommand(ShowSettingsView);
            ShowExtractViewCommand = new RelayCommand(ShowExtractView);
            // Default view
            ShowExtractView();
        }

        private void ShowHomeView()
        {
            CurrentViewModel = DIContainer.ServiceProvider.GetRequiredService<HomeViewModel>();
        }

        private void ShowSettingsView()
        {
            CurrentViewModel = DIContainer.ServiceProvider.GetRequiredService<SettingsViewModel>();
        }
        private void ShowExtractView()
        {
            CurrentViewModel = DIContainer.ServiceProvider.GetRequiredService<ExtractViewModel>();
        }
    }
}
